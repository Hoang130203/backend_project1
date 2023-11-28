using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace project1_backend.Models;

public partial class SamphamDonhang
{
    public int Orderid { get; set; }

    public int Productid { get; set; }

    public int Quantity { get; set; }

    public int Cost { get; set; }
    [JsonIgnore]
    public virtual Donhang? Order { get; set; } = null!;
    [JsonIgnore]
    public virtual Product? Product { get; set; } = null!;
}
