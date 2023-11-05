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
    public partial class PromjenaLozinkeNaloga : Form
    {
        Korisnik korisnik;
        public PromjenaLozinkeNaloga(Korisnik korisnik)
        {
            this.korisnik = korisnik;
            InitializeComponent();
        }

        private void PromjenaLozinkeNaloga_Load(object sender, EventArgs e)
        {
            
        }

        private void prijava_Click(object sender, EventArgs e)
        {
            String lozinka1 = textBox1.Text;
            string lozinka2 = textBox2.Text;
            if (lozinka1.Equals("") || lozinka2.Equals(""))
                MessageBox.Show("Lozinke nisu unesene, pokusajte ponovo");
            else if (!lozinka1.Equals(lozinka2))
                MessageBox.Show("Lozinke nisu iste, pokusajte ponovo");
            else if(new KorisnikBaza().PonistiPrvuPrijavu(korisnik))
            {
                new KorisnikBaza().promijeniLozinkuPolicajca(korisnik, lozinka1);
                new AdminPocetna(korisnik).Visible = true;
            }

        }
    }
}
