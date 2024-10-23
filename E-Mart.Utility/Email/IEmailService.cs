namespace E_Mart.WebApi.Utilities.Email;

public interface IEmailService
{
    Task SendEmailAsync(MailRequest mailRequest);
}