namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RateHistory")]
    public partial class RateHistory
    {
        public int id { get; set; }

        public int clientId { get; set; }

        public int rateId { get; set; }

        public DateTime fromDate { get; set; }

        public DateTime? tillDate { get; set; }

        public virtual Client Client { get; set; }

        public virtual Rate Rate { get; set; }
    }
}
