using System;
using System.Collections.Generic;

namespace project1_backend.Models;

public partial class Thongbao
{
    public int Notifid { get; set; }

    public int Orderid { get; set; }

    public string? Message { get; set; }

    public DateTime? Time { get; set; }

    public string? Type { get; set; }

    public virtual Donhang Order { get; set; } = null!;
}
