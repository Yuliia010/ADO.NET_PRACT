using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DAL.Entity
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Summary { get; set; }

        public string Description { get; set; }
        [Required]
        public int Priority { get; set; }
        [Required]
        public DateTime DueDateTime { get; set; }
       
        public int UserId { get; set; }
        
        public  User User { get; set; }

    }
}
