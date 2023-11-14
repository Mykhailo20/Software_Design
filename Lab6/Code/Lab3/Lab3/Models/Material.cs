using System;
using System.Collections.Generic;

namespace Lab3.Models;

public partial class Material
{
    public int MaterialId { get; set; }

    public int? CourseId { get; set; }

    public string Title { get; set; } = null!;

    public virtual BinaryMaterial? BinaryMaterial { get; set; }

    public virtual Course? Course { get; set; }

    public virtual UrlMaterial? UrlMaterial { get; set; }
}
