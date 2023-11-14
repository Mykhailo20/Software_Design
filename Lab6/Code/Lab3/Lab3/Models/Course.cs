using System;
using System.Collections.Generic;

namespace Lab3.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public int? TeacherId { get; set; }

    public string Title { get; set; } = null!;

    public decimal CreditHours { get; set; }

    public decimal? AvgGrade { get; set; }

    public virtual ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();

    public virtual ICollection<Certificate> Certificates { get; set; } = new List<Certificate>();

    public virtual ICollection<Material> Materials { get; set; } = new List<Material>();

    public virtual ICollection<StudentCourse> StudentCourses { get; set; } = new List<StudentCourse>();

    public virtual Teacher? Teacher { get; set; }
}
