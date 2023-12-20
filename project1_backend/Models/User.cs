using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace project1_backend.Models;

public partial class User
{
    public string Phonenumber { get; set; } = null!;

    public string Name { get; set; } = null!;

    public DateTime? Birthdate { get; set; }

    public bool Gender {  get; set; }
    public string? Address { get; set; }

    public string? Avt { get; set; }

    [JsonIgnore]
    public virtual Account? Account { get; set; }
    [JsonIgnore]
    public virtual ICollection<Donhang>? Donhangs { get; set; } = new List<Donhang>();
    [JsonIgnore]
    public virtual ICollection<ProductUser>? ProductUsers { get; set; } = new List<ProductUser>();
    [JsonIgnore]
    public virtual ICollection<SanbongUser>? SanbongUsers { get; set; } = new List<SanbongUser>();
    [JsonIgnore]
    public virtual ICollection<SanphamGiohang> SanphamGiohangs { get; set; } = new List<SanphamGiohang>();
}
