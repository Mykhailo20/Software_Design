// See https://aka.ms/new-console-template for more information
using Lab3.Models;
using System.Security.Cryptography;

namespace Lab3
{
    public class Program
    {
        static void Main(string[] args)
        {
            using(var db = new ComputerCoursesDbContext())
            {
                string choicesString = "Select the desired operation: "
                                        + "\n1 - View records of the \"person\" table"
                                        + "\n2 - Add a new record to the \"person\" table"
                                        + "\n3 - Update a record in the \"person\" table"
                                        + "\n4 - Delete a record from the \"person\" table"
                                        + "\n5 or any other number - Exit";
                int operationChoice = 0;
                while (true)
                {
                    operationChoice = GetUserChoice(choicesString);
                    switch (operationChoice)
                    {
                        case 1: 
                            ShowPersons(db);
                            break;
                        case 2:
                            HandleInsertPerson(db);
                            break;
                        case 3:
                            HandleUpdatePerson(db);
                            break;
                        case 4:
                            HandleDeletePerson(db);
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

        private static void ShowPersons(ComputerCoursesDbContext db)
        {
            Console.WriteLine();
            var persons = db.Persons.ToList();
            if(persons.Count == 0)
            {
                Console.WriteLine("The \"Skills\" table does not contain any records.\n");
                return;
            }
            int maxFirstNameLength = persons.Max(p => p.FirstName.Length);
            int maxLastNameLength = persons.Max(p => p.LastName.Length);

            if(maxFirstNameLength < "First Name".Length) {
                maxFirstNameLength = "First Name".Length;
            }
            if(maxLastNameLength < "Last Name".Length)
            {
                maxLastNameLength = "Last Name".Length;
            }

            Console.WriteLine($"{"Id".PadRight(3)} " +
                          $"{"First Name".PadRight(maxFirstNameLength)} " +
                          $"{"Last Name".PadRight(maxLastNameLength)} " +
                          $"Email");

            foreach (var person in persons)
            {
                Console.WriteLine($"{person.PersonId.ToString().PadRight(3)} " +
                          $"{person.FirstName.PadRight(maxFirstNameLength)} " +
                          $"{person.LastName.PadRight(maxLastNameLength)} " +
                          $"{person.Email}");
            }
            Console.WriteLine();
        }

        private static void HandleInsertPerson(ComputerCoursesDbContext db)
        {
            Console.Write("Enter first name: ");
            string fName = Console.ReadLine();
            Console.Write("Enter last name: ");
            string lName = Console.ReadLine();
            DateOnly birthDate = new DateOnly(2005, 5, 15);
            while (true)
            {
                Console.Write("Enter birth date in the format \"yyyy-MM-dd\" (for example, 1990-05-15): ");
                string birthDateInput = Console.ReadLine();

                if (DateTime.TryParseExact(birthDateInput, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                {
                    birthDate = DateOnly.FromDateTime(parsedDate);
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid date format. Please enter the date in the correct format.".ToUpper());
                }
            }
            
            Console.Write("Enter username: ");
            string username = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            Console.Write("Enter phone number (10 numbers): ");
            string phone = Console.ReadLine();

            // Create a new Person instance with the data you want to insert
            Person newPerson = new Person
            {
                FirstName = fName,
                LastName = lName,
                BirthDate = birthDate,
                Username = username,
                Password = GetHashedPassword(password),
                Email = $"{fName.ToLower()}.{lName.ToLower()}@gmail.com",
                Phone = phone
            };

            // Add the new person to the context and save changes
            db.Persons.Add(newPerson);
            db.SaveChanges();
            Console.WriteLine("A new record was successfully added to the \"person\" table!\n");
        }

        // Helper method to hash the password using SHA-256
        private static string GetHashedPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }

        private static void HandleUpdatePerson(ComputerCoursesDbContext db)
        {
            Console.WriteLine("Enter person Id to update: ");
            if(int.TryParse(Console.ReadLine(), out int updatePersonId))
            {
                var personToUpdate = db.Persons.FirstOrDefault(person => person.PersonId == updatePersonId);
                if (personToUpdate != null)
                {
                    Console.Write("Enter person new first name: ");
                    bool updated = false;
                    string newFName = Console.ReadLine();
                    if ((newFName != null) && (newFName != ""))
                    {
                        personToUpdate.FirstName = newFName;
                        updated = true;
                    }

                    Console.Write("Enter person new last name: ");
                    string newLName = Console.ReadLine();
                    if ((newLName != null) && (newLName != ""))
                    {
                        personToUpdate.LastName = newLName;
                        updated = true;
                    }

                    if (updated)
                    {
                        personToUpdate.Email = $"{personToUpdate.FirstName.ToLower()}.{personToUpdate.LastName.ToLower()}@gmail.com";
                        db.SaveChanges();
                        Console.WriteLine($"\nThe record with person Id = {updatePersonId} was successfully updated!\n");
                    }
                }
                else
                {
                    Console.WriteLine($"\nThe record with person Id = {updatePersonId} not found.\n");
                }
            }
            else
            {
                Console.Write("Invalid input for person Id! Please enter a positive integer.".ToUpper());
            }
        }

        private static void HandleDeletePerson(ComputerCoursesDbContext db)
        {
            Console.WriteLine("Enter person Id to delete: ");
            if (int.TryParse(Console.ReadLine(), out int deletePersonId))
            {
                var personToDelete = db.Persons.FirstOrDefault(person => person.PersonId == deletePersonId);
                if (personToDelete != null)
                {
                    db.Persons.Remove(personToDelete);
                    db.SaveChanges();
                    Console.WriteLine($"\nThe record with person Id = {deletePersonId} was successfully deleted!\n");
                }
                else
                {
                    Console.WriteLine($"\nThe record with person Id = {deletePersonId} not found.\n");
                }
            }
            else
            {
                Console.Write("Invalid input for person Id. Please enter a positive integer.".ToUpper());
            }  
        }
    }
}

