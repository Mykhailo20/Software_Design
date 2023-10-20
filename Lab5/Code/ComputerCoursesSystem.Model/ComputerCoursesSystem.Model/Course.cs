using System.Runtime.Serialization;

namespace ComputerCoursesSystem.Model
{
    [DataContract]
    public class Course
    {

        [DataMember]
        public string TeacherName { get; set; } = string.Empty;

        [DataMember]
        public string Title { get; set; } = string.Empty;

        [DataMember]
        public float CreditHours { get; set; }

        [DataMember]
        public Specialization Specialization { get; set; }

        [DataMember]
        public float AvgGrade { get; set; }
    }

    [DataContract]
    public enum Specialization
    {
        [DataMember]
        DataScience,

        [DataMember]
        MachineLearning,

        [DataMember]
        ArtificialIntelligence,

        [DataMember]
        WebDevelopment,

        [DataMember]
        MobileAppDevelopment,

        [DataMember]
        GameDevelopment,

        [DataMember]
        Cybersecurity,

        [DataMember]
        CloudComputing,

        [DataMember]
        DevOps,

        [DataMember]
        UIDesign,

        [DataMember]
        DataEngineering
    }
}
