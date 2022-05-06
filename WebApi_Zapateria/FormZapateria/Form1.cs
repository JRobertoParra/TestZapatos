using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebApi_Zapateria.Models;

namespace FormZapateria
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //// TODO: esta línea de código carga datos en la tabla 'dbZapateriaDataSet.articles' Puede moverla o quitarla según sea necesario.
            //this.articlesTableAdapter.Fill(this.dbZapateriaDataSet.articles);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.txtID.Text != string.Empty)
                mLLenaArticulosID();
            else
                //Se llama a metodo que sonsulta la informacion
                mLLenaArticilos();
        }


        private void btnInsertar_Click(object sender, EventArgs e)
        {
            try
            {
                var sMsj = fValidaCajas();
                if (sMsj == string.Empty)
                {
                    this.mInsertar();
                    this.mLLenaArticilos();
                    this.mLimpiaCajas();
                }
                else
                    MessageBox.Show(sMsj);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string fValidaCajas()
        {
            string sMsj = string.Empty;
            try
            {
                var numero = Convert.ToDecimal(this.txtPrice.Text);
            }
            catch (Exception)
            {
                sMsj += "- Valores incorrectos(Prices)\n";
            }

            try
            {
                var numero = Convert.ToDecimal(this.txtTotal.Text);
            }
            catch (Exception)
            {
                sMsj += "- Valores incorrectos(Total in shelf)\n";
            }

            try
            {
                var numero = Convert.ToDecimal(this.txtTotalV.Text);
            }
            catch (Exception)
            {
                sMsj += "- Valores incorrectos(Total vault)\n";
            }

            try
            {
                var numero = Convert.ToDecimal(this.txtStoreId.Text);
            }
            catch (Exception)
            {
                sMsj += "- Valores incorrectos(Store)\n";
            }

           

            return sMsj;
        }


        private void mLimpiaCajas()
        {
            this.txtname.Text = string.Empty;
            this.txtDesc.Text = string.Empty;
            this.txtPrice.Text = string.Empty;
            this.txtTotal.Text = string.Empty;
            this.txtTotalV.Text = string.Empty;
            this.txtStoreId.Text = string.Empty;
        }

        private void mInsertar()
        {
            var httpClient = new HttpClient();
            string URL = "http://localhost:12596/api/Postarticles";
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Accept.Add(contentType);

            var olsArticles = new articlesViewModel();
            olsArticles.Id = 0; //Se calcula al insertar el registro desde el api
            olsArticles.Name = this.txtname.Text;
            olsArticles.description = this.txtDesc.Text;
            olsArticles.price = Decimal.Parse(this.txtPrice.Text);
            olsArticles.total_in_shelf = Decimal.Parse(this.txtTotal.Text);
            olsArticles.total_in_vault = Decimal.Parse(this.txtTotalV.Text);
            olsArticles.store_id = int.Parse(this.txtStoreId.Text);
            //Se convierter a json
            string stringData = JsonConvert.SerializeObject(olsArticles);
            HttpContent httpContent = new StringContent(stringData, Encoding.UTF8, "application/json");
            var json = httpClient.PostAsync(URL, httpContent).Result;

            var msj = json.Content.ReadAsStringAsync();

            MessageBox.Show("Transacción realiza con exito.");
        }

        private string fValidaId()
        {
            string sMsj = string.Empty;
            try
            {
                var numero = Convert.ToDecimal(this.txtID.Text);
            }
            catch (Exception)
            {
                sMsj += "- Valores incorrectos(ID)\n";
            }

           
            return sMsj;
        }

        private void mLLenaArticulosID ()
        {
            var Msj = fValidaId();
            if (Msj == string.Empty)
            {
                var httpCliente = new HttpClient();
                var json = httpCliente.GetStringAsync("http://localhost:12596/services/articles/stores?id=" + this.txtID.Text).Result;
                var lis = JsonConvert.DeserializeObject<List<articlesViewModel>>(json);

                gvArtic.AutoGenerateColumns = true;
                gvArtic.DataSource = lis;
            }
            else
                MessageBox.Show(Msj);
        }

        private void mLLenaArticilos()
        {
            var httpCliente = new HttpClient();
            var json = httpCliente.GetStringAsync("http://localhost:12596/api/articles").Result;
            var lis = JsonConvert.DeserializeObject<List<articlesViewModel>>(json);

            gvArtic.AutoGenerateColumns = true;
            gvArtic.DataSource = lis;
        }

    }
}
