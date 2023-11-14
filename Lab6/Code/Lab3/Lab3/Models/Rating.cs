using System;
using System.Collections.Generic;

namespace Lab3.Models;

public partial class Rating
{
    public int RatingId { get; set; }

    public int? SubmissionId { get; set; }

    public decimal Grade { get; set; }

    public string? Feedback { get; set; }

    public DateTime RatingTimestamp { get; set; }

    public virtual Submission? Submission { get; set; }
}
