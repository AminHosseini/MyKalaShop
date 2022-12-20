using _0_Framework.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _repository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository repository)
        {
            _repository = repository;
        }

        public OperationResult Create(CreateColleagueDiscount model)
        {
            var operationResult = new OperationResult();

            if (_repository.Exists(x => x.ProductId == model.ProductId && x.DiscountRate == model.DiscountRate))
                return operationResult.Failed();

            var colleagueDiscount = new ColleagueDiscount(model.ProductId, model.DiscountRate);

            _repository.Create(colleagueDiscount);
            _repository.Save();
            return operationResult.Succeeded();
        }


        public OperationResult Edit(EditColleagueDiscount model)
        {
            var operationResult = new OperationResult();
            var colleagueDiscount = _repository.Get(model.Id);

            if (colleagueDiscount == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            if (_repository.Exists(x => x.ProductId == model.ProductId && x.DiscountRate == model.DiscountRate && x.Id != model.Id))
                return operationResult.Failed();

            colleagueDiscount.Edit(model.ProductId, model.DiscountRate);
            _repository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Delete(long id)
        {
            var operationResult = new OperationResult();
            var colleagueDiscount = _repository.Get(id);

            if (colleagueDiscount == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            colleagueDiscount.Delete();
            _repository.Save();
            return operationResult.Succeeded();
        }

        public OperationResult Restore(long id)
        {
            var operationResult = new OperationResult();
            var colleagueDiscount = _repository.Get(id);

            if (colleagueDiscount == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            colleagueDiscount.Restore();
            _repository.Save();
            return operationResult.Succeeded();
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _repository.GetDetails(id);
        }

        public List<ColleagueDiscountViewModel> Search(SearchColleagueDiscount model)
        {
            return _repository.Search(model);
        }
    }
}
