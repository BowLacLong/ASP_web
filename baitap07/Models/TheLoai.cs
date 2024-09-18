using System.ComponentModel.DataAnnotations;

namespace baitap07.Models
{
    public class TheLoai
    {
        [Key] 
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}
