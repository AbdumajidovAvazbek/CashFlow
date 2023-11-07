namespace CashFlow.Service.Exceptions;

public class CashFlowExseption : Exception
{
    public int _statusCode {  get; set; }

    public CashFlowExseption(int code,string message) : base (message)
    {
        _statusCode = code;
    }
}
