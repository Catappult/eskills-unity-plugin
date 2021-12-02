namespace Eskills.Scripts.Service.Repository
{
    public class EskillsError
    {
        public ErrorCode ErrorCode { get; }

        public string Message { get; }

        public EskillsError(ErrorCode errorCode, string message)
        {
            Message = message;
            ErrorCode = errorCode;
        }
    }
}