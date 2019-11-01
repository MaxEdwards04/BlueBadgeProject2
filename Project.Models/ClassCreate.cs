using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class ClassCreate
    {
        public int ClassId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Primary Gun")]
        public string PrimaryGun { get; set; }
        [Display(Name ="Primary Attachment")]
        public string PrimaryAttach { get; set; }
        [Display(Name = "Secondary Gun")]
        public string SecondaryGun { get; set; }
        [Display(Name = "Secondary Attachment")]
        public string SecondaryAttach { get; set; }
    }
}
