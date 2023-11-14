using System;
using System.Collections.Generic;

namespace Lab3.Models;

public partial class Submission
{
    public int SubmissionId { get; set; }

    public int? AssignmentId { get; set; }

    public int? StudentId { get; set; }

    public DateTime SubmissionTimestamp { get; set; }

    public byte[]? Content { get; set; }

    public virtual Assignment? Assignment { get; set; }

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual Student? Student { get; set; }
}
