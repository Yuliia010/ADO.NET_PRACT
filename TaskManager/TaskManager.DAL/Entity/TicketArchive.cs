using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DAL.Entity
{
    public class TicketArchive
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Summary { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Priority { get; set; }
        [Required]
        public string DueDateTime { get; set; }
        [Required]
        public string UserName { get; set; }

    }
}
