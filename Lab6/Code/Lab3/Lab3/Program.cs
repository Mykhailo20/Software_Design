// See https://aka.ms/new-console-template for more information
using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                var persons = db.Persons.ToList();
                foreach (var person in persons)
                {
                    Console.WriteLine($"{person.PersonId} {person.FirstName} {person.LastName} {person.Email}");
                }

            }
        }
    }
}

