namespace E_Mart.WebApi.Models.Response;

public class DataResponse<T> where T : class
{
    public T Data { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
}