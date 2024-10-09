using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BaiKiemTra03_03.Models
{
    public class KhachHang
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int PhoneNumer { get; set; }
        [Required]
        public string Email { get; set; }

    }
}
