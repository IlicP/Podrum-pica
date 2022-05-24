using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Podrum_pica
{
    public partial class Login_forma : Form
    {
        public DataTable tabela;
        public Login_forma()
        {
            InitializeComponent();
        }
        private void Load_Data()
        {
            SqlConnection veza = Konekcija.Connect();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Osoba", veza);
            tabela = new DataTable();
            adapter.Fill(tabela);
        }

        private void Login_forma_Load(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection veza = Konekcija.Connect();
            string imejl_korisnika = textBox1.Text;
            string pasvord = textBox2.Text;
            int flag = 0;
            label3.Text = "";
            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                if (tabela.Rows[i]["Imejl"].ToString() == imejl_korisnika)
                {
                    flag++;
                    if (tabela.Rows[i]["Lozinka"].ToString() == pasvord)
                    {
                        if (tabela.Rows[i]["Administrator"].ToString() == "t")
                        {
                            string ubacivanje = "insert into Stara_logovanja values ('" + imejl_korisnika + "', getdate(), 't')";
                            SqlCommand comm = new SqlCommand(ubacivanje, veza);
                            veza.Open();
                            comm.ExecuteNonQuery();
                            veza.Close();
                            this.Hide();
                            Forma_admin f = new Forma_admin();
                            f.Show();
                        }
                        else
                        {
                            string ubacivanje = "insert into Stara_logovanja values ('" + imejl_korisnika + "', getdate(), 'f')";
                            SqlCommand comm = new SqlCommand(ubacivanje, veza);
                            veza.Open();
                            comm.ExecuteNonQuery();
                            veza.Close();
                            this.Hide();
                            Pocetna f1 = new Pocetna();
                            f1.Show();
                        }
                    }
                    else
                    {
                        label3.Text = "Ponovo unesite lozinku";
                        break;
                    }
                }
            }
            if (flag == 0)
            {
                label3.Text = "Ponovo unesite imejl";
            }
        }
    }
}
