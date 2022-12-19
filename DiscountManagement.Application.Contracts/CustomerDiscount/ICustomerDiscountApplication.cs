using _0_Framework.Application;

namespace DiscountManagement.Application.Contracts.CustomerDiscount
{
    public interface ICustomerDiscountApplication
    {
        OperationResult Create(CreateCustomerDiscount model);
        OperationResult Edit(EditCustomerDiscount model);
        List<CustomerDiscountViewModel> Search(SearchCustomerDiscount model);
        EditCustomerDiscount GetDetails(long id);
    }
}
