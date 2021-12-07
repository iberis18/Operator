namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rate")]
    public partial class Rate
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rate()
        {
            Client = new HashSet<Client>();
            RateHistory = new HashSet<RateHistory>();
        }

        public int id { get; set; }

        [StringLength(50)]
        public string name { get; set; }

        public decimal? cityCost { get; set; }

        public decimal? intercityCost { get; set; }

        public decimal? internationalCost { get; set; }

        public float? GB { get; set; }

        public int? SMS { get; set; }

        public int? minutes { get; set; }

        public decimal? connectionCost { get; set; }

        public decimal? GBCost { get; set; }

        public decimal? SMSCost { get; set; }

        public decimal? cost { get; set; }

        public bool? corporate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Client> Client { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RateHistory> RateHistory { get; set; }
    }
}
