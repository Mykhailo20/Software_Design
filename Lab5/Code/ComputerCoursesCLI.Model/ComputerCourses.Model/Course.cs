using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCoursesCLI.Model
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
        [EnumMember]
        DataScience,

        [EnumMember]
        MachineLearning,

        [EnumMember]
        ArtificialIntelligence,

        [EnumMember]
        WebDevelopment,

        [EnumMember]
        MobileAppDevelopment,

        [EnumMember]
        GameDevelopment,

        [EnumMember]
        Cybersecurity,

        [EnumMember]
        CloudComputing,

        [EnumMember]
        DevOps,

        [EnumMember]
        UIDesign,

        [EnumMember]
        DataEngineering
    }
}
