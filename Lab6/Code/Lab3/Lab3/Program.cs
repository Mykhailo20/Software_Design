// See https://aka.ms/new-console-template for more information
using Lab3.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class Program
    {
        static void Main(string[] args)
        {
            using(var db = new ComputerCoursesDbContext())
            {
                UpdatePerson(db);
                ShowPersons(db);
            }
        }

        private static void ShowPersons(ComputerCoursesDbContext db)
        {
            var persons = db.Persons.ToList();
            foreach (var person in persons)
            {
                Console.WriteLine($"{person.PersonId} {person.FirstName} {person.LastName} {person.Email}");
            }
        }

        private static void InsertPerson(ComputerCoursesDbContext db)
        {
            // Create a new Person instance with the data you want to insert
            Person newPerson = new Person
            {
                FirstName = "Ben",
                LastName = "Ten",
                BirthDate = new DateOnly(2005, 5, 15),
                Username = "ben10",
                Password = GetHashedPassword("ben10_password"),
                Email = "ben.ten@gmail.com",
                Phone = "0960001009"
            };

            // Add the new person to the context and save changes
            db.Persons.Add(newPerson);
            Console.WriteLine("\nNew record in table \"person\" was successfully added!\n");
            db.SaveChanges();
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

        private static void UpdatePerson(ComputerCoursesDbContext db)
        {
            int updatePersonId = 32;
            var personToUpdate = db.Persons.FirstOrDefault(person => person.PersonId == updatePersonId);
            if (personToUpdate != null) {
                Console.Write("Enter Person New First Name: ");
                bool updated = false;
                string newFName = Console.ReadLine();
                if((newFName != null) && (newFName != ""))
                {
                    personToUpdate.FirstName = newFName;
                    updated = true;
                }

                Console.Write("Enter New Person New Last Name: ");
                string newLName = Console.ReadLine();
                if ((newLName != null) && (newLName != ""))
                {
                    personToUpdate.LastName = newLName;
                    updated = true;
                }

                if (updated)
                {
                    personToUpdate.Email = $"{personToUpdate.FirstName.ToLower()}.{personToUpdate.LastName.ToLower()}@gmail.com";
                    Console.WriteLine($"\nThe record with person_id = {updatePersonId} was successfully updated\n");
                }
            }
        }
    }
}

