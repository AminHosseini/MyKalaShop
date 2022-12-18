namespace _0_Framework.Application
{
    public class OperationResult
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }

        public OperationResult()
        {
            IsSuccess = false;
        }

        public OperationResult Succeeded(string successMessage = "عملیات با موفقیت انجام شد.")
        {
            IsSuccess = true;
            Message = successMessage;
            return this;
        }

        public OperationResult Failed(string failMessage = "امکان ثبت رکورد تکراری وجود ندارد.")
        {
            IsSuccess = false;
            Message = failMessage;
            return this;
        }
    }
}
