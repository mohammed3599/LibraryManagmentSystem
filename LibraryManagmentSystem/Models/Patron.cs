using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LibraryManagmentSystem.Models
{
    public class Patron
    {
        [Key]
        [JsonIgnore]
        public int PatronId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [JsonIgnore]
        public List<BorrowingTransaction> BorrowingTransactions { get; set; }


        [MaxLength(200)]
        public int Age { get; set; }
    }
}