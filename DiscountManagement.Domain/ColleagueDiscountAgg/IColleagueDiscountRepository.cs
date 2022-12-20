using _0_Framework.Domain;
using DiscountManagement.Application.Contracts.ColleagueDiscount;

namespace DiscountManagement.Domain.ColleagueDiscountAgg
{
    public interface IColleagueDiscountRepository : IRepository<long, ColleagueDiscount>
    {
        List<ColleagueDiscountViewModel> Search(SearchColleagueDiscount model);
        EditColleagueDiscount GetDetails(long id);
    }
}
