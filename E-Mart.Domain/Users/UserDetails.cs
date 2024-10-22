using E_Mart.Domain.Customer;
using System.ComponentModel.DataAnnotations;

namespace E_Mart.Domain.Users
{
    public class UserDetails
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Address_line1 { get; set; }
        public string? Address_line2 { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Mobile { get; set; }
        public bool IsDefault { get; set; }
        public virtual User User { get; set; }
    }
}