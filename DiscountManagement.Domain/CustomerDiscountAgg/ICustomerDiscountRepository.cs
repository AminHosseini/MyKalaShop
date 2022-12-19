using _0_Framework.Domain;
using DiscountManagement.Application.Contracts.CustomerDiscount;

namespace DiscountManagement.Domain.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository : IRepository<long, CustomerDiscount>
    {
        List<CustomerDiscountViewModel> Search(SearchCustomerDiscount model);
        EditCustomerDiscount GetDetails(long id);
    }
}
