using System;
using System.Collections.Generic;

namespace Lab3.Models;

public partial class Assignment
{
    public int AssignmentId { get; set; }

    public int? CourseId { get; set; }

    public string Name { get; set; } = null!;

    public decimal PassingScore { get; set; }

    public DateTime StartTimestamp { get; set; }

    public DateTime DeadlineTimestamp { get; set; }

    public string? Description { get; set; }

    public virtual Course? Course { get; set; }

    public virtual ICollection<Submission> Submissions { get; set; } = new List<Submission>();
}
