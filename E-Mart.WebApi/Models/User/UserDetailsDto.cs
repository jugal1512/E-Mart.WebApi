namespace E_Mart.WebApi.Models.User;

public class UserDetailsDto
{
    public int UserId { get; set; }
    public string Address_line1 { get; set; }
    public string? Address_line2 { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
    public string Mobile { get; set; }
}
