namespace E_Mart.WebApi.Models.Response;

public class DataResponseList<T> where T : class
{
    public List<T> Data { get; set; }
    public string Status { get; set; }
    public string Message { get; set; }
}