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
    public partial class AdminPocetna : Form
    {
        Korisnik korisnik;
        static List<Tema> teme = new List<Tema>();

        KazneniNaloziBaza bazaNaloga = new KazneniNaloziBaza();
        SaobracajneNesreceBaza bazaNesreca = new SaobracajneNesreceBaza();
        SluzbeniciBaza bazaSluzbenika = new SluzbeniciBaza();
        RadarskeKazneBaza bazaKazni = new RadarskeKazneBaza();
        List<Panel> paneli = new List<Panel>();
        List<Control> kontrolePozadine = new List<Control>();
        List<Control> kontroleBojeTeksta = new List<Control>();
        public AdminPocetna()
        {
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("");
            InitializeComponent();
            panelPocetna.Visible = true;
            dodajKomponenteUListe();
            
            postaviTemu(korisnik.getTema());

        }

        

        public AdminPocetna(Korisnik korisnik)
        {
            this.korisnik = korisnik;
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("");
            InitializeComponent();
            sakrijPanelePrikaza();
            panelPocetna.Visible = true;
            dodajKomponenteUListe();
            
            postaviTemu(korisnik.getTema());

            if (korisnik.isAdmin())
                postavkeAdministratora();
            else
                postavkePolicajca();


        }

        private void postavkePolicajca()
        {
            panel2.Visible = false;
        }

        private void postavkeAdministratora()
        {
            dodajNesrecu.Visible = false;
            izbrisiNesrecu.Visible = false;
            izmijeniNesrecu.Visible = false;
            naslovNesrece.ReadOnly = true;
            adresaNesrece.ReadOnly = true;
            vrijemeNesrece.ReadOnly = true;
            izvjestajNesrece.ReadOnly = true;

            dodajKaznu.Visible = false;
            izmijeniKaznu.Visible = false;
            izbrisiKaznu.Visible = false;

            dodajNalog.Visible = false;
            izbrisiKazneniNalog.Visible = false;
            izmijeniKazneniNalog.Visible = false;
            ocistiFormuKaznenogNaloga.Visible = false;

            imePocionica.ReadOnly = true;
            JMBPocionica.ReadOnly = true;
            prezimePocinioca.ReadOnly = true;
            vrijemePrekrsaja.ReadOnly = true;
            izvjestajPrekrsaja.ReadOnly = true;
            idKaznenogNaloga.ReadOnly = true;


        }

        private void PocetnaClick(object sender, EventArgs e)
        {
            foreach (var panel in paneli)
            {
                panel.BackColor = Color.White;
            }
            panel1.BackColor = Color.FromName(korisnik.getTema().getBojaPozadine());
            sakrijPanelePrikaza();
            panelPocetna.Visible = true;
           
        }

        private void RadarskeKazne(object sender, EventArgs e)
        {
            foreach (var panel in paneli)
            {
                panel.BackColor = Color.White;
            }
            panel3.BackColor = Color.FromName(korisnik.getTema().getBojaPozadine());
            
            sakrijPanelePrikaza();
            panelRadarskeKazne.Visible = true;
            tabelaRadarskihKazni.DataSource = bazaKazni.listaRadarskihKazni(korisnik.getJMB());

        }

        private void SaobracajneNesrece(object sender, EventArgs e)
        {
            foreach (var panel in paneli)
            {
                panel.BackColor = Color.White;
            }
            panel4.BackColor = Color.FromName(korisnik.getTema().getBojaPozadine());
            
            sakrijPanelePrikaza();
            panelNesreca.Visible = true;
            tabelaSaobracajnihNesreca.DataSource = bazaNesreca.listaSaobracajnihNesreca(korisnik);
            //tabelaSaobracajnihNesreca.colu= false;  treba sakrit prvu kolonu
        }


        private void popuniPoljaSluzbenika(object sender, DataGridViewCellEventArgs e)
        {
            if (tabelaPolicajaca.SelectedCells.Count > 0)
            {
                textBoxJMB.Enabled = false;
                int selectedrowindex = tabelaPolicajaca.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = tabelaPolicajaca.Rows[selectedrowindex];
                textBoxJMB.Text = Convert.ToString(selectedRow.Cells["JMB"].Value);
                textBoxIme.Text= Convert.ToString(selectedRow.Cells["Ime"].Value);
                textBoxPrezime.Text= Convert.ToString(selectedRow.Cells["Prezime"].Value);
                comboBoxCin.Text= Convert.ToString(selectedRow.Cells["Cin"].Value);
                textBoxUsername.Text= Convert.ToString(selectedRow.Cells["username"].Value);
            }
        }

        private void Izmijeni_Click(object sender, EventArgs e)
        {
            if(textBoxJMB.Enabled)
            {
                MessageBox.Show("Selektujte policajca u tabeli za izmjenu!");
                return;
            }
            if (textBoxIme.Text.Equals("") || textBoxPrezime.Text.Equals("") || textBoxUsername.Text.Equals("") || comboBoxCin.SelectedItem==null)
            {
                if (comboBoxJezik.SelectedIndex == 1)
                    MessageBox.Show("Fill all fields!");
                else
                    MessageBox.Show("Polja su obavezna, pokusajte ponovo!");
                return;
            }
            string jmb = textBoxJMB.Text;
            string ime = textBoxIme.Text;
            string prezime = textBoxPrezime.Text;
            String cin = comboBoxCin.Text;
            String username = textBoxUsername.Text;
            Policajac policajac = new Policajac(jmb, ime, prezime, cin, username);
            bazaSluzbenika.azurirajPolicajaca(policajac);
            tabelaPolicajaca.DataSource = bazaSluzbenika.listaPolicajaca();


        }

        private void Izbrisi_Click(object sender, EventArgs e)
        {
            string jmb = textBoxJMB.Text;
            if(!bazaSluzbenika.izbrsiPolicajca(jmb))
                MessageBox.Show("Policajac ne moze biti izbrisan jer je potpisao odredjena dokumenta!");
            tabelaPolicajaca.Rows.RemoveAt(tabelaPolicajaca.SelectedRows[0].Index);
            tabelaPolicajaca.ClearSelection();
            textBoxJMB.Text = "";
            textBoxIme.Text = "";
            textBoxPrezime.Text = "";
            comboBoxCin.Text = "";
            textBoxUsername.Text = "";

            textBoxJMB.Text = "";

        }


        private void comboBoxJezik_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBoxJezik.SelectedIndex)
            {
                case 0:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("");
                    break;
                case 1:
                    Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en");
                    break;
            }
            this.Controls.Clear();
            InitializeComponent();
            sakrijPanelePrikaza();
            panelPocetna.Visible = true;
            dodajKomponenteUListe();

            postaviTemu(korisnik.getTema());

            if (korisnik.isAdmin())
                postavkeAdministratora();
            else
                postavkePolicajca();

            AdminPocetna_Load(null, null);

        }

        private void comboBoxTeme_SelectedIndexChanged(object sender, EventArgs e)
        {
            Tema tema = teme.ElementAt(comboBoxTeme.SelectedIndex);
            korisnik.setTema(tema);
            bazaSluzbenika.promijeniTemuSluzbeniku(tema.getID(), korisnik);
            postaviTemu(tema);
        }

        private void comboBoxPretragaKazni_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pretraga_Click(object sender, EventArgs e)
        {
            String patern = paternPretrage.Text;
            int tipPretrage = comboBoxTipPretrage.SelectedIndex==null?0: comboBoxTipPretrage.SelectedIndex;
            if (patern.Equals(""))
            {
                tabelaRadarskihKazni.DataSource = bazaKazni.listaRadarskihKazni(korisnik.getJMB());

            }
            else
            {
                try
                {
                    switch (tipPretrage)
                    {
                        case 0:
                            tabelaRadarskihKazni.DataSource = bazaKazni.pretragaPoTablicama(patern,korisnik.getJMB());
                            break;
                        case 1:
                            tabelaRadarskihKazni.DataSource = bazaKazni.pretragaPoPrekoracenju(int.Parse(patern), ">", korisnik.getJMB());
                            break;
                        case 2:
                            tabelaRadarskihKazni.DataSource = bazaKazni.pretragaPoPrekoracenju(int.Parse(patern), "<", korisnik.getJMB());
                            break;

                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    MessageBox.Show("Unijeli ste nevlidnu vrijednost paterna!");
                }
            }

        }

        private void policijskiSluzbenici(object sender, EventArgs e)
        {
            foreach (var panel in paneli)
            {
                panel.BackColor = Color.White;
            }
            panel2.BackColor = Color.FromName(korisnik.getTema().getBojaPozadine());
            
            sakrijPanelePrikaza();
            panelSluzbenika.Visible = true;
            tabelaPolicajaca.DataSource = bazaSluzbenika.listaPolicajaca();

        }
        private void sakrijPanelePrikaza()
        {
            panelPocetna.Visible = false;
            panelSluzbenika.Visible = false;
            panelRadarskeKazne.Visible = false;
            panelNesreca.Visible = false;
            panelKazneniNalozi.Visible = false;

        }

        private void prikaziKazneneNaloge(object sender, EventArgs e)
        {
            foreach (var panel in paneli)
            {
                panel.BackColor = Color.White;
            }
            panel6.BackColor = Color.FromName(korisnik.getTema().getBojaPozadine()); ;
            
            sakrijPanelePrikaza();
            panelKazneniNalozi.Visible = true;
            tabelaNaloga.DataSource = bazaNaloga.listaKaznenihNaloga(korisnik.getJMB());

        }

        private void panelNesreca_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PretraziNaloge(object sender, EventArgs e)
        {
            String patern = paternZaNalog.Text;
            int tipPretrage = comboBoxTipZaNalog.SelectedIndex == null ? 0 : comboBoxTipZaNalog.SelectedIndex;
            if (patern.Equals(""))
            {
                tabelaNaloga.DataSource = bazaNaloga.listaKaznenihNaloga(korisnik.getJMB());

            }
            else
            {
                try
                {
                    switch (tipPretrage)
                    {
                        case 0:
                            tabelaNaloga.DataSource = bazaNaloga.pretragaPoJMBPocionica(patern,korisnik.getJMB());
                            break;
                        case 1:
                            tabelaNaloga.DataSource = bazaNaloga.pretragaPoImenuPocionica(patern, korisnik.getJMB());
                            break;
                        case 2:
                            tabelaNaloga.DataSource = bazaNaloga.pretragaPoIDNaloga(int.Parse(patern), korisnik.getJMB());
                            break;
                        case 3:
                            tabelaNaloga.DataSource = bazaNaloga.pretragaPoIzvjestaju(patern, korisnik.getJMB());
                            break;
                        case 4:
                            tabelaNaloga.DataSource = bazaNaloga.pretragaPoImenuPolicajca(patern, korisnik.getJMB());
                            break;


                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Unijeli ste nevlidnu vrijednost paterna!");
                }
            }


        }

        private void panelKazneniNalozi_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelRadarskeKazne_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AdminPocetna_Load(object sender, EventArgs e)
        {
            teme=bazaSluzbenika.ucitajTeme();
            foreach(var tema in teme)
            {
                comboBoxTeme.Items.Add(tema.getNaziv());
            }
            if (!korisnik.isAdmin())
                comboBoxTipZaNalog.Items.RemoveAt(4);
            // comboBoxJezik.SelectedIndex = 0;
            ukupnoRadarskeKazne.Text += ((DataTable)bazaKazni.listaRadarskihKazni("")).Rows.Count;
            ukupnoSluzbenika.Text+= ((DataTable)bazaSluzbenika.listaPolicajaca()).Rows.Count;
            ukupnoSaobracajneNesrece.Text+= ((DataTable)bazaNesreca.listaSaobracajnihNesreca(new Korisnik("",true))).Rows.Count;
        }

        private void panelSluzbenika_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dodajNalog_Click(object sender, EventArgs e)
        {
            if (idKaznenogNaloga.Text.Equals("") || JMBPocionica.Text.Equals("") || vrijemePrekrsaja.Text.Equals("") || imePocionica.Text.Equals("") || prezimePocinioca.Text.Equals("") || izvjestajPrekrsaja.Text.Equals(""))
            {
                if (comboBoxJezik.SelectedIndex == 1)
                    MessageBox.Show("Fill all field!");
                else
                    MessageBox.Show("Polja su obavezna, pokusajte ponovo!");
                return;
            }
            bazaNaloga.DodajKazneniNalog(JMBPocionica.Text, imePocionica.Text, prezimePocinioca.Text, vrijemePrekrsaja.Text,izvjestajPrekrsaja.Text,idKaznenogNaloga.Text, korisnik.getJMB());
            tabelaNaloga.DataSource = bazaNaloga.listaKaznenihNaloga(korisnik.getJMB());
            JMBPocionica.Text = "";
            imePocionica.Text = "";
            prezimePocinioca.Text = "";
            vrijemePrekrsaja.Text = "";
            izvjestajPrekrsaja.Text = "";
            idKaznenogNaloga.Text = "";
        }

        private void textBoxIme_TextChanged(object sender, EventArgs e)
        {

        }
        public void dodajKomponenteUListe()
        {
            paneli.Add(panel1);
            paneli.Add(panel2);
            paneli.Add(panel3);
            paneli.Add(panel4);
            paneli.Add(panel5);
            paneli.Add(panel6);

            kontrolePozadine.Add(panel1);
            kontrolePozadine.Add(panel2);
            kontrolePozadine.Add(panel3);
            kontrolePozadine.Add(panel4);
            kontrolePozadine.Add(panel5);
            kontrolePozadine.Add(panel6);
            kontrolePozadine.Add(panelPocetna);
            kontrolePozadine.Add(panelKazneniNalozi);
            kontrolePozadine.Add(panelNesreca);
            kontrolePozadine.Add(panelRadarskeKazne);
            kontrolePozadine.Add(panelSluzbenika);

            kontroleBojeTeksta.Add(label1);
            kontroleBojeTeksta.Add(label2);
            kontroleBojeTeksta.Add(label3);
            kontroleBojeTeksta.Add(labelNalozi);
            kontroleBojeTeksta.Add(label4);
            kontroleBojeTeksta.Add(label5);
            kontroleBojeTeksta.Add(label9);
            kontroleBojeTeksta.Add(label10);
            kontroleBojeTeksta.Add(pretraga);
            kontroleBojeTeksta.Add(dodajKaznu);
            kontroleBojeTeksta.Add(paternPretrage);
            kontroleBojeTeksta.Add(comboBoxTipPretrage);

            kontroleBojeTeksta.Add(label11);
            kontroleBojeTeksta.Add(label12);
            kontroleBojeTeksta.Add(pretragaNaloga);
            kontroleBojeTeksta.Add(dodajNalog);
            kontroleBojeTeksta.Add(comboBoxTipZaNalog);
            kontroleBojeTeksta.Add(paternZaNalog);

            kontroleBojeTeksta.Add(ukupnoRadarskeKazne);
            kontroleBojeTeksta.Add(ukupnoSaobracajneNesrece);
            kontroleBojeTeksta.Add(ukupnoSluzbenika);
            kontroleBojeTeksta.Add(comboBoxTeme);
            kontroleBojeTeksta.Add(comboBoxJezik);

            kontroleBojeTeksta.Add(label6);
            kontroleBojeTeksta.Add(label7);
            kontroleBojeTeksta.Add(label8);
            kontroleBojeTeksta.Add(JMB);
            kontroleBojeTeksta.Add(Ime);
            kontroleBojeTeksta.Add(Prezime);
            kontroleBojeTeksta.Add(Cin);
            kontroleBojeTeksta.Add(dodajSluzbenika);
            kontroleBojeTeksta.Add(Izmijeni);
            kontroleBojeTeksta.Add(Izbrisi);
            kontroleBojeTeksta.Add(textBoxIme);
            kontroleBojeTeksta.Add(textBoxJMB);
            kontroleBojeTeksta.Add(textBoxPrezime);
            kontroleBojeTeksta.Add(comboBoxCin);
            kontroleBojeTeksta.Add(textBoxUsername);

            kontroleBojeTeksta.Add(label13);
            kontroleBojeTeksta.Add(naslovNesrece);
            kontroleBojeTeksta.Add(label14);
            kontroleBojeTeksta.Add(adresaNesrece);
            kontroleBojeTeksta.Add(label15);
            kontroleBojeTeksta.Add(vrijemeNesrece);
            kontroleBojeTeksta.Add(label16);
            kontroleBojeTeksta.Add(izvjestajNesrece);
            kontroleBojeTeksta.Add(dodajNesrecu);
            kontroleBojeTeksta.Add(izmijeniNesrecu);
            kontroleBojeTeksta.Add(izbrisiNesrecu);
            kontroleBojeTeksta.Add(OcistiPoljaNesrece);
            kontroleBojeTeksta.Add(izmijeniKaznu);
            kontroleBojeTeksta.Add(izbrisiKaznu);
            kontroleBojeTeksta.Add(ocistiFormuSluzbenika);


            kontroleBojeTeksta.Add(label17);
            kontroleBojeTeksta.Add(label18);
            kontroleBojeTeksta.Add(label19);
            kontroleBojeTeksta.Add(label20);
            kontroleBojeTeksta.Add(label21);
            kontroleBojeTeksta.Add(label22);
            kontroleBojeTeksta.Add(JMBPocionica);
            kontroleBojeTeksta.Add(imePocionica);
            kontroleBojeTeksta.Add(prezimePocinioca);
            kontroleBojeTeksta.Add(vrijemePrekrsaja);
            kontroleBojeTeksta.Add(izvjestajPrekrsaja);
            kontroleBojeTeksta.Add(idKaznenogNaloga);
            kontroleBojeTeksta.Add(ocistiFormuKaznenogNaloga);
            kontroleBojeTeksta.Add(izmijeniKazneniNalog);
            kontroleBojeTeksta.Add(izbrisiKazneniNalog);





        }




        private void postaviTemu(Tema tema)
        {
            foreach(var kontrola in kontroleBojeTeksta)
            {
                kontrola.ForeColor = Color.FromName(tema.getBojaTeksta());
                kontrola.Font = new Font(tema.getFont(), 12);
            }
            foreach (var kontrola in kontrolePozadine)
            {
                kontrola.BackColor = Color.FromName(tema.getBojaPozadine());
            }
            foreach(var panel in paneli)
            {
                panel.BackColor = Color.FromName("White");
            }
            tabelaNaloga.GridColor = Color.FromName(tema.getBojaPozadine());
            tabelaNaloga.BackgroundColor = Color.FromName(tema.getBojaPozadine());
            tabelaNaloga.ForeColor = Color.FromName(tema.getBojaTeksta());
            tabelaSaobracajnihNesreca.GridColor = Color.FromName(tema.getBojaPozadine());
            tabelaPolicajaca.GridColor = Color.FromName(tema.getBojaPozadine());
            tabelaRadarskihKazni.GridColor = Color.FromName(tema.getBojaPozadine());
           
        }

        private void popuniPoljaNesrece(object sender, DataGridViewCellEventArgs e)
        {

            if (tabelaSaobracajnihNesreca.SelectedCells.Count > 0)
            {
                int selectedrowindex = tabelaSaobracajnihNesreca.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = tabelaSaobracajnihNesreca.Rows[selectedrowindex];
                idNesrece.Text= Convert.ToString(selectedRow.Cells["ID"].Value);
                naslovNesrece.Text = Convert.ToString(selectedRow.Cells["Naslov"].Value);
                izvjestajNesrece.Text = Convert.ToString(selectedRow.Cells["Izvjestaj"].Value);
                adresaNesrece.Text = Convert.ToString(selectedRow.Cells["Adresa"].Value);
                String vrijeme = Convert.ToString(selectedRow.Cells["Vrijeme"].Value);
               
                String[] pom = vrijeme.Split('/');
                vrijemeNesrece.Text = pom[1] + "-" + pom[0] + "-" + pom[2];
               
            }

        }

        private void ocistiPoljaNesrece(object sender, EventArgs e)
        {
            naslovNesrece.Text = "";
            adresaNesrece.Text = "";
            vrijemeNesrece.Text = "";
            izvjestajNesrece.Text = "";
            idNesrece.Text = "";
        }

        private void dodajNesrecu_Click(object sender, EventArgs e)
        {
            if(naslovNesrece.Text.Equals("") || adresaNesrece.Text.Equals("") || vrijemeNesrece.Text.Equals("") || izvjestajNesrece.Text.Equals("") )
            {
                if(comboBoxJezik.SelectedIndex==1)
                     MessageBox.Show("Fill all field!");
                else
                    MessageBox.Show("Polja su obavezna, pokusajte ponovo!");
                return;
            }
            if(!bazaNesreca.DodajNesrecu(naslovNesrece.Text, adresaNesrece.Text, vrijemeNesrece.Text, izvjestajNesrece.Text,korisnik.getJMB()))
            {
                MessageBox.Show("Polja su obavezna, pokusajte ponovo!");
                return;
            }
            tabelaSaobracajnihNesreca.DataSource = bazaNesreca.listaSaobracajnihNesreca(korisnik);
            naslovNesrece.Text = "";
            adresaNesrece.Text = "";
            vrijemeNesrece.Text = "";
            izvjestajNesrece.Text = "";
            idNesrece.Text = "";
        }

        private void izbrisiNesrecu_Click(object sender, EventArgs e)
        {
            bazaNesreca.IzbrisiNesrecu(idNesrece.Text);
            tabelaSaobracajnihNesreca.DataSource = bazaNesreca.listaSaobracajnihNesreca(korisnik);
            naslovNesrece.Text = "";
            adresaNesrece.Text = "";
            vrijemeNesrece.Text = "";
            izvjestajNesrece.Text = "";
            idNesrece.Text = "";
        }

        private void izmijeniNesrecu_Click(object sender, EventArgs e)
        {
            if (naslovNesrece.Text.Equals("") || adresaNesrece.Text.Equals("") || vrijemeNesrece.Text.Equals("") || izvjestajNesrece.Text.Equals(""))
            {
                if (comboBoxJezik.SelectedIndex == 1)
                    MessageBox.Show("Fill all fields!");
                else
                    MessageBox.Show("Polja nisu validna!");
                return;
            }
            bazaNesreca.IzmijeniNesrecu(idNesrece.Text,naslovNesrece.Text,adresaNesrece.Text,vrijemeNesrece.Text,izvjestajNesrece.Text);
            tabelaSaobracajnihNesreca.DataSource = bazaNesreca.listaSaobracajnihNesreca(korisnik);
            

        }

        private void dodajKaznu_Click(object sender, EventArgs e)
        {
            String jezik = "";
           // if (comboBoxJezik.SelectedItem.Equals("Engleski"))
           //     jezik = "en";
            new DodajRadarskuKaznu(this,korisnik, jezik).Visible=true;
        }
        public void azurirajTabeluKazni()
        {
            tabelaRadarskihKazni.DataSource = bazaKazni.listaRadarskihKazni(korisnik.getJMB());
        }

        private void izmijeniKaznu_Click(object sender, EventArgs e)
        {
            String jezik = "";
            // if (comboBoxJezik.SelectedItem.Equals("Engleski"))
            //     jezik = "en";
            if (tabelaRadarskihKazni.SelectedRows.Count == 0)
                MessageBox.Show("Oznacite kaznu u tabeli za izmjenu!");

            else 
            {
                int selectedrowindex = tabelaRadarskihKazni.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = tabelaRadarskihKazni.Rows[selectedrowindex];
                int id = int.Parse(Convert.ToString(selectedRow.Cells["ID"].Value));

                new DodajRadarskuKaznu(this, korisnik, jezik, id).Visible = true;
            }
            
        }

        private void izbrisiKaznu_Click(object sender, EventArgs e)
        {
            if (tabelaRadarskihKazni.SelectedRows.Count == 0)
                MessageBox.Show("Oznacite kaznu u tabeli za brisanje!");

            else
            {
                int selectedrowindex = tabelaRadarskihKazni.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = tabelaRadarskihKazni.Rows[selectedrowindex];
                int id = int.Parse(Convert.ToString(selectedRow.Cells["ID"].Value));

                bazaKazni.IzbrisiRadarskuKaznu(id);
                azurirajTabeluKazni();
            }

        }

        private void ocistiFormuSluzbenika_Click(object sender, EventArgs e)
        {
            textBoxJMB.Text = "";
            textBoxJMB.Enabled = true;
            textBoxIme.Text = "";
            textBoxPrezime.Text = "";
            textBoxUsername.Text = "";

        }

        private void dodajSluzbenika_Click(object sender, EventArgs e)
        {
            
            if (textBoxJMB.Text.Equals("") || textBoxIme.Text.Equals("") || textBoxPrezime.Text.Equals("") || textBoxUsername.Text.Equals("") || comboBoxCin.SelectedItem == null)
            {
                if (comboBoxJezik.SelectedIndex == 1)
                    MessageBox.Show("Fill all fields!");
                else
                    MessageBox.Show("Polja su obavezna, pokusajte ponovo!");
                return;
            }
            string jmb = textBoxJMB.Text;
            string ime = textBoxIme.Text;
            string prezime = textBoxPrezime.Text;
            String cin = comboBoxCin.Text;
            String username = textBoxUsername.Text;
            Policajac policajac = new Policajac(jmb, ime, prezime, cin, username);
            int sifra=bazaSluzbenika.dodajPolicajca(policajac);
            tabelaPolicajaca.DataSource = bazaSluzbenika.listaPolicajaca();
            MessageBox.Show("Izgenerisana lozinka naloga: " + sifra);


        }

        private void prikaziPodatkeKaznenogNaloga(object sender, DataGridViewCellEventArgs e)
        {
            if (tabelaNaloga.SelectedCells.Count > 0)
            {
                int selectedrowindex = tabelaNaloga.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = tabelaNaloga.Rows[selectedrowindex];
                idKaznenogNaloga.Text = Convert.ToString(selectedRow.Cells["ID"].Value);
                JMBPocionica.Text = Convert.ToString(selectedRow.Cells["JMB"].Value);
                String[] pom= Convert.ToString(selectedRow.Cells["Pocinilac"].Value).Split(',');
                imePocionica.Text = pom[1].Trim();
                prezimePocinioca.Text = pom[0].Trim();
                String vrijeme= Convert.ToString(selectedRow.Cells["Vrijeme"].Value);
                izvjestajPrekrsaja.Text = Convert.ToString(selectedRow.Cells["Izvjestaj"].Value);

                String[] pom2 = vrijeme.Split('/');
                vrijemePrekrsaja.Text = pom2[1] + "-" + pom2[0] + "-" + pom2[2];
                idKaznenogNaloga.Enabled = false;
            }
        }

        private void ocistiFormuKaznenogNaloga_Click(object sender, EventArgs e)
        {
            JMBPocionica.Enabled = true;
            JMBPocionica.Text = "";
            imePocionica.Text = "";
            prezimePocinioca.Text = "";
            vrijemePrekrsaja.Text = "";
            izvjestajPrekrsaja.Text = "";
            idKaznenogNaloga.Text = "";
        }

        private void izmijeniKazneniNalog_Click(object sender, EventArgs e)
        {
            if (idKaznenogNaloga.Text.Equals("") || JMBPocionica.Text.Equals("") || vrijemePrekrsaja.Text.Equals("") || imePocionica.Text.Equals("") || prezimePocinioca.Text.Equals("") || izvjestajPrekrsaja.Text.Equals(""))
            {
                if (comboBoxJezik.SelectedIndex == 1)
                    MessageBox.Show("Fill all fields!");
                else
                    MessageBox.Show("Polja su obavezna, pokusajte ponovo!");
                return;
            }
            bazaNaloga.azurirajKazneniNalog(JMBPocionica.Text, imePocionica.Text, prezimePocinioca.Text, vrijemePrekrsaja.Text, izvjestajPrekrsaja.Text, idKaznenogNaloga.Text, korisnik.getJMB());
            tabelaNaloga.DataSource = bazaNaloga.listaKaznenihNaloga(korisnik.getJMB());
            
        }

        private void izbrisiKazneniNalog_Click(object sender, EventArgs e)
        {
            if (tabelaNaloga.SelectedRows.Count == 0)
                MessageBox.Show("Oznacite nalog u tabeli za brisanje!");

            bazaNaloga.izbrisiKazneniNalog(idKaznenogNaloga.Text);
            tabelaNaloga.DataSource = bazaNaloga.listaKaznenihNaloga(korisnik.getJMB());
            ocistiFormuKaznenogNaloga_Click(null, null);
        }

        private void odjavaSaSistema(object sender, EventArgs e)
        {
            this.Dispose();
            new Form1().Visible = true;
        }

        private void label18_Click(object sender, EventArgs e)
        {

        }
    }
}
