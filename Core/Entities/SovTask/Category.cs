using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Entities.SovTask
{
    public partial class Category:BaseEntity
    {
        public Category()
        {
            Jokes = new HashSet<Joke>();
        }
        [Required]
        [StringLength(128)]
        public string Code { get; set; }
        [Required]
        [StringLength(128)]
        public string Description { get; set; }

        //[ForeignKey(nameof(JokeId))]
        //[InverseProperty(nameof(Joke.Categories))]
        //public virtual Joke CategoryJoke { get; set; }
        [InverseProperty(nameof(Joke.JokeCategory))]
        public virtual ICollection<Joke> Jokes { get; set; }
    }
}
