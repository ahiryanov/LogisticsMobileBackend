using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}