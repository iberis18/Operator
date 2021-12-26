namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public int id { get; set; }

        [StringLength(50)]
        public string password { get; set; }

        public virtual Admin Admin { get; set; }

        public virtual Client Client { get; set; }
    }
}
