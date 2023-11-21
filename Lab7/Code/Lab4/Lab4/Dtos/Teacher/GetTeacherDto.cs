namespace Lab4.Dtos.Teacher
{
    public class GetTeacherDto
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; } = "Ardit";
        public string LastName { get; set; } = "Pythoner";
        public string MiddleName { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; } = new DateOnly();

        public TeachingStyle Style { get; set; } = TeachingStyle.Mentorship;
    }
}
