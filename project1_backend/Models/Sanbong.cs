using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace project1_backend.Models;

public partial class Sanbong
{
    public string Fieldid { get; set; } = null!;

    public int Price { get; set; }

    public string? Linkimg { get; set; }

    public string? Address { get; set; }

    public int Cost { get; set; }

    public int Rate { get; set; }

    public string? Type { get; set; }

    public string? Decription { get; set; }

    public string Name { get; set; } = null!;
    [JsonIgnore]
    public virtual ICollection<SanbongDonhang>? SanbongDonhangs { get; set; } = new List<SanbongDonhang>();
    [JsonIgnore]
    public virtual ICollection<SanbongUser>? SanbongUsers { get; set; } = new List<SanbongUser>();
}
