using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_15092023.DAL.Entity
{
    public class StudentCard
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public DateTime ExpireDate { get; set; }

        public bool IsActive { get; set; }

        public virtual Student Student { get; set; }

    }
}
