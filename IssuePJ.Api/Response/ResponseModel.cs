namespace IssuePJ.Api.Response;

public class ResponseModel<T> where T : class
{
    public string Status { set; get; }
    public string Message { set; get; }
    public T Data { set; get; }
}