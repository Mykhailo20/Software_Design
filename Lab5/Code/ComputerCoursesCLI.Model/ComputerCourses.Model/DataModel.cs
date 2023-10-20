using ComputerCoursesCLI.Model.Serialization;
using System.Runtime.Serialization;


namespace ComputerCoursesCLI.Model
{
    [DataContract]
    public class DataModel
    {
        [DataMember]
        public IEnumerable<Teacher> Teachers { get; set; }
        [DataMember]
        public IEnumerable<Course> Courses { get; set; }
        [DataMember]
        public IEnumerable<Assignment> Assignments { get; set; }

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
