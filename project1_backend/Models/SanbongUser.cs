using System;
using System.Collections.Generic;

namespace project1_backend.Models;

public partial class SanbongUser
{
    public string Fieldid { get; set; } = null!;

    public string Userphonenumber { get; set; } = null!;

    public int? Rate { get; set; }

    public string? Comment { get; set; }

    public virtual Sanbong Field { get; set; } = null!;

    public virtual User UserphonenumberNavigation { get; set; } = null!;
}
