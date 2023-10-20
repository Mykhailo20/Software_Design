using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using ComputerCoursesSystem.Model.Serialization;

namespace ComputerCoursesSystem.Model
{
    [DataContract]
    public class DataModel
    {
        [DataMember]
        public IEnumerable<Teacher> Teachers { get; set; } = new List<Teacher>();
        [DataMember]
        public IEnumerable<Course> Courses { get; set; } = new List<Course>();
        [DataMember]
        public IEnumerable<Assignment> Assignments { get; set; } = new List<Assignment>();

        private static readonly string dataFile = "data.src";

        public static DataModel Load()
        {
            if (File.Exists(dataFile))
            {
                return DataSerializer.DeserializeItem(dataFile);
            }
            return new DataModel();
        }

        public void Save()
        {
            DataSerializer.SerializeData(dataFile, this);
        }
    }
}
