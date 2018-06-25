using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public partial class PersonPackage
    {
        [Column("PackageID")]
        public long PackageId { get; set; }
        [Column("PersonID")]
        public string PersonId { get; set; }

        [ForeignKey("PackageId")]
        [InverseProperty("PersonPackage")]
        public Package Package { get; set; }
    }
}
