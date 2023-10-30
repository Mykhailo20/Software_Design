using System.Runtime.Serialization;

namespace ComputerCoursesSystem.Model
{
    [DataContract]
    public class Teacher
    {
        [DataMember]
        public string FirstName { get; set; } = string.Empty;

        [DataMember]
        public string LastName { get; set; } = string.Empty;

        [DataMember]
        public string Email { get; set; } = string.Empty;

        [DataMember]
        public string PhoneNumber { get; set; } = string.Empty;

        [DataMember]
        public TeachingStyle style { get; set; }
    }

    [DataContract]
    public enum TeachingStyle
    {
        [EnumMember]
        Mentorship,

        [EnumMember]
        LectureBased,

        [EnumMember]
        Facilitator,

        [EnumMember]
        ProjectBased,

        [EnumMember]
        ProblemSolving
    }
}
