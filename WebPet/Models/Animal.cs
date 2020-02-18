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
        public string AnimalName { get; set; }
        // giong loai
        [Required]
        public string Breed { get; set; }
        [Phone]
        public string TypeOfAnimal { get; set; }
        public int YearOld { get; set; }
        public Custommer Owner { get; set; }
    }
}
