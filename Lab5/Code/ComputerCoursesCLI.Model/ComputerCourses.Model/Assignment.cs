using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ComputerCoursesCLI.Model
{

    [DataContract]
    public class Assignment
    {
        [DataMember]
        public string CourseName { get; set; } = string.Empty;
        [DataMember]
        public string Name { get; set; } = string.Empty;
        [DataMember]
        public float PassingScore { get; set; }
        [DataMember]
        public string Description { get; set; } = string.Empty;
    }
}
