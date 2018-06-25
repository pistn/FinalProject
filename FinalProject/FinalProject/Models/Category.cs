using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public partial class Category
    {
        public Category()
        {
            Project = new HashSet<Project>();
        }

        [Column("ID")]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(255)]
        public string Description { get; set; }

        [InverseProperty("Category")]
        public ICollection<Project> Project { get; set; }
    }
}
