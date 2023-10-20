﻿using System;
using System.Runtime.Serialization;

namespace ComputerCoursesSystem.Model
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
        public DateTime StartTimestamp { get; set; }
        [DataMember]
        public DateTime DeadlineTimestamp { get; set; }
        [DataMember]
        public string Description { get; set; } = string.Empty;
    }
}
