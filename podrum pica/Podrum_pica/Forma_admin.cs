using System;
using System.Collections.Generic;
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
    public partial class Forma_admin : Form
    {
        new DataTable tabela;
        new DataTable pomocna_tabela;
        new DataTable mesecna_tabela;

        public Forma_admin()
        {
            InitializeComponent();
        }
        private void Load_Data()
        {
            SqlConnection veza = Konekcija.Connect();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Osoba", veza);
            tabela = new DataTable();
            adapter.Fill(tabela);
            SqlDataAdapter pomocni_adapter = new SqlDataAdapter("SELECT * FROM Stara_logovanja", veza);
            pomocna_tabela = new DataTable();
            pomocni_adapter.Fill(pomocna_tabela);
            SqlDataAdapter pomocni_adapter2 = new SqlDataAdapter("select Datum_popisa, Ime_prezime, Razlika_u_ceni from Mesecni_popis join Osoba on Mesecni_popis.Popis_obavio = Osoba.Id", veza);
            mesecna_tabela = new DataTable();
            pomocni_adapter2.Fill(mesecna_tabela);
        }
        private void Ucitavanje()
        {
            dataGridView1.Rows.Clear();
            if (tabela.Rows.Count != 0)
            {
                for (int i = mesecna_tabela.Rows.Count - 1; i > -1; i--)
                {
                    dataGridView1.Rows.Add(mesecna_tabela.Rows[i]["Datum_popisa"].ToString(), mesecna_tabela.Rows[i]["Ime_prezime"].ToString(), mesecna_tabela.Rows[i]["Razlika_u_ceni"].ToString());
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string razlika = textBox1.Text;
            string admin = pomocna_tabela.Rows[pomocna_tabela.Rows.Count - 1]["Ulogovani_imejl"].ToString();
            SqlConnection veza = Konekcija.Connect();
            string naredba = "insert into Mesecni_popis(Popis_obavio, Datum_popisa, Razlika_u_ceni) select Id, getdate()," + razlika + " from Osoba where Imejl = '" + admin + "' ";
            SqlCommand unesi_popis = new SqlCommand(naredba, veza);
            veza.Open();
            unesi_popis.ExecuteNonQuery();
            veza.Close();
            Load_Data();
            Ucitavanje();
        }

        private void Forma_admin_Load_1(object sender, EventArgs e)
        {
            Load_Data();
            Ucitavanje();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Pocetna f = new Pocetna();
            f.Show();
        }
    }
}
