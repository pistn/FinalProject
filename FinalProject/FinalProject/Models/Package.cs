using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public partial class Package
    {
        public Package()
        {
            PersonPackage = new HashSet<PersonPackage>();
        }

        [Column("ID")]
        public long Id { get; set; }
        [Column("ProjectID")]
        public long ProjectId { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public decimal Value { get; set; }

        [ForeignKey("ProjectId")]
        [InverseProperty("Package")]
        public Project Project { get; set; }
        [InverseProperty("Package")]
        public ICollection<PersonPackage> PersonPackage { get; set; }

        //public List<Project> Title { get; set; }//navigation
    }
}
