using System;
using System.Collections.Generic;

namespace Lab3.Models;

public partial class CourseInfo
{
    public int? CourseId { get; set; }

    public string? Title { get; set; }

    public decimal? CreditHours { get; set; }

    public decimal? AvgGrade { get; set; }

    public string? Teacher { get; set; }

    public long? TotalStudents { get; set; }
}
