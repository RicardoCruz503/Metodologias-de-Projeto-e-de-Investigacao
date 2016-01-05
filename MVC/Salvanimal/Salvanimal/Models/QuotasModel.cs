using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Salvanimal.Models
{
    public class QuotasContext : DbContext
    {
        public QuotasContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<QuotasEntity> NoticiasProfiles { get; set; }

        public System.Data.Entity.DbSet<Salvanimal.Models.AspNetUsers> AspNetUsers { get; set; }
    }

    [Table("QuotasEntity")]
    public class QuotasEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int QuotaId { get; set; }
        [ForeignKey("UserContext")]
        [Required]
        public string SocioId { get; set; }
        public virtual AspNetUsers UserContext { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "A {0} tem de ter pelo menos {2} caracteres.", MinimumLength = 10)]
        public string Descrição { get; set; }
        [Required]
        public float Valor { get; set; }
        [Required]
        public DateTime data { get; set; }
    }
}