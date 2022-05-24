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
    public partial class Korpa : Form
    {
        DataTable tabela;
        DataTable pomocna_tabela;
        public Korpa()
        {
            InitializeComponent();
        }

        private void Load_Data()
        {
            SqlConnection veza = Konekcija.Connect();
            SqlDataAdapter pomocni_adapter = new SqlDataAdapter("SELECT * FROM Narudzbina", veza);
            pomocna_tabela = new DataTable();
            pomocni_adapter.Fill(pomocna_tabela); 
            int broj_narudzbine = Convert.ToInt32(pomocna_tabela.Rows[pomocna_tabela.Rows.Count - 1]["Broj_narudzbine"]);
            SqlDataAdapter adapter = new SqlDataAdapter("select Naziv, Cena, Kolicina from Narudzbina join Pice on Narudzbina.Pice = Pice.Id where Broj_narudzbine in(" + broj_narudzbine + ")", veza);
            tabela = new DataTable();
            adapter.Fill(tabela);
        }
        private void Korpa_Load(object sender, EventArgs e)
        {
            Load_Data();
            Ucitavanje("");
        }

        private void Ucitavanje(string vrednost)
        {
            int broj_narudzbine = Convert.ToInt32(pomocna_tabela.Rows[pomocna_tabela.Rows.Count - 1]["Broj_narudzbine"]);
            dataGridView1.Rows.Clear();
            if (tabela.Rows.Count != 0)
            {
                for (int i = 0; i < tabela.Rows.Count; i++)
                {
                    if (Convert.ToInt32(pomocna_tabela.Rows[pomocna_tabela.Rows.Count - 1 - i]["Broj_narudzbine"]) == broj_narudzbine)
                    {
                        dataGridView1.Rows.Add(tabela.Rows[i]["Naziv"].ToString(), tabela.Rows[i]["Kolicina"].ToString(), tabela.Rows[i]["Cena"].ToString());
                    }
                    else
                    {
                        break;
                    }
                }
            }
            int ukupna_cena = 0;
            if (tabela.Rows.Count != 0)
            {
                for (int i = 0; i < tabela.Rows.Count; i++)
                {
                    ukupna_cena += Convert.ToInt32(tabela.Rows[i]["Kolicina"]) * Convert.ToInt32(tabela.Rows[i]["Cena"]);
                }
            }
            label4.Text = ukupna_cena.ToString();
        }
    }
}
