using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CareerCloud.Pocos
{
    [Table("Company_Job_Educations")]
    public class CompanyJobEducationPoco :IPoco
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("FK_Company_Job_Educations_Company_Jobs")]
        public Guid Job { get; set; }
        public String Major { get; set; }
        public Int16 Importance { get; set; }
        [Column("Time_Stamp")]
        [NotMapped]
        public Byte[] TimeStamp { get; set; }

        public virtual CompanyJobPoco CompanyJob { get; set; }
    }
}
