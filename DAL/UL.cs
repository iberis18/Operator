namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UL")]
    public partial class UL
    {
        [Required]
        [StringLength(50)]
        public string organizationName { get; set; }

        [Required]
        [StringLength(50)]
        public string address { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int userId { get; set; }

        public virtual Client Client { get; set; }
    }
}
