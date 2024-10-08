using E_Mart.Domain.Base;
using E_Mart.Domain.OrderDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Mart.Domain.Payments;
public class PaymentDetails : BaseEntity
{
    public int OrderId { get; set; }
    public double Amount { get; set; }
    public string Provider { get; set; }
    public bool Status { get; set; }
    public virtual OrderDetails.OrderDetails OrderDetails { get; set; }
}
