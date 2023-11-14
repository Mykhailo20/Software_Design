using System;
using System.Collections.Generic;

namespace Lab3.Models;

public partial class Teacher
{
    public int TeacherId { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual Person TeacherNavigation { get; set; } = null!;
}
