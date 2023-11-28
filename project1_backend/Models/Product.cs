using System;
using System.Collections.Generic;

namespace project1_backend.Models;

public partial class Product
{
    public int Productid { get; set; }

    public string Productname { get; set; } = null!;

    public string? Detail { get; set; }

    public int Price { get; set; }

    public string Color { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string? Linkimg { get; set; }

    public int? Rate { get; set; }

    public virtual Khohang? Khohang { get; set; }

    public virtual ICollection<ProductUser> ProductUsers { get; set; } = new List<ProductUser>();

    public virtual ICollection<SamphamDonhang> SamphamDonhangs { get; set; } = new List<SamphamDonhang>();

    public virtual ICollection<SanphamGiohang> SanphamGiohangs { get; set; } = new List<SanphamGiohang>();
}
