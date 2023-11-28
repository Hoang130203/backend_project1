using System;
using System.Collections.Generic;

namespace project1_backend.Models;

public partial class Khohang
{
    public int Productid { get; set; }

    public int Quantity { get; set; }

    public virtual Product Product { get; set; } = null!;
}
