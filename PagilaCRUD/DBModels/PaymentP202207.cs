using System;
using System.Collections.Generic;

namespace PagilaCRUD.DBModels;

public partial class PaymentP202207
{
    public int PaymentId { get; set; }

    public int CustomerId { get; set; }

    public int StaffId { get; set; }

    public int RentalId { get; set; }

    public decimal Amount { get; set; }

    public DateTime PaymentDate { get; set; }
}
