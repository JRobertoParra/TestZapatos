using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebApi_Zapateria.Models;

namespace FormZapateria
{
    public partial class FrmStore : Form
    {
        public FrmStore()
        {
            InitializeComponent();
        }

        private void btnConsultar_Click(object sender, EventArgs e)
        {
            this.mConsultaStore();
        }

        private void mConsultaStore()
        {
            var httpCliente = new HttpClient();
            var json = httpCliente.GetStringAsync("http://localhost:12596/services/stores").Result;
            var lis = JsonConvert.DeserializeObject<List<StoreViewModel>>(json);

            gvStore.AutoGenerateColumns = true;
            gvStore.DataSource = lis;
        }
    }
}
