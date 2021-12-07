namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Call")]
    public partial class Call
    {
        public int id { get; set; }

        public int callerId { get; set; }

        public int? calledId { get; set; }

        [StringLength(11)]
        public string calledNumber { get; set; }

        public DateTime time { get; set; }

        public int duration { get; set; }

        public int typeId { get; set; }

        public virtual Client Client { get; set; }

        public virtual Client Client1 { get; set; }

        public virtual Type Type { get; set; }
    }
}
