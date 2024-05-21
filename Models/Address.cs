using System;
using System.Collections.Generic;

namespace APIdemo.Models;

public partial class Address
{
    public int Id { get; set; }

    public string? City { get; set; }

    public string? SiteId { get; set; }

    public string? Road { get; set; }
}
