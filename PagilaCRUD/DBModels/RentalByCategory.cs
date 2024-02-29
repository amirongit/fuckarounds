using System;
using System.Collections.Generic;

namespace PagilaCRUD.DBModels;

public partial class RentalByCategory
{
    public string? Category { get; set; }

    public decimal? TotalSales { get; set; }
}
