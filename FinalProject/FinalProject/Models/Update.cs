using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public partial class Update
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("ProjectID")]
        public long ProjectId { get; set; }
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime DateOfUpdate { get; set; }

        [ForeignKey("ProjectId")]
        [InverseProperty("Update")]
        public Project Project { get; set; }
    }
}
