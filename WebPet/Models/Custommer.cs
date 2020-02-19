using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPet.Models
{
    public class Custommer
    {

        [Key]
        public string CustID { get; set; }
        [Phone]
        [Required]
        [Display(Name = "Số Điện Thoại")]
        public string PhoneNo { get; set; }
        [Display(Name = "Họ và Tên")]
        public string FName { get; set; }
        [Required]
        [Display(Name = "Năm Sinh")]
        public int? YearOld { get; set; }
        [EmailAddress]
        [Display(Name = "Địa Chỉ Email")]
        public string Email { get; set; }
        [Display(Name = "Địa Chỉ Lưu Trú")]
        public string Address { get; set; }
        public ICollection<Animal> Animals { get; set; }

    }
}
