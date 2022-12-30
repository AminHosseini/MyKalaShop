using _0_Framework.Application.ZarinPal;
using Microsoft.AspNetCore.Mvc;
using MyKalaShopQuery.Contracts;
using MyKalaShopQuery.Contracts.Product;
using Nancy.Json;
using ShopManagement.Application.Contracts.Order;
using System.Globalization;

namespace ServiceHost.Controllers
{
    public class CartController : Controller
    {
        private const string CookieName = "cart-items";
        private readonly IProductQuery _productQuery;
        private readonly ICartCalculatorService _cartCalculatorService;
        private readonly ICartService _cartService;
        private readonly IOrderApplication _orderApplication;
        private readonly IZarinPalFactory _zarinPalFactory;

        public CartController(IProductQuery productQuery, ICartCalculatorService cartCalculatorService,
            ICartService cartService, IOrderApplication orderApplication, IZarinPalFactory zarinPalFactory)
        {
            _productQuery = productQuery;
            _cartCalculatorService = cartCalculatorService;
            _cartService = cartService;
            _orderApplication = orderApplication;
            _zarinPalFactory = zarinPalFactory;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var serializer = new JavaScriptSerializer();
            var cookie = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(cookie);

            foreach (var item in cartItems)
                item.CalculateItemTotalPrice();

            var model = _productQuery.CheckInventoryStatus(cartItems);
            return View(model);
        }

        [HttpGet]
        public IActionResult RemoveFromCart(long id)
        {
            Response.Cookies.Delete(CookieName);

            var serializer = new JavaScriptSerializer();
            var cookie = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(cookie);

            var itemToRemove = cartItems.FirstOrDefault(x => x.Id == id);
            cartItems.Remove(itemToRemove);
            var cookieItems = serializer.Serialize(cartItems);

            var options = new CookieOptions()
            {
                Expires = DateTime.Now.AddDays(1),
                IsEssential = true
            };

            Response.Cookies.Append(CookieName, cookieItems, options);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult RecheckCart()
        {
            var serializer = new JavaScriptSerializer();
            var cookie = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(cookie);

            foreach (var item in cartItems)
                item.CalculateItemTotalPrice();

            var model = _productQuery.CheckInventoryStatus(cartItems);
            return RedirectToAction(model.Any(x => !x.IsInStock) ? nameof(Index) : nameof(CheckOut));
        }

        [HttpGet]
        public IActionResult CheckOut()
        {
            var model = new Cart();

            var serializer = new JavaScriptSerializer();
            var cookie = Request.Cookies[CookieName];
            var cartItems = serializer.Deserialize<List<CartItem>>(cookie);

            foreach (var item in cartItems)
                item.CalculateItemTotalPrice();

            model = _cartCalculatorService.ComputeCart(cartItems);
            _cartService.Set(model);

            return View(model);
        }

        [HttpPost]
        public IActionResult Pay(int paymentMethod)
        {
            var cart = _cartService.Get();
            cart.SetPaymentMethod(paymentMethod);

            var result = _productQuery.CheckInventoryStatus(cart.Items);
            if (result.Any(x => !x.IsInStock))
                return RedirectToAction(nameof(Index));

            var orderId = _orderApplication.PlaceOrder(cart);

            if (paymentMethod == PaymentMethod.Online)
            {
                var paymentResponse = _zarinPalFactory.CreatePaymentRequest(
                    cart.PayAmount.ToString(CultureInfo.InvariantCulture), "", "",
                    "خرید از درگاه فروشگاه کالای من", orderId);

                return Redirect($"https://{_zarinPalFactory.Prefix}.zarinpal.com/pg/StartPay/{paymentResponse.Authority}");
            }

            var issueTrackingNo = _orderApplication.SetIssueTrackingNo(orderId);
            Response.Cookies.Delete(CookieName);

            var paymentResult = new PaymentResult().Succeeded(
                    "سفارش شما با موفقیت ثبت شد. پس از تماس کارشناسان ما و پرداخت وجه، سفارش ارسال خواهد شد.", issueTrackingNo);

            return RedirectToAction(nameof(PaymentResult), paymentResult);
        }

        [HttpGet]
        public IActionResult CallBack([FromQuery] string authority, [FromQuery] string status,
            [FromQuery] long oId)
        {
            var orderAmount = _orderApplication.GetAmountById(oId);
            var verificationResponse =
                _zarinPalFactory.CreateVerificationRequest(authority,
                    orderAmount.ToString(CultureInfo.InvariantCulture));

            var result = new PaymentResult();
            if (status == "OK" && verificationResponse.Status >= 100)
            {
                var issueTrackingNo = _orderApplication.SetIssueTrackingNo(oId);
                var operationResult = _orderApplication.PaymentSucceeded(oId, verificationResponse.RefID);
                Response.Cookies.Delete(CookieName);
                result = result.Succeeded("پرداخت با موفقیت انجام شد.", issueTrackingNo);
                return RedirectToAction(nameof(PaymentResult), result);
            }

            result = result.Failed(
                "پرداخت با موفقیت انجام نشد. درصورت کسر وجه از حساب، مبلغ تا 24 ساعت دیگر به حساب شما بازگردانده خواهد شد.");
            return RedirectToAction(nameof(PaymentResult), result);
        }

        [HttpGet]
        public IActionResult PaymentResult(PaymentResult paymentResult)
        {
            return View(paymentResult);
        }
    }
}
