using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities.SovTask
{
    public partial class People:BaseEntity
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 0)")]
        public Decimal? Height { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 0)")]
        public Decimal? Mass { get; set; }
        [Required]
        [StringLength(30)]
        public string HairColor { get; set; }
        [Required]
        [StringLength(30)]
        public string SkinColor { get; set; }
        [StringLength(30)]
        public string EyeColor { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateOfBirth { get; set; }
        [StringLength(30)]
        public string Gender { get; set; }
        [StringLength(250)]
        public string HomeWorld { get; set; }
    }
}
