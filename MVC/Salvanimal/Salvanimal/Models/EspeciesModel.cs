using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Salvanimal.Models
{
    public class EspeciesContext : DbContext
    {
        public EspeciesContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<EspeciesEntity> NoticiasProfiles { get; set; }
    }

    [Table("EspeciesEntity")]
    public class EspeciesEntity
    {
        [Key]
        [Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int EspecieId { get; set; }

        [Required]
        public string NomeEspecie { get; set; }
    }
}