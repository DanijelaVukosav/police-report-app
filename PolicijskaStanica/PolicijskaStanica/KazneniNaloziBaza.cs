using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicijskaStanica
{
    class KazneniNaloziBaza:Konekcija
    {
        public KazneniNaloziBaza() : base() { }

        internal object listaKaznenihNaloga(string jmbPolicajac)
        {
            try { 
                if(jmbPolicajac.Equals(""))
                    cmd = new MySqlCommand("select idKazneniNalog as ID,pocinilacJMB as JMB,CONCAT_WS(', ', pocinilacPrezime,pocinilacIme) AS Pocinilac,izvjestaj as Izvjestaj,vrijeme as Vrijeme,CONCAT_WS(' ', ime, prezime) AS Policajac from kazneninalog inner join policajac where policajac.JMB=kazneninalog.policajacJMB", con);
                else
                {
                    cmd = new MySqlCommand("select idKazneniNalog as ID,pocinilacJMB as JMB,CONCAT_WS(', ', pocinilacPrezime,pocinilacIme) AS Pocinilac,izvjestaj as Izvjestaj,vrijeme as Vrijeme from kazneninalog where policajacJMB=@jmb", con);
                    cmd.Parameters.AddWithValue("@jmb", jmbPolicajac);
                }
                con.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                con.Close();
                return dt;
            }
            catch
            {
                con.Close();
                return new DataTable();
            }
        }

        internal object pretragaPoJMBPocionica(string patern,string jmbPolicajac)
        {
            try
            {
                if (jmbPolicajac.Equals(""))
                    cmd = new MySqlCommand("select* from(select idKazneniNalog as ID,pocinilacJMB as JMB,CONCAT_WS(' ', pocinilacIme, pocinilacPrezime) AS Pocinilac,izvjestaj as Izvjestaj,vrijeme as Vrijeme,CONCAT_WS(' ', ime, prezime) AS Policajac from kazneninalog inner join policajac where policajac.JMB=kazneninalog.policajacJMB) as tabela where JMB LIKE '%" + patern + "%'", con);
                else
                {
                    cmd = new MySqlCommand("select* from(select idKazneniNalog as ID,pocinilacJMB as JMB,CONCAT_WS(' ', pocinilacIme, pocinilacPrezime) AS Pocinilac,izvjestaj as Izvjestaj,vrijeme as Vrijeme from kazneninalog  where kazneninalog.policajacJMB=@policajac) as tabela where JMB LIKE '%" + patern + "%'", con);
                    cmd.Parameters.AddWithValue("@policajac", jmbPolicajac);
                }
                con.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                con.Close();
                return dt;
            }
            catch
            {
                con.Close();
                return new DataTable();
            }
        }

        internal object pretragaPoImenuPocionica(string patern, string jmbPolicajac)
        {
            try
            {
                if (jmbPolicajac.Equals(""))
                    cmd = new MySqlCommand("select* from(select idKazneniNalog as ID,pocinilacJMB as JMB,CONCAT_WS(' ', pocinilacIme, pocinilacPrezime) AS Pocinilac,izvjestaj as Izvjestaj,vrijeme as Vrijeme,CONCAT_WS(' ', ime, prezime) AS Policajac from kazneninalog inner join policajac where policajac.JMB=kazneninalog.policajacJMB) as tabela where Pocinilac LIKE '%" + patern + "%'", con);
                else
                {
                    cmd = new MySqlCommand("select* from(select idKazneniNalog as ID,pocinilacJMB as JMB,CONCAT_WS(' ', pocinilacIme, pocinilacPrezime) AS Pocinilac,izvjestaj as Izvjestaj,vrijeme as Vrijeme from kazneninalog  where kazneninalog.policajacJMB=@policajac) as tabela where Pocinilac LIKE '%" + patern + "%'", con);
                    cmd.Parameters.AddWithValue("@policajac", jmbPolicajac);
                }


                con.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                con.Close();
                return dt;
            }
            catch
            {
                con.Close();
                return new DataTable();
            }
        }

        internal object pretragaPoImenuPolicajca(string patern, string jmbPolicajac)
        {
            try
            {


                if (jmbPolicajac.Equals(""))
                    cmd = new MySqlCommand("select* from(select idKazneniNalog as ID,pocinilacJMB as JMB,CONCAT_WS(' ', pocinilacIme, pocinilacPrezime) AS Pocinilac,izvjestaj as Izvjestaj,vrijeme as Vrijeme,CONCAT_WS(' ', ime, prezime) AS Policajac from kazneninalog inner join policajac where policajac.JMB=kazneninalog.policajacJMB) as tabela where Policajac LIKE '%" + patern + "%'", con);
                else
                {
                    cmd = new MySqlCommand("select* from(select idKazneniNalog as ID,pocinilacJMB as JMB,CONCAT_WS(' ', pocinilacIme, pocinilacPrezime) AS Pocinilac,izvjestaj as Izvjestaj,vrijeme as Vrijeme from kazneninalog  where kazneninalog.policajacJMB=@policajac) as tabela where Policajac LIKE '%" + patern + "%'", con);
                    cmd.Parameters.AddWithValue("@policajac", jmbPolicajac);
                }

                con.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                con.Close();
                return dt;
            }
            catch
            {
                con.Close();
                return new DataTable();
            }
        }

        internal object pretragaPoIDNaloga(int id, string jmbPolicajac)
        {
            try
            {
                if (jmbPolicajac.Equals(""))
                    cmd = new MySqlCommand("select * from(select idKazneniNalog as ID,pocinilacJMB as JMB,CONCAT_WS(' ', pocinilacIme, pocinilacPrezime) AS Pocinilac,izvjestaj as Izvjestaj,vrijeme as Vrijeme,CONCAT_WS(' ', ime, prezime) AS Policajac from kazneninalog inner join policajac where policajac.JMB=kazneninalog.policajacJMB) as tabela where ID=@patern", con);
                else
                {
                    cmd = new MySqlCommand("select* from(select idKazneniNalog as ID,pocinilacJMB as JMB,CONCAT_WS(' ', pocinilacIme, pocinilacPrezime) AS Pocinilac,izvjestaj as Izvjestaj,vrijeme as Vrijeme from kazneninalog  where kazneninalog.policajacJMB=@policajac) as tabela where ID=@patern", con);
                    cmd.Parameters.AddWithValue("@policajac", jmbPolicajac);
                }

                cmd.Parameters.AddWithValue("@patern", id);
                con.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                con.Close();
                return dt;
            }
            catch
            {
                con.Close();
                return new DataTable();
            }
        }

        internal object pretragaPoIzvjestaju(string patern, string jmbPolicajac)
        {
            try
            {
                if (jmbPolicajac.Equals(""))
                    cmd = new MySqlCommand("select* from(select idKazneniNalog as ID,pocinilacJMB as JMB,CONCAT_WS(' ', pocinilacIme, pocinilacPrezime) AS Pocinilac,izvjestaj as Izvjestaj,vrijeme as Vrijeme,CONCAT_WS(' ', ime, prezime) AS Policajac from kazneninalog inner join policajac where policajac.JMB=kazneninalog.policajacJMB) as tabela where Izvjestaj LIKE '%" + patern + "%'", con);
                else
                {
                    cmd = new MySqlCommand("select* from(select idKazneniNalog as ID,pocinilacJMB as JMB,CONCAT_WS(' ', pocinilacIme, pocinilacPrezime) AS Pocinilac,izvjestaj as Izvjestaj,vrijeme as Vrijeme from kazneninalog  where kazneninalog.policajacJMB=@policajac) as tabela where Izvjestaj LIKE '%" + patern + "%'", con);
                    cmd.Parameters.AddWithValue("@policajac", jmbPolicajac);
                }

                con.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                con.Close();
                return dt;
            }
            catch
            {
                con.Close();
                return new DataTable();
            }
        }

        internal void DodajKazneniNalog(string jmbPocinioca, string ime, string prezime, string vrijeme, string izvjestaj, string id, string policajac)
        {
            try
            {

                String[] datum = vrijeme.Split(' ');
                String[] pomocna1 = datum[0].Split('-');
                String[] pomocna2 = datum[1].Split(':');
                int godina = int.Parse(pomocna1[2]);
                int mjesec = int.Parse(pomocna1[1]);
                int dan = int.Parse(pomocna1[1]);
                int sati = int.Parse(pomocna2[0]);
                int minuti = int.Parse(pomocna1[1]);


                DateTime formatirano = new DateTime(godina, mjesec, dan, sati, minuti, 0);

                cmd = new MySqlCommand("insert into kazneninalog values(@id,@JMB,@izvjestaj,@vrijeme,@ime,@prezime,@policajac)", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@JMB", jmbPocinioca);
                cmd.Parameters.AddWithValue("@ime", ime);
                cmd.Parameters.AddWithValue("@prezime", prezime);
                cmd.Parameters.AddWithValue("@vrijeme", formatirano);
                cmd.Parameters.AddWithValue("@izvjestaj", izvjestaj);
                cmd.Parameters.AddWithValue("@policajac", policajac);
                con.Open();
                int rez = cmd.ExecuteNonQuery();
                con.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                con.Close();
            }
        }

        internal void azurirajKazneniNalog(string jmbPocinioca, string ime, string prezime, string vrijeme, string izvjestaj, string id, string policajac)
        {
            try
            {

                String[] datum = vrijeme.Split(' ');
                String[] pomocna1 = datum[0].Split('-');
                String[] pomocna2 = datum[1].Split(':');
                int godina = int.Parse(pomocna1[2]);
                int mjesec = int.Parse(pomocna1[1]);
                int dan = int.Parse(pomocna1[1]);
                int sati = int.Parse(pomocna2[0]);
                int minuti = int.Parse(pomocna1[1]);


                DateTime formatirano = new DateTime(godina, mjesec, dan, sati, minuti, 0);

                cmd = new MySqlCommand("update kazneninalog set pocinilacJMB=@JMB,izvjestaj=@izvjestaj,vrijeme=@vrijeme,pocinilacIme=@ime,pocinilacPrezime=@prezime where idKazneniNalog=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@JMB", jmbPocinioca);
                cmd.Parameters.AddWithValue("@ime", ime);
                cmd.Parameters.AddWithValue("@prezime", prezime);
                cmd.Parameters.AddWithValue("@vrijeme", formatirano);
                cmd.Parameters.AddWithValue("@izvjestaj", izvjestaj);
                con.Open();
                int rez = cmd.ExecuteNonQuery();
                con.Close();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                con.Close();
            }

        }

        internal void izbrisiKazneniNalog(string ID)
        {
            if (ID.Equals(""))
                return;
            try
            {
                cmd = new MySqlCommand("delete from kazneninalog  where idKazneniNalog=@id", con);
                cmd.Parameters.AddWithValue("@id", ID);
                con.Open();
                int rez = cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception)
            {
                Console.WriteLine("Greskaaa");
                con.Close();
            }
        }
    }
}
