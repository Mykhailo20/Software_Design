using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Models;

public partial class ComputerCoursesDbContext : DbContext
{
    public ComputerCoursesDbContext()
    {
    }

    public ComputerCoursesDbContext(DbContextOptions<ComputerCoursesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<AssignmentInfo> AssignmentInfos { get; set; }

    public virtual DbSet<BinaryMaterial> BinaryMaterials { get; set; }

    public virtual DbSet<Certificate> Certificates { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseInfo> CourseInfos { get; set; }

    public virtual DbSet<CourseStudentsInfo> CourseStudentsInfos { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Person> Persons { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<StudentCourse> StudentCourses { get; set; }

    public virtual DbSet<Submission> Submissions { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<UrlMaterial> UrlMaterials { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost;Database=DB_Lozinskyi_ComputerCourses;Username=***;Password=***");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasPostgresEnum("assignment_status_enum", new[] { "Not Started", "In Progress", "Completed" })
            .HasPostgresEnum("specialization_enum", new[] { "Data Science", "Machine Learning", "Artificial Intelligence", "Web Development", "Mobile App Development", "Game Development", "Cybersecurity", "Cloud Computing", "UI/UX Design", "DevOps" })
            .HasPostgresEnum("submission_status_enum", new[] { "Submitted", "Evaluating", "Evaluated", "Late", "Revised" })
            .HasPostgresEnum("teaching_style_enum", new[] { "Mentorship", "Lecture-Based", "Facilitator", "Project-Based", "Problem-Solving" })
            .HasPostgresExtension("pgcrypto")
            .HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.AssignmentId).HasName("assignment_pkey");

            entity.ToTable("assignment");

            entity.Property(e => e.AssignmentId).HasColumnName("assignment_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.DeadlineTimestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("deadline_timestamp");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.PassingScore)
                .HasPrecision(5, 2)
                .HasColumnName("passing_score");
            entity.Property(e => e.StartTimestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("start_timestamp");

            entity.HasOne(d => d.Course).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("assignment_course_id_fkey");
        });

        modelBuilder.Entity<AssignmentInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("assignment_info");

            entity.Property(e => e.AssignmentId).HasColumnName("assignment_id");
            entity.Property(e => e.AssignmentName)
                .HasMaxLength(100)
                .HasColumnName("assignment_name");
            entity.Property(e => e.AvgAssignmentTime).HasColumnName("avg_assignment_time");
            entity.Property(e => e.AvgStudentsGrade).HasColumnName("avg_students_grade");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.CourseTitle)
                .HasMaxLength(100)
                .HasColumnName("course_title");
            entity.Property(e => e.PassingScore)
                .HasPrecision(5, 2)
                .HasColumnName("passing_score");
            entity.Property(e => e.TotalSubmissions).HasColumnName("total_submissions");
        });

        modelBuilder.Entity<BinaryMaterial>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("binary_material_pkey");

            entity.ToTable("binary_material");

            entity.Property(e => e.MaterialId)
                .ValueGeneratedOnAdd()
                .HasColumnName("material_id");
            entity.Property(e => e.Content).HasColumnName("content");

            entity.HasOne(d => d.Material).WithOne(p => p.BinaryMaterial)
                .HasForeignKey<BinaryMaterial>(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("binary_material_material_id_fkey");
        });

        modelBuilder.Entity<Certificate>(entity =>
        {
            entity.HasKey(e => e.CertificateNumber).HasName("certificate_pkey");

            entity.ToTable("certificate");

            entity.Property(e => e.CertificateNumber)
                .HasDefaultValueSql("uuid_generate_v4()")
                .HasColumnName("certificate_number");
            entity.Property(e => e.CertificateUrl)
                .HasMaxLength(2048)
                .HasColumnName("certificate_url");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.IssuanceDate).HasColumnName("issuance_date");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.StudentScore)
                .HasPrecision(5, 2)
                .HasColumnName("student_score");

            entity.HasOne(d => d.Course).WithMany(p => p.Certificates)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("certificate_course_id_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.Certificates)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("certificate_student_id_fkey");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("course_pkey");

            entity.ToTable("course");

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.AvgGrade)
                .HasPrecision(5, 2)
                .HasColumnName("avg_grade");
            entity.Property(e => e.CreditHours).HasColumnName("credit_hours");
            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Courses)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("course_teacher_id_fkey");
        });

        modelBuilder.Entity<CourseInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("course_info");

            entity.Property(e => e.AvgGrade)
                .HasPrecision(5, 2)
                .HasColumnName("avg_grade");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.CreditHours).HasColumnName("credit_hours");
            entity.Property(e => e.Teacher).HasColumnName("teacher");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.TotalStudents).HasColumnName("total_students");
        });

        modelBuilder.Entity<CourseStudentsInfo>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("course_students_info");

            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.TeacherId).HasColumnName("teacher_id");
            entity.Property(e => e.TeacherName).HasColumnName("teacher_name");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
            entity.Property(e => e.TotalCertified).HasColumnName("total_certified");
            entity.Property(e => e.TotalStudents).HasColumnName("total_students");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("material_pkey");

            entity.ToTable("material");

            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");

            entity.HasOne(d => d.Course).WithMany(p => p.Materials)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("material_course_id_fkey");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.PersonId).HasName("person_pkey");

            entity.ToTable("person");

            entity.HasIndex(e => e.Email, "person_email_key").IsUnique();

            entity.HasIndex(e => e.Phone, "person_phone_key").IsUnique();

            entity.HasIndex(e => e.Username, "person_username_key").IsUnique();

            entity.HasIndex(e => e.Username, "unique_username").IsUnique();

            entity.Property(e => e.PersonId).HasColumnName("person_id");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("last_name");
            entity.Property(e => e.MiddleName)
                .HasMaxLength(50)
                .HasColumnName("middle_name");
            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .HasColumnName("phone");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("rating_pkey");

            entity.ToTable("rating");

            entity.Property(e => e.RatingId).HasColumnName("rating_id");
            entity.Property(e => e.Feedback).HasColumnName("feedback");
            entity.Property(e => e.Grade)
                .HasPrecision(5, 2)
                .HasColumnName("grade");
            entity.Property(e => e.RatingTimestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("rating_timestamp");
            entity.Property(e => e.SubmissionId).HasColumnName("submission_id");

            entity.HasOne(d => d.Submission).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.SubmissionId)
                .HasConstraintName("rating_submission_id_fkey");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("student_pkey");

            entity.ToTable("student");

            entity.Property(e => e.StudentId)
                .ValueGeneratedOnAdd()
                .HasColumnName("student_id");

            entity.HasOne(d => d.StudentNavigation).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.StudentId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("student_student_id_fkey1");
        });

        modelBuilder.Entity<StudentCourse>(entity =>
        {
            entity.HasKey(e => new { e.StudentId, e.CourseId }).HasName("student_course_pkey");

            entity.ToTable("student_course");

            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.EnrollmentDate).HasColumnName("enrollment_date");
            entity.Property(e => e.PassPercentage)
                .HasPrecision(5, 2)
                .HasColumnName("pass_percentage");

            entity.HasOne(d => d.Course).WithMany(p => p.StudentCourses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_course_course_id_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.StudentCourses)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("student_course_student_id_fkey");
        });

        modelBuilder.Entity<Submission>(entity =>
        {
            entity.HasKey(e => e.SubmissionId).HasName("submission_pkey");

            entity.ToTable("submission");

            entity.Property(e => e.SubmissionId).HasColumnName("submission_id");
            entity.Property(e => e.AssignmentId).HasColumnName("assignment_id");
            entity.Property(e => e.Content).HasColumnName("content");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.SubmissionTimestamp)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("submission_timestamp");

            entity.HasOne(d => d.Assignment).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.AssignmentId)
                .HasConstraintName("submission_assignment_id_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("submission_student_id_fkey");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("teacher_pkey");

            entity.ToTable("teacher");

            entity.Property(e => e.TeacherId)
                .ValueGeneratedOnAdd()
                .HasColumnName("teacher_id");

            entity.HasOne(d => d.TeacherNavigation).WithOne(p => p.Teacher)
                .HasForeignKey<Teacher>(d => d.TeacherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("teacher_teacher_id_fkey");
        });

        modelBuilder.Entity<UrlMaterial>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("url_material_pkey");

            entity.ToTable("url_material");

            entity.Property(e => e.MaterialId)
                .ValueGeneratedOnAdd()
                .HasColumnName("material_id");
            entity.Property(e => e.Url)
                .HasMaxLength(2048)
                .HasColumnName("url");

            entity.HasOne(d => d.Material).WithOne(p => p.UrlMaterial)
                .HasForeignKey<UrlMaterial>(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("url_material_material_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
