using _0_Framework.Application;

namespace DiscountManagement.Application.Contracts.ColleagueDiscount
{
    public interface IColleagueDiscountApplication
    {
        OperationResult Create(CreateColleagueDiscount model);
        OperationResult Edit(EditColleagueDiscount model);
        OperationResult Delete(long id);
        OperationResult Restore(long id);
        List<ColleagueDiscountViewModel> Search(SearchColleagueDiscount model);
        EditColleagueDiscount GetDetails(long id);
    }
}
