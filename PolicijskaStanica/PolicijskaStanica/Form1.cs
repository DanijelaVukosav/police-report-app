using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolicijskaStanica
{
    public partial class Form1 : Form
    {
        KorisnikBaza bazaKorisnika = new KorisnikBaza();
        public Form1()
        {
            InitializeComponent();
            
            comboBox1.SelectedIndex=1;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            String ime = username.Text;
            String pass = password.Text;
            int index = comboBox1.SelectedIndex;
            Korisnik korisnik = null;
            if (index == 0)
            {
                korisnik = bazaKorisnika.LoginAdmin(ime, pass);
                    

            }
            else
            {
                korisnik = bazaKorisnika.loginPolicajac(ime, pass);
            }
            if (korisnik != null)
            {

                // this.Hide();
                this.Visible = false;
                if (!korisnik.isAdmin() && bazaKorisnika.daLiJePrvaPrijava(korisnik))
                {
                    this.Visible = false;
                    new PromjenaLozinkeNaloga(korisnik).Visible = true;
                    Console.WriteLine("Udjeee u uslov");
                }
                else
                    new AdminPocetna(korisnik).Visible = true;

            }
            else
            {
                MessageBox.Show("Pogresan unos! Ne postoji trazeni nalog!");
            }
        }
    }
}
