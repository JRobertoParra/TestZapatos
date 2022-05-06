namespace WebApi_Zapateria.DbObjects
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class articles
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        [StringLength(300)]
        public string description { get; set; }

        public decimal? price { get; set; }

        public decimal? total_in_shelf { get; set; }

        public decimal? total_in_vault { get; set; }

        public int? store_id { get; set; }
    }
}
