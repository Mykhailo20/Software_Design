using ComputerCoursesCLI.Model.Serialization;

namespace ComputerCoursesCLI.Model
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DataModel dataModel = new DataModel();
            dataModel.Teachers = new List<Teacher>()
            { new Teacher() {
                FirstName = "Ardit",
                LastName = "Pythoner",
                Email = "ardit.pythoner@gmail.com",
                PhoneNumber = "0970001000",
                Style = TeachingStyle.ProjectBased
            },new Teacher(){
                FirstName = "Angela",
                LastName = "Web",
                Email = "angela.web@gmail.com",
                PhoneNumber = "0970001001",
                Style = TeachingStyle.Facilitator
              }
            };


            dataModel.Courses = new List<Course>()
            { new Course() {TeacherName = "Ardit Pythoner", Title="Neural Networks in Python from Scratch", CreditHours=10,
                Specialization=Specialization.MachineLearning, AvgGrade=85.00f},
              new Course() {TeacherName = "Anjela Web", Title="NestJS: The Complete Developer's Guide", CreditHours=20,
                Specialization=Specialization.WebDevelopment, AvgGrade=81.00f}
            };

            dataModel.Assignments = new List<Assignment>()
            { new Assignment() {CourseName="Neural Networks in Python from Scratch", Name="Install Python 3",
                PassingScore = 100.00f, Description=String.Empty},
              new Assignment() {CourseName="NestJS: The Complete Developer's Guide", Name="What is JavaScript?",
                PassingScore = 100.00f, Description=String.Empty}
            };

            String filename = "organizer.src";
            DataSerializer.SerializeData(filename, dataModel);
            Console.WriteLine("dataModel was successfully serialized!");

            var readModel = DataSerializer.DeserializeItem(filename);
            Console.WriteLine("dataModel was successfully deserialized!");
            Console.WriteLine("Results of deserialization:");
            Console.WriteLine();
            PrintTeachers(readModel);

            Console.WriteLine();
            PrintCourses(readModel);

            Console.WriteLine();
            PrintAssignments(readModel);
        }

        public static void PrintTeachers(DataModel model)
        {
            Console.WriteLine("Teachers:");
            foreach(var teacher in model.Teachers)
            {
                Console.WriteLine($"{teacher.FirstName} {teacher.LastName}");
            }
        }

        public static void PrintCourses(DataModel model)
        {
            Console.WriteLine("Courses:");
            foreach (var course in model.Courses)
            {
                Console.WriteLine($"{course.TeacherName}; {course.Title}");
            }
        }

        public static void PrintAssignments(DataModel model)
        {
            Console.WriteLine("Assignments:");
            foreach (var assignment in model.Assignments)
            {
                Console.WriteLine($"{assignment.CourseName}; {assignment.Name}");
            }
        }
    }
}
