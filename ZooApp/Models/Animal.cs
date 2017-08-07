using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ZooApp.Models
{
    public class Animal
    {    
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Index("Ix_AnimalName)")]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        [Index("Ix_AnimalOrigin)")]

        public string Origin { get; set; }

        public byte[] Photo { get; set; }

        [Required]

        public int Quantiy { get; set; }

        public virtual ICollection<AnimalFood> AnimalFoods { get; set; }

    }

}