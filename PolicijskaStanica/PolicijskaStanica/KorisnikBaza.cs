using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolicijskaStanica
{
    class KorisnikBaza:Konekcija
    {
        public KorisnikBaza() : base() { }
        public  Korisnik LoginAdmin(string ime, string pass)
        {
            Korisnik korisnik = null;
            int idTema = 0;
            try
            {
                cmd = new MySqlCommand("select * from administrator where username=@username", con);
                cmd.Parameters.AddWithValue("@username", ime);
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    
                    String username = (string)reader[1];
                    String password = (string)reader[2];
                    if (username.Equals(ime) && password.Equals(pass))
                    {
                        korisnik = new Korisnik(username, true);
                        idTema= (int)reader[3];
                    }
                }
                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
            }
            if(korisnik!=null && (!idTema.Equals("")))
            {
                korisnik.setTema(new SluzbeniciBaza().getTemaByID(idTema));
            }
            return korisnik;
        }

        internal void promijeniLozinkuPolicajca(Korisnik korisnik, string lozinka)
        {
            try
            {
                cmd = new MySqlCommand("update policajac set password=@sifra where JMB=@JMB", con);
                cmd.Parameters.AddWithValue("@sifra", lozinka);
                cmd.Parameters.AddWithValue("@JMB", korisnik.getJMB());
                con.Open();
                int rez = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
        }

        internal bool PonistiPrvuPrijavu(Korisnik korisnik)
        {
            try
            {
                cmd = new MySqlCommand("update policajac set prvaPrijava='0' where JMB=@JMB", con);
                
                cmd.Parameters.AddWithValue("@JMB", korisnik.getJMB());
                con.Open();
                int rez = cmd.ExecuteNonQuery();
                con.Close();
                if (rez > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
                return false;
            }
        }

        internal bool daLiJePrvaPrijava(Korisnik korisnik)
        {
            int rezultat=1;
            try
            {
                cmd = new MySqlCommand("select prvaPrijava from policajac where username=@username", con);
                cmd.Parameters.AddWithValue("@username", korisnik.getUsername());
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                rezultat = (int)reader[0];
                con.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
            Console.WriteLine(rezultat);
            if (rezultat == 1)
                return true;
            else
                return false;
        }

        internal Korisnik loginPolicajac(string ime, string pass)
        {
            Korisnik korisnik = null;
            int idTema =0;
            try
            {
                cmd = new MySqlCommand("select * from policajac where username=@username", con);
                cmd.Parameters.AddWithValue("@username", ime);
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string jmb= (string)reader[0];
                    String username = (string)reader[4];
                    String password = (string)reader[5];
                    if (username.Equals(ime) && password.Equals(pass))
                    {
                        korisnik = new Korisnik(jmb,username, false);
                        idTema = (int)reader[6];
                    }
                }
                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
            }
            if (korisnik != null)
            {
                Console.WriteLine(">>>>>>>>>>  " + korisnik.getUsername() + "  <<<<<<<<<<<<<");
                Console.WriteLine(">>>>>>>>>>  " + idTema + "  <<<<<<<<<<<<<");
                korisnik.setTema(new SluzbeniciBaza().getTemaByID(idTema));
            }
            return korisnik;
        }
    }
}
