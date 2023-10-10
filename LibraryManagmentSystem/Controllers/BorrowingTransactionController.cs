using LibraryManagmentSystem;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibaryManagmentSystemAPI.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class BorrowingTransactionController : ControllerBase
    {
        public static LibraryDbContext _Context;

        public BorrowingTransactionController(LibraryDbContext DB)
        {
            _Context = DB;
        }

        [HttpGet("ViewBorrowingHistory")]
        public void ViewBorrowingHistory(int patronId)
        {

            var borrowingHistory = _Context.borrowingTransactions
                .Include(history => history.Patron)
                .Include(history => history.Book)
                .Where(history => history.PatronId == patronId)
                .ToList();

            if (borrowingHistory != null && borrowingHistory.Any())
            {
                var patron = borrowingHistory.First().Patron;
                Console.WriteLine($"Borrowing History for Patron: {patron.Name}");
                Console.WriteLine("------------------------------------------------");

                foreach (var history in borrowingHistory)
                {
                    Console.WriteLine($"Book Title: {history.Book.Title}");
                    Console.WriteLine($"Borrow Date: {history.BorrowDate}");
                    Console.WriteLine($"Return Date: {history.ReturnDate ?? DateTime.MinValue}"); // Use DateTime.MinValue if ReturnDate is null
                    Console.WriteLine();
                }

            }
            else
            {
                Console.WriteLine($"No borrowing history found for Patron with ID {patronId}.");

            }

        }
        [HttpDelete("RemoveBorrowingTransaction/{transactionId}")]
        public void RemoveBorrowingTransaction(int transactionId)
        {
            var transactionToRemove = _Context.borrowingTransactions.Find(transactionId);

            if (transactionToRemove == null)
            {

                Console.WriteLine($"Borrowing transaction with ID {transactionId} not found.");
            }
            else
            {
                _Context.borrowingTransactions.Remove(transactionToRemove);
                _Context.SaveChanges();


                Console.WriteLine($"Borrowing transaction with ID {transactionId} has been removed.");
            }
        }
        [HttpGet("TransactionsByBorrowDate")]
        public void TransactionsByBorrowDate(DateTime borrowDate)
        {
            var transactions = _Context.borrowingTransactions
                .Where(history => history.BorrowDate.Date == borrowDate.Date)
                .ToList();

            if (transactions != null && transactions.Any())
            {

                foreach (var transaction in transactions)
                {
                    Console.WriteLine($"Book Title: {transaction.Book.Title}");
                    Console.WriteLine($"Borrow Date: {transaction.BorrowDate}");
                    Console.WriteLine($"Return Date: {transaction.ReturnDate ?? DateTime.MinValue}"); // Use DateTime.MinValue if ReturnDate is null
                    Console.WriteLine();
                }
            }
            else
            {

                Console.WriteLine($"No transactions found with Borrow Date {borrowDate.Date}.");
            }
        }

        [HttpGet("TransactionsByReturnDate")]
        public void TransactionsByReturnDate(DateTime returnDate)
        {
            var transactions = _Context.borrowingTransactions
                .Where(history => history.ReturnDate != null && history.ReturnDate.Value.Date == returnDate.Date)
                .ToList();

            if (transactions != null && transactions.Any())
            {

                foreach (var transaction in transactions)
                {
                    Console.WriteLine($"Book Title: {transaction.Book.Title}");
                    Console.WriteLine($"Borrow Date: {transaction.BorrowDate}");
                    Console.WriteLine($"Return Date: {transaction.ReturnDate ?? DateTime.MinValue}"); // Use DateTime.MinValue if ReturnDate is null
                    Console.WriteLine();
                }
            }
            else
            {

                Console.WriteLine($"No transactions found with Return Date {returnDate.Date}.");
            }
        }

        [HttpGet("TransactionsByPatron")]
        public void TransactionsByPatron(int patronId)
        {
            var transactions = _Context.borrowingTransactions
                .Include(history => history.Patron)
                .Where(history => history.PatronId == patronId)
                .ToList();

            if (transactions != null && transactions.Any())
            {

                foreach (var transaction in transactions)
                {
                    Console.WriteLine($"Book Title: {transaction.Book.Title}");
                    Console.WriteLine($"Borrow Date: {transaction.BorrowDate}");
                    Console.WriteLine($"Return Date: {transaction.ReturnDate ?? DateTime.MinValue}"); // Use DateTime.MinValue if ReturnDate is null
                    Console.WriteLine();
                }
            }
            else
            {

                Console.WriteLine($"No transactions found for Patron with ID {patronId}.");
            }
        }

        [HttpGet("TransactionsByBook")]
        public void TransactionsByBook(int bookId)
        {
            var transactions = _Context.borrowingTransactions
                .Include(history => history.Book)
                .Where(history => history.BookId == bookId)
                .ToList();

            if (transactions != null && transactions.Any())
            {

                foreach (var transaction in transactions)
                {
                    Console.WriteLine($"Book Title: {transaction.Book.Title}");
                    Console.WriteLine($"Borrow Date: {transaction.BorrowDate}");
                    Console.WriteLine($"Return Date: {transaction.ReturnDate ?? DateTime.MinValue}"); // Use DateTime.MinValue if ReturnDate is null
                    Console.WriteLine();
                }
            }
            else
            {

                Console.WriteLine($"No transactions found for Book with ID {bookId}.");
            }
        }

        [HttpGet("BorrowCountForBook")]
        public void BorrowCountForBook(int bookId)
        {
            var borrowCount = _Context.borrowingTransactions
                .Count(history => history.BookId == bookId);

            Console.WriteLine($"Borrow count for Book with ID {bookId}: {borrowCount}");
        }

    }
}