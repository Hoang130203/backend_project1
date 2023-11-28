using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace project1_backend.Models;

public partial class SanbongUser
{
    public string Fieldid { get; set; } = null!;

    public string Userphonenumber { get; set; } = null!;

    public int? Rate { get; set; }

    public string? Comment { get; set; }
    [JsonIgnore]
    public virtual Sanbong? Field { get; set; } = null!;
    [JsonIgnore]    
    public virtual User? UserphonenumberNavigation { get; set; } = null!;
}
