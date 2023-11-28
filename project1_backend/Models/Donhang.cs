using System;
using System.Collections.Generic;

namespace project1_backend.Models;

public partial class Donhang
{
    public int Orderid { get; set; }

    public string Phonenumber { get; set; } = null!;

    public int Totalcost { get; set; }

    public string? Status { get; set; }

    public virtual User PhonenumberNavigation { get; set; } = null!;

    public virtual ICollection<SamphamDonhang> SamphamDonhangs { get; set; } = new List<SamphamDonhang>();

    public virtual ICollection<SanbongDonhang> SanbongDonhangs { get; set; } = new List<SanbongDonhang>();

    public virtual ICollection<Thongbao> Thongbaos { get; set; } = new List<Thongbao>();
}
