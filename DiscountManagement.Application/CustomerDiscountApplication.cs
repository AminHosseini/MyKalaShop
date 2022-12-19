using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;

namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _repository;

        public CustomerDiscountApplication(ICustomerDiscountRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Create(CreateCustomerDiscount model)
        {
            var operationResult = new OperationResult();

            if (_repository.Exists(x => x.ProductId == model.ProductId && x.StartDate.ToString() == model.StartDate))
                return operationResult.Failed();


            var startDate = model.StartDate.ToGeorgianDateTime();
            var endDate = model.EndDate.ToGeorgianDateTime();

            var customerDiscount = new CustomerDiscount(model.DiscountRate, model.Reason,
                startDate, endDate, model.ProductId);

            _repository.Create(customerDiscount);
            _repository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Edit(EditCustomerDiscount model)
        {
            var operationResult = new OperationResult();
            var customerDiscount = _repository.Get(model.Id);

            if (customerDiscount == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            if (_repository.Exists(x => x.ProductId == model.ProductId && 
                x.StartDate.ToString() == model.StartDate && x.Id != model.Id))
                return operationResult.Failed();

            var startDate = model.StartDate.ToGeorgianDateTime();
            var endDate = model.EndDate.ToGeorgianDateTime();

            customerDiscount.Edit(model.DiscountRate, model.Reason,
                startDate, endDate, model.ProductId);

            _repository.Save();
            return operationResult.Succeeded();
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search(SearchCustomerDiscount model)
        {
            return _repository.Search(model);
        }
    }
}
