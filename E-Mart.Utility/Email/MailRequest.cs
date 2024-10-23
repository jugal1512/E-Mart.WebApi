using Microsoft.AspNetCore.Http;

namespace E_Mart.WebApi.Utilities.Email;

public class MailRequest
{
    public List<string> ToEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public List<string> ToCC { get; set; }
    public List<IFormFile> Attachments { get; set; }
}