using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Zapateria.Models
{
    public class articlesViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string description { get; set; }

        public decimal? price { get; set; }

        public decimal? total_in_shelf { get; set; }

        public decimal? total_in_vault { get; set; }

        public int? store_id { get; set; }
        public string store_name { get; set; }
    }
}