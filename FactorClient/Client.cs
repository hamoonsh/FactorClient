using FactorClient.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;

namespace FactorClient
{
    public partial class Client : Form
    {
        public Client()
        {
            InitializeComponent();
        }
        private const string URL = "https://localhost:44332/api/";
        private HttpClient client;
        private void Client_Load(object sender, EventArgs e)
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private async void btnAdd_ClickAsync(object sender, EventArgs e)
        {
            Random rnd = new Random();

            Invoice invoice = new Invoice() { IsPayed = false, RegDate = DateTime.Now, UserID = rnd.Next(1, 200), InvoiceItems = new List<InvoiceItem>() };

            for (int i = 0; i < rnd.Next(2, 10); i++)
            {
                invoice.InvoiceItems.Add(new InvoiceItem()
                {
                    Count = rnd.Next(1, 10),
                    DiscountPercentage = rnd.Next(0, 100),
                    PricePerEach = rnd.Next(10000, 9999999),
                    ProductID = rnd.Next(1, 200)
                });
            }
            try
            {
                HttpResponseMessage res = await client.PostAsJsonAsync(URL + "Invoices", invoice);
                if (res.IsSuccessStatusCode)
                {
                    MessageBox.Show("added");
                }
                else
                {
                    MessageBox.Show("error while adding");
                }
            }
            catch (Exception)
            {

                MessageBox.Show("error connecting to API");
            }

        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(URL + "Invoices/" + tbxInvoiceID.Text);

                if (response.IsSuccessStatusCode)
                {
                    Invoice Res = await response.Content.ReadAsAsync<Invoice>();
                    lblIsPayed.Text = "IsPayed : " + Res.IsPayed;
                    lblRegDate.Text = "RegDate : " + Res.RegDate;
                    lblUserID.Text = "UserID : " + Res.UserID;

                    DGV.DataSource = Res.InvoiceItems;
                    DataTable dt = new DataTable();
                    dt.Columns.Add("ProductID");
                    dt.Columns.Add("Count");
                    dt.Columns.Add("PricePerEach");
                    dt.Columns.Add("DiscountPercentage");
                    foreach (var item in Res.InvoiceItems)
                    {
                        dt.Rows.Add(new object[] {
                            item.ProductID,
                            item.Count,
                            item.PricePerEach ,
                            item.DiscountPercentage
                        });
                    }
                    DGV.DataSource = dt;
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    MessageBox.Show("No invoice Found");
                    lblIsPayed.Text = "";
                    lblRegDate.Text = "";
                    lblUserID.Text = "";
                    DGV.DataSource = null;
                }
            }
            catch (Exception)
            {

                MessageBox.Show("error connecting to API");
            }


        }
    }
}
