using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
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
    [JsonIgnore]
    public virtual Khohang? Khohang { get; set; }
    [JsonIgnore]
    public virtual ICollection<ProductUser>? ProductUsers { get; set; } = new List<ProductUser>();
    [JsonIgnore]
    public virtual ICollection<SamphamDonhang>? SamphamDonhangs { get; set; } = new List<SamphamDonhang>();
    [JsonIgnore]
    public virtual ICollection<SanphamGiohang>? SanphamGiohangs { get; set; } = new List<SanphamGiohang>();
}
