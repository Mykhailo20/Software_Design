using System.Text.Json.Serialization;

namespace Lab4.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public string FirstName { get; set; } = "Ardit";
        public string LastName { get; set; } = "Pythoner";
        public string MiddleName { get; set; } = string.Empty;
        public DateOnly BirthDate { get; set; } = new DateOnly();

        public TeachingStyle Style { get; set; } = TeachingStyle.Mentorship;
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TeachingStyle
    {
        Mentorship = 1,
        LectureBased = 2,
        ProjectBased = 3
    }
}
