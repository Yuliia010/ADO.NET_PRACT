using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DAL.Entity
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Info { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public string UserName { get; set; }

        [ForeignKey("Ticket")]
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
