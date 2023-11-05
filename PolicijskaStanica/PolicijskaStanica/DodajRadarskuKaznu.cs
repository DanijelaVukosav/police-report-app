using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolicijskaStanica
{
    public partial class DodajRadarskuKaznu : Form
    {
        AdminPocetna pocetna;
        Korisnik korisnik = null;
        String jezik = null;
        int idKazne;
        public DodajRadarskuKaznu(AdminPocetna pocetna,Korisnik korisnik,string jezik)
        {
            this.pocetna = pocetna;
            this.korisnik = korisnik;
            this.jezik = jezik;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(jezik);
            InitializeComponent();
            postaviTemu(korisnik.getTema());
            
            dodajKaznu.Visible = true;
            izmijeniKaznu.Visible = false;
            pocetna.Enabled = false;
            
        }
        public DodajRadarskuKaznu(AdminPocetna pocetna, Korisnik korisnik, string jezik,int idKazne)
        {
            this.pocetna = pocetna;
            this.korisnik = korisnik;
            this.jezik = jezik;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(jezik);
            InitializeComponent();
            postaviTemu(korisnik.getTema());

            dodajKaznu.Visible = false;
            izmijeniKaznu.Visible = true;
            this.idKazne = idKazne;
            pocetna.Enabled = false;

            (String tablice, int prekoracenje, DateTime vrijeme) kazna = new RadarskeKazneBaza().getKaznaById(idKazne);
            registracijskeTablice.Text = kazna.tablice;
            prekoracenjeBrzine.Text = "" + kazna.prekoracenje;
            Console.WriteLine(kazna.vrijeme);
            String[] pom = kazna.vrijeme.ToString().Split('/');
            vrijemeKazne.Text = pom[1] + "-" + pom[0] + "-" + pom[2];


        }

        private void postaviTemu(Tema tema)
        {
            label1.ForeColor = Color.FromName(tema.getBojaTeksta());
            label2.ForeColor = Color.FromName(tema.getBojaTeksta());
            label3.ForeColor = Color.FromName(tema.getBojaTeksta());
            registracijskeTablice.ForeColor = Color.FromName(tema.getBojaTeksta());
            vrijemeKazne.ForeColor = Color.FromName(tema.getBojaTeksta());
            prekoracenjeBrzine.ForeColor = Color.FromName(tema.getBojaTeksta());
            dodajKaznu.ForeColor = Color.FromName(tema.getBojaTeksta());
            izmijeniKaznu.ForeColor = Color.FromName(tema.getBojaTeksta());

            dodajKaznu.BackColor = Color.FromName(tema.getBojaPozadine());
            izmijeniKaznu.BackColor = Color.FromName(tema.getBojaPozadine());

            label1.Font = new Font(tema.getFont(), 12);
            label2.Font = new Font(tema.getFont(), 12);
            label3.Font = new Font(tema.getFont(), 12);
            registracijskeTablice.Font = new Font(tema.getFont(), 12);
            vrijemeKazne.Font = new Font(tema.getFont(), 12);
            prekoracenjeBrzine.Font = new Font(tema.getFont(), 12);
            dodajKaznu.Font = new Font(tema.getFont(), 12);
            izmijeniKaznu.Font = new Font(tema.getFont(), 12);
        }

        private void DodajRadarskuKaznu_Load(object sender, EventArgs e)
        {

        }

        private void dodajKaznu_Click(object sender, EventArgs e)
        {
            try
            {
                String[] datum = vrijemeKazne.Text.Split(' ');
                String[] pomocna1 = datum[0].Split('-');
                String[] pomocna2 = datum[1].Split(':');
                int godina = int.Parse(pomocna1[2]);
                int mjesec = int.Parse(pomocna1[1]);
                int dan = int.Parse(pomocna1[1]);
                int sati = int.Parse(pomocna2[0]);
                int minuti = int.Parse(pomocna1[1]);


                DateTime formatirano = new DateTime(godina, mjesec, dan, sati, minuti, 0);

                int prekoracenje = int.Parse(prekoracenjeBrzine.Text);
                if (registracijskeTablice.Text.Equals("") || vrijemeKazne.Text.Equals(""))
                    throw new Exception();
                new RadarskeKazneBaza().DodajRadarskuKaznu(registracijskeTablice.Text, prekoracenje, formatirano, korisnik);
                this.Visible=false;
                
                pocetna.Visible = true;
                pocetna.azurirajTabeluKazni();
                pocetna.Enabled = true;
                

            }
            
            catch (Exception ex)
            {
                if (jezik.Equals("en"))
                    MessageBox.Show("Fields are invalid!");
                else
                    MessageBox.Show("Polja nisu validna!");
            }
            
            
        }

        private void izmijeniKaznu_Click(object sender, EventArgs e)
        {
            try
            {
                String[] datum = vrijemeKazne.Text.Split(' ');
                String[] pomocna1 = datum[0].Split('-');
                String[] pomocna2 = datum[1].Split(':');
                int godina = int.Parse(pomocna1[2]);
                int mjesec = int.Parse(pomocna1[1]);
                int dan = int.Parse(pomocna1[1]);
                int sati = int.Parse(pomocna2[0]);
                int minuti = int.Parse(pomocna1[1]);


                DateTime formatirano = new DateTime(godina, mjesec, dan, sati, minuti, 0);

                int prekoracenje = int.Parse(prekoracenjeBrzine.Text);
                if (registracijskeTablice.Text.Equals("") || vrijemeKazne.Text.Equals(""))
                    throw new Exception();
                new RadarskeKazneBaza().AzurirajRadarskuKaznu(idKazne,registracijskeTablice.Text, prekoracenje, formatirano);
                this.Dispose();
                pocetna.Enabled = true;
                pocetna.azurirajTabeluKazni();

            }

            catch (Exception ex)
            {
                if (jezik.Equals("en"))
                    MessageBox.Show("Fields are invalid!");
                else
                    MessageBox.Show("Polja nisu validna!");
            }


        }
    }
}
