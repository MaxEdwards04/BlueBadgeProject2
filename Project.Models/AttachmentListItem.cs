using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class AttachmentListItem
    {
        public int AttachmentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Is a Primary Weapon")]
        public bool IsPrimary { get; set; }

        [Display(Name = "CreatedUtc")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
