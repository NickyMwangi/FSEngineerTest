using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities.SovTask
{
    public partial class Joke:BaseEntity
    {
        //public Joke()
        //{
        //    Categories = new HashSet<Category>();
        //}

        [Required]
        [StringLength(128)]
        public string CategoryId { get; set; }
        [Required]
        [StringLength(20)]
        public string Code { get; set; }
        [Required]
        [StringLength(128)]
        public string Description { get; set; }
        [StringLength(128)]
        public string Url { get; set; }
        [StringLength(128)]
        public string Icon { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [InverseProperty(nameof(Category.Jokes))]
        public virtual Category JokeCategory { get; set; }

        //[InverseProperty(nameof(Category.CategoryJoke))]
        //public virtual ICollection<Category> Categories { get; set; }

    }
}
