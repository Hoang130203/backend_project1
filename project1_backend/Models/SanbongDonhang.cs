using System;
using System.Collections.Generic;

namespace project1_backend.Models;

public partial class SanbongDonhang
{
    public int Orderid { get; set; }

    public string Fieldid { get; set; } = null!;

    public int Kip { get; set; }

    public DateTime Times { get; set; }

    public int Cost { get; set; }

    public string? Note { get; set; }

    public virtual Sanbong Field { get; set; } = null!;

    public virtual Donhang Order { get; set; } = null!;
}
