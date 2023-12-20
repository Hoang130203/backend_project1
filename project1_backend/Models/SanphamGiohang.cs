using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace project1_backend.Models;

public partial class SanphamGiohang
{
    public string Userphonenumber { get; set; } = null!;

    public int Productid { get; set; }

    public int Price { get; set; }

    public int Quantity { get; set; }

    public string Color { get; set; }

    [JsonIgnore]
    public virtual Product? Product { get; set; } = null!;
    [JsonIgnore]
    public virtual User? UserphonenumberNavigation { get; set; } = null!;
}
