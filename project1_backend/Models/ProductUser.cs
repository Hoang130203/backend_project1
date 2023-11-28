using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace project1_backend.Models;

public partial class ProductUser
{
    public int Productid { get; set; }

    public string Userphonenumber { get; set; } = null!;

    public int? Rate { get; set; }

    public string? Comment { get; set; }

    public DateTime? Time { get; set; }
    [JsonIgnore]
    public virtual Product? Product { get; set; } = null!;
    [JsonIgnore]
    public virtual User? UserphonenumberNavigation { get; set; } = null!;
}
