using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicijskaStanica
{
    class SaobracajneNesreceBaza:Konekcija
    {
        public SaobracajneNesreceBaza() : base() { }

        internal object listaSaobracajnihNesreca( Korisnik korisnik)
        {
            if (korisnik.isAdmin())
                cmd = new MySqlCommand("select idSaobracajnaNesreca as ID, naslov as Naslov,izvjestaj as Izvjestaj,adresa as Adresa,vrijeme as Vrijeme,CONCAT_WS(' ', ime, prezime) AS Policajac  from saobracajnanesreca inner join policajac where policajac.JMB=saobracajnanesreca.policajacJMB", con);
            else
            {
                cmd = new MySqlCommand("select idSaobracajnaNesreca as ID, naslov as Naslov,izvjestaj as Izvjestaj,adresa as Adresa,vrijeme as Vrijeme  from saobracajnanesreca where policajacJMB=@jmb", con);
                cmd.Parameters.AddWithValue("@jmb", korisnik.getJMB());
            }
            con.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            con.Close();
            return dt;
        }

        internal bool DodajNesrecu(string naslov, string adresa, string vrijeme, string izvjestaj,string jmb)
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
            

                DateTime formatirano = new DateTime(godina,mjesec,dan,sati,minuti,0);
            
                cmd = new MySqlCommand("insert into saobracajnanesreca(naslov,adresa,vrijeme,izvjestaj,policajacJMB) values(@naslov,@adresa,@vrijeme,@izvjestaj,@JMB)", con);
                cmd.Parameters.AddWithValue("@naslov", naslov);
                cmd.Parameters.AddWithValue("@adresa", adresa);
                cmd.Parameters.AddWithValue("@izvjestaj", izvjestaj);
                cmd.Parameters.AddWithValue("@vrijeme",  formatirano);
                cmd.Parameters.AddWithValue("@JMB", jmb );
                con.Open();
                int rez = cmd.ExecuteNonQuery();
                con.Close();
                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                con.Close();
                return false;
            }
        }

        internal void IzbrisiNesrecu(string ID)
        {
            if (ID.Equals(""))
                return;
            try
            {
                cmd = new MySqlCommand("delete from saobracajnanesreca  where idSaobracajnaNesreca=@id", con);
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

        internal void IzmijeniNesrecu(string id, string naslov, string adresa, string vrijeme, string izvjestaj)
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

            
                cmd = new MySqlCommand("update saobracajnanesreca set naslov=@naslov,adresa=@adresa,vrijeme=@vrijeme,izvjestaj=@izvjestaj where idSaobracajnaNesreca=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@naslov", naslov);
                cmd.Parameters.AddWithValue("@adresa", adresa);
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
                Console.WriteLine("Greska pri izmjeni!");
                con.Close();
            }
        }
    }
}
