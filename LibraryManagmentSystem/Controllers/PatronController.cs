using LibraryManagmentSystem.Models;
using LibraryManagmentSystem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Reflection.Metadata.BlobBuilder;


namespace LibaryManagmentSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatronController : ControllerBase
    {
        public static LibraryDbContext _Context;

        public PatronController(LibraryDbContext DB)
        {
            _Context = DB;
        }


        [Authorize]
        [HttpPost("ADD")]
        public void addPatron(string name, string phoneNumber, int age)
        {
            var newPatron = new Patron
            {
                Name = name,
                PhoneNumber = phoneNumber,
                Age = age
            };

            _Context.patrons.Add(newPatron);
            _Context.SaveChanges();
            Console.WriteLine("Patron is added");
        }




        [HttpDelete("Delete")]
        public void deletePatron(int patronId)
        {
            var deletePatrons = _Context.patrons.FirstOrDefault(x => x.PatronId == patronId);

            if (deletePatrons != null)
            {
                _Context.patrons.Remove(deletePatrons);
                _Context.SaveChanges();
                Console.WriteLine("Patron is delete");
            }
            else
            {
                Console.WriteLine("Patron is not found");
            }
        }

        [HttpPut("update")]
        public void update(int patronId, string name, string phoneNumber, int age)
        {
            var updatePatron = _Context.patrons.FirstOrDefault(x => x.PatronId == patronId);
            if (updatePatron != null)
            {
                updatePatron.Name = name;
                updatePatron.PhoneNumber = phoneNumber;
                updatePatron.Age = age;
                _Context.SaveChanges();
                Console.WriteLine("Patron is update");
            }
            else
            {
                Console.WriteLine("Patron is not found");
            }
        }

        [HttpGet("View")]
        public void viewAllPatron(int patronId)
        {
            var viewPatron = _Context.patrons.FirstOrDefault(x => x.PatronId == patronId);
            if (viewPatron == null)

            {
                Console.WriteLine("The book is not found");
            }
            else
            {
                Console.WriteLine("Title: " + viewPatron.Name);
                Console.WriteLine("Author: " + viewPatron.PhoneNumber);

            }
        }



    }
}
