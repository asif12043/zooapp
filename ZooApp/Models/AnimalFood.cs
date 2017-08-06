using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ZooApp.Models
{
    public class AnimalFood
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }

        [ForeignKey("Animal")]
        [Required]
        public int AnimalId { get; set; }

        public virtual Animal Animal { get; set; }

        [ForeignKey("Food")]
        [Required]
        public int FoodId { get; set; }

        public virtual Food Food { get; set; }

        [Required]
        public double Quantiy { get; set; }
    }
}