using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Salvanimal.Models
{

    public class AnimalsContext : DbContext
    {
        public AnimalsContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<AnimalsEntity> NoticiasProfiles { get; set; }
    }

    [Table("AnimalsEntity")]
    public class QuotasEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AnimalId { get; set; }
        [Required]
        [StringLength(50)]
        public string Nome { get; set; }
        [Required]
        public int Idade { get; set; }
        public String Observacoes { get; set; }
    }
}