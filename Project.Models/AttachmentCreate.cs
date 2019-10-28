using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class AttachmentCreate
    {
        public int AttachmentId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrimary { get; set; }
    }
}
