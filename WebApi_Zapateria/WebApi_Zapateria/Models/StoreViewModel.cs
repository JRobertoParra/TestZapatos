using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi_Zapateria.Models
{
    public class StoreViewModel
    {
        public int store_id { get; set; }

        public string name { get; set; }

        public string address {get; set;}
    }
}