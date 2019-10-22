using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models
{
    public class GunListItem
    {
        public int GunId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsPrimary { get; set; }

        [Display(Name="CreatedUtc")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
