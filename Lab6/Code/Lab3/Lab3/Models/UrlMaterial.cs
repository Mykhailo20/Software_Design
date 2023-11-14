using System;
using System.Collections.Generic;

namespace Lab3.Models;

public partial class UrlMaterial
{
    public int MaterialId { get; set; }

    public string Url { get; set; } = null!;

    public virtual Material Material { get; set; } = null!;
}
