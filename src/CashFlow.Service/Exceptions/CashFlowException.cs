namespace CashFlow.Service.Exceptions;

public class CashFlowException : Exception
{
    public int _statusCode {  get; set; }

    public CashFlowException(int code,string message) : base (message)
    {
        _statusCode = code;
    }
}
