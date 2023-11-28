using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace project1_backend.Models;

public partial class Khohang
{
    public int Productid { get; set; }

    public int Quantity { get; set; }

    [JsonIgnore]
    public virtual Product? Product { get; set; } = null!;
}
