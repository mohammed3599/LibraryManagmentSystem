using LibaryManagmentSystemAPI.Models;
using LibraryManagmentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibaryManagmentSystemAPI.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public static LibraryDbContext _Context;

        public BookController(LibraryDbContext DB)
        {
            _Context = DB;
        }

        [HttpPost]
        public void addBook(string title, string author, int publicationYear)
        {
            if (_Context.books.Any(x => x.Title == title && x.Author == author))
            {
                Console.WriteLine("Book is already exisit");
                return;
            }
            var newBook = new Book
            {
                Title = title,
                Author = author,
                PublicationYear = publicationYear,
                IsAvailable = true

            };
            _Context.books.Add(newBook);
            _Context.SaveChanges();
            Console.WriteLine("Book is added");

        }

        [HttpPut]
        public void update(string title, string author, int publishYear)
        {
            var update = _Context.books.FirstOrDefault(x => x.Title == title);
            if (update != null)
            {
                update.Title = title;
                update.Author = author;
                update.PublicationYear = publishYear;
                _Context.SaveChanges();
                Console.WriteLine("book is update");
            }
            else
            {
                Console.WriteLine("The book is not found");
            }
        }

        [HttpDelete]
        public void deleteBook(int bookId)
        {
            var deleteBook = _Context.books.FirstOrDefault(x => x.BookId == bookId);
            if (deleteBook != null)
            {
                _Context.books.Remove(deleteBook);
                _Context.SaveChanges();
                Console.WriteLine("The book is delete");
            }
            else
            {
                Console.WriteLine("The book is not found");
            }
        }
        [HttpGet]
        public void getBook(int bookID)
        {
            var books = _Context.books.
                FirstOrDefault(x => x.BookId == bookID);


            if (books == null)

            {
                Console.WriteLine("The book is not found");
            }
            else
            {
                Console.WriteLine("Title: " + books.Title);
                Console.WriteLine("Author: " + books.Author);
                Console.WriteLine("Publication Year: " + books.PublicationYear);
                Console.WriteLine("Available: " + books.IsAvailable);

            }

        }
    }



}


