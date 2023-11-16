using Lab3CodeFirst.Models;

namespace Lab3CodeFirst
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using(var db = new ComputerCoursesDbContext())
            {
                string choicesString = "Select the desired operation: "
                                        + "\n1 - View records of the \"skills\" table"
                                        + "\n2 - Add a new record to the \"skills\" table"
                                        + "\n3 - Update a record in the \"skills\" table"
                                        + "\n4 - Delete a record from the \"skills\" table"
                                        + "\n5 or any other number - Exit";
                int operationChoice = 0;
                while (true)
                {
                    operationChoice = GetUserChoice(choicesString);
                    switch (operationChoice)
                    {
                        case 1:
                            ShowSkills(db);
                            break;
                        case 2:
                            HandleInsertSkill(db);
                            break;
                        case 3:
                            HandleUpdateSkill(db);
                            break;
                        case 4:
                            HandleDeleteSkill(db);
                            break;
                        default:
                            Console.WriteLine("\nThe program completed its execution successfully!");
                            return;
                    }
                }
            }
        }

        private static int GetUserChoice(string choiceString)
        {
            while (true)
            {
                Console.WriteLine(choiceString);
                Console.Write("Your choice: ");
                try
                {
                    return int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a number.\n".ToUpper());
                }
            }
        }

        private static void ShowSkills(ComputerCoursesDbContext db)
        {
            Console.WriteLine();
            var skills = db.Skills.ToList();
            if(skills.Count == 0)
            {
                Console.WriteLine("The \"Skills\" table does not contain any records.\n");
                return;
            }
            int maxNameLength = skills.Max(s => s.Name.Length);

            if (maxNameLength < "Name".Length)
            {
                maxNameLength = "Name".Length;
            }
            Console.WriteLine($"{"Id".PadRight(3)} " +
                          $"{"Name".PadRight(maxNameLength)} " +
                          $"{"Level".PadRight(7)} " +
                          $"Description");

            foreach (var skill in skills)
            {
                Console.WriteLine($"{skill.SkillId.ToString().PadRight(3)} " +
                          $"{skill.Name.PadRight(maxNameLength)} " +
                          $"{skill.Level.ToString().PadRight(7)} " +
                          $"{skill.Description}");
            }
            Console.WriteLine();
        }

        private static void HandleInsertSkill(ComputerCoursesDbContext db)
        {
            Console.Write("Enter skill name: ");
            string skillName = Console.ReadLine();
            int level = 1;
            int tempLevel = 1;
            while (true)
            {
                Console.Write("Enter skill level (from 1 to 10): ");
                try
                {
                    tempLevel = int.Parse(Console.ReadLine());
                    if((tempLevel > 1) && (tempLevel < 10))
                    {
                        level = tempLevel;
                        break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a number.\n".ToUpper());
                }
            }

            Console.Write("Enter description: ");
            string description = Console.ReadLine();

            // Create a new Person instance with the data you want to insert
            Skill newSkill = new Skill
            {
                Name = skillName,
                Level = level,
                Description = description
            };

            // Add the new person to the context and save changes
            db.Skills.Add(newSkill);
            db.SaveChanges();
            Console.WriteLine("\nA new record was successfully added to the \"skill\" table!\n");
        }

        private static void HandleUpdateSkill(ComputerCoursesDbContext db)
        {
            Console.WriteLine("Enter skill Id to update: ");
            if (int.TryParse(Console.ReadLine(), out int updateSkillId))
            {
                var skillToUpdate = db.Skills.FirstOrDefault(skill => skill.SkillId == updateSkillId);
                if (skillToUpdate != null)
                {
                    Console.Write("Enter skill new name: ");
                    bool updated = false;
                    string newName = Console.ReadLine();
                    if ((newName != null) && (newName != ""))
                    {
                        skillToUpdate.Name = newName;
                        updated = true;
                    }

                    Console.Write("Enter skill new level: ");
                    int level = 1;
                    while (true)
                    {
                        Console.Write("Enter skill level (from 1 to 10): ");
                        try
                        {
                            level = int.Parse(Console.ReadLine());
                            if ((level > 1) && (level < 10))
                            {
                                skillToUpdate.Level = level;
                                break;
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Invalid input. Please enter a number.\n".ToUpper());
                        }
                    }

                    Console.Write("Enter skill new description: ");
                    string newDescription = Console.ReadLine();
                    if ((newDescription != null) && (newDescription != ""))
                    {
                        skillToUpdate.Description = newDescription;
                        updated = true;
                    }

                    if (updated)
                    {
                        db.SaveChanges();
                        Console.WriteLine($"\nThe record with skill Id = {updateSkillId} was successfully updated!\n");
                    }

                }
                else
                {
                    Console.WriteLine($"\nThe record with skill Id = {updateSkillId} not found.\n");
                }
            }
            else
            {
                Console.Write("Invalid input for skill Id! Please enter a positive integer.".ToUpper());
            }
        }

        private static void HandleDeleteSkill(ComputerCoursesDbContext db)
        {
            Console.WriteLine("Enter skill Id to delete: ");
            if (int.TryParse(Console.ReadLine(), out int deleteSkillId))
            {
                var skillToDelete = db.Skills.FirstOrDefault(skill => skill.SkillId == deleteSkillId);
                if (skillToDelete != null)
                {
                    db.Skills.Remove(skillToDelete);
                    db.SaveChanges();
                    Console.WriteLine($"\nThe record with skill Id = {deleteSkillId} was successfully deleted!\n");
                }
                else
                {
                    Console.WriteLine($"\nThe record with skill Id = {deleteSkillId} not found.\n");
                }
            }
            else
            {
                Console.Write("Invalid input for skill Id. Please enter a positive integer.".ToUpper());
            }
        }
    }
}

