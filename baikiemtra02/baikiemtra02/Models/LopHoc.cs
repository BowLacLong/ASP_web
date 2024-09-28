using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace baikiemtra02.Models
{
    public class LopHoc
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? TenLopHoc { get; set; }
        [Required]
        public int SoLuongSinhVien { get; set; }
        [Display(Name = "Năm Nhập Học")]
        public DateTime NamNhapHoc { get; set; }
        [Display(Name = "Năm Ra Trường")]
        public DateTime NamRaTruong { get; set; }
    }
}
