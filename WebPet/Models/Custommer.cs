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
        public Guid CustID { get; set; }
        public string FName { get; set; }
        [Required]
        public string LName { get; set; }
        [Phone]
        public string PhoneNo { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Address { get; set; }
        public List<Animal> Animals { get; set; }

    }
}
