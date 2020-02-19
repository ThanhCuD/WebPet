using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebPet.Models
{
    public class Animal
    {
        [Key]
        public Guid AniID { get; set; }
        [Required]
        [Display(Name = "Tên Thú Cưng")]
        public string AnimalName { get; set; }
        // giong loai
        [Required]
        [Display(Name = "Loài")]
        public string Breed { get; set; }
        [Display(Name = "Giống")]
        public string TypeOfAnimal { get; set; }
        [Display(Name = "Năm Tuổi")]
        public int YearOld { get; set; }
        [Display(Name = "Chủ Sở Hữu")]
        public string OwnerID { get; set; }
        public Custommer Owner { get; set; }
    }
}
