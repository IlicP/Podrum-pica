using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Podrum_pica
{
    public partial class Pocetna : Form
    {
        int max = 0;
        DataTable tabela;
        DataTable pomocna_tabela;
        public Pocetna()
        {
            InitializeComponent();
        }
        private void Load_Data()
        {
            SqlConnection veza = Konekcija.Connect();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Pice", veza);
            tabela = new DataTable();
            adapter.Fill(tabela);
            SqlDataAdapter pomocni_adapter = new SqlDataAdapter("SELECT * FROM Narudzbina", veza);
            pomocna_tabela = new DataTable();
            pomocni_adapter.Fill(pomocna_tabela);
        }
        public int maksimum()
        {
            int m = 0;
            SqlConnection veza = Konekcija.Connect();
            SqlCommand komanda = new SqlCommand("select max(broj_narudzbine) from Narudzbina", veza);
            SqlDataReader reader;
            veza.Open();
            reader = komanda.ExecuteReader();
            try
            {
                if (reader.Read())
                {
                    m = reader.GetInt32(0);
                }
            }
            catch(Exception)
            {
                m = 0;
            }

            veza.Close();

            return m;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Load_Data();
            Ucitavanje("");
            max = maksimum() + 1;

        }
        private void Ucitavanje(string vrednost)
        {
            dataGridView1.Rows.Clear();
            if (tabela.Rows.Count != 0)
            {
                for (int i = 0; i < tabela.Rows.Count; i++)
                {
                    if (tabela.Rows[i]["Naziv"].ToString().Contains(vrednost)) dataGridView1.Rows.Add(tabela.Rows[i]["Serijski_broj"].ToString(), tabela.Rows[i]["Naziv"].ToString(), tabela.Rows[i]["Cena"].ToString(), tabela.Rows[i]["Zemlja_proizvodnje"].ToString(), tabela.Rows[i]["Procenat_alkohola"].ToString());
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string tekst = textBox1.Text;
            Ucitavanje(tekst);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string naziv_pica = textBox1.Text;
            int kolicina = Convert.ToInt32(textBox2.Text);

            SqlConnection veza = Konekcija.Connect();
            string naredba = "insert into Narudzbina (Broj_narudzbine, Pice, Kolicina, Datum) select '" + max + "', Id, '" + kolicina + "', getdate() from Pice where Naziv = '" + naziv_pica + "'";
            SqlCommand ubaci = new SqlCommand(naredba, veza);
            veza.Open();
            ubaci.ExecuteNonQuery();
            veza.Close();
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Korpa k = new Korpa();
            k.Show();
        }

    }
}
