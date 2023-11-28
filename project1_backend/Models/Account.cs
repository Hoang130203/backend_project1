using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace project1_backend.Models;

public partial class Account
{
    public string Phonenumber { get; set; } = null!;

    public string Password { get; set; } = null!;


    [JsonIgnore]
    public virtual User? PhonenumberNavigation { get; set; } = null!;
}
