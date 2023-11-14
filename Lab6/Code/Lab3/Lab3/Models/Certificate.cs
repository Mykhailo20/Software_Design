using System;
using System.Collections.Generic;

namespace Lab3.Models;

public partial class Certificate
{
    public Guid CertificateNumber { get; set; }

    public int? CourseId { get; set; }

    public int? StudentId { get; set; }

    public DateOnly IssuanceDate { get; set; }

    public string CertificateUrl { get; set; } = null!;

    public decimal StudentScore { get; set; }

    public virtual Course? Course { get; set; }

    public virtual Student? Student { get; set; }
}
