using System;
using System.Collections.Generic;

namespace Lab3.Models;

public partial class CourseStudentsInfo
{
    public int? CourseId { get; set; }

    public string? Title { get; set; }

    public int? TeacherId { get; set; }

    public string? TeacherName { get; set; }

    public long? TotalStudents { get; set; }

    public long? TotalCertified { get; set; }
}
