using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace project1_backend.Models;

public partial class Admin
{
    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Name { get; set; }

    public string? PhoneNumber { get; set; }
}
