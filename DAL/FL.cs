namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FL")]
    public partial class FL
    {
        [Required]
        [StringLength(50)]
        public string FIO { get; set; }

        [Required]
        [StringLength(10)]
        public string pasportDetales { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int userId { get; set; }

        public virtual Client Client { get; set; }
    }
}
