using System;
using System.Collections.Generic;

namespace Lab3.Models;

public partial class AssignmentInfo
{
    public int? CourseId { get; set; }

    public string? CourseTitle { get; set; }

    public int? AssignmentId { get; set; }

    public string? AssignmentName { get; set; }

    public decimal? PassingScore { get; set; }

    public long? TotalSubmissions { get; set; }

    public decimal? AvgStudentsGrade { get; set; }

    public string? AvgAssignmentTime { get; set; }
}
