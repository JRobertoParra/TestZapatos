namespace WebApi_Zapateria.DbObjects
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class stores
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int store_id { get; set; }

        [StringLength(100)]
        public string name { get; set; }

        [StringLength(300)]
        public string address { get; set; }
    }
}
