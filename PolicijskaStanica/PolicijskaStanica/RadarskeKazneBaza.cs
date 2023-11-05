using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicijskaStanica
{
    class RadarskeKazneBaza : Konekcija
    {
        public RadarskeKazneBaza() : base() { }

        internal object listaRadarskihKazni(string jmbPolicajac)
        {
            if (jmbPolicajac.Equals(""))
                cmd = new MySqlCommand("select idRadarskaKazna as ID,registracijskeTablice as Tablice,prekoracenjeBrzine as Prekoracenje,novcanaKazna as Kazna,vrijemePreksaja as Vrijeme,CONCAT_WS(' ', ime, prezime) AS Policajac from radarskakazna inner join policajac where policajac.JMB=radarskaKazna.policajacJMB", con);
            else
            {
                cmd = new MySqlCommand("select idRadarskaKazna as ID,registracijskeTablice as Tablice,prekoracenjeBrzine as Prekoracenje,novcanaKazna as Kazna,vrijemePreksaja as Vrijeme from radarskakazna  where policajacJMB=@jmb", con);
                cmd.Parameters.AddWithValue("@jmb", jmbPolicajac);
            }
            con.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            con.Close();
            return dt;
        }

        internal object pretragaPoPrekoracenju(int patern, string znak,string jmbPolicajac)
        {
            Console.WriteLine("patern je " + patern);
            if(znak.Equals(">"))
            {
                if (jmbPolicajac.Equals(""))
                    cmd = new MySqlCommand("select idRadarskaKazna as ID,registracijskeTablice as Tablice,prekoracenjeBrzine as Prekoracenje,novcanaKazna as Kazna,vrijemePreksaja as Vrijeme from radarskakazna  where prekoracenjeBrzine > @patern ", con);
                else
                {
                    cmd = new MySqlCommand("select idRadarskaKazna as ID,registracijskeTablice as Tablice,prekoracenjeBrzine as Prekoracenje,novcanaKazna as Kazna,vrijemePreksaja as Vrijeme from radarskakazna  where policajacJMB=@jmb and prekoracenjeBrzine > @patern ", con);
                    cmd.Parameters.AddWithValue("@jmb", jmbPolicajac);
                }
            }
            else
            {
                if (jmbPolicajac.Equals(""))
                    cmd = new MySqlCommand("select idRadarskaKazna as ID,registracijskeTablice as Tablice,prekoracenjeBrzine as Prekoracenje,novcanaKazna as Kazna,vrijemePreksaja as Vrijeme from radarskakazna  where prekoracenjeBrzine < @patern ", con);
                else
                {
                    cmd = new MySqlCommand("select idRadarskaKazna as ID,registracijskeTablice as Tablice,prekoracenjeBrzine as Prekoracenje,novcanaKazna as Kazna,vrijemePreksaja as Vrijeme from radarskakazna  where policajacJMB=@jmb and prekoracenjeBrzine < @patern ", con);
                    cmd.Parameters.AddWithValue("@jmb", jmbPolicajac);
                }
            }
            
            cmd.Parameters.AddWithValue("@patern", patern);
            con.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            con.Close();
            return dt;
        }

        internal object pretragaPoTablicama(string patern,string jmbPolicajac)
        {
            if (jmbPolicajac.Equals(""))
                cmd = new MySqlCommand("select idRadarskaKazna as ID,registracijskeTablice as Tablice,prekoracenjeBrzine as Prekoracenje,novcanaKazna as Kazna,vrijemePreksaja as Vrijeme from radarskakazna  where registracijskeTablice LIKE '%" + patern + "%'", con);
            else
            {
                cmd = new MySqlCommand("select idRadarskaKazna as ID,registracijskeTablice as Tablice,prekoracenjeBrzine as Prekoracenje,novcanaKazna as Kazna,vrijemePreksaja as Vrijeme from radarskakazna  where policajacJMB=@jmb and registracijskeTablice LIKE '%" + patern + "%' ", con);
                cmd.Parameters.AddWithValue("@jmb", jmbPolicajac);
            }

            
           // cmd.Parameters.AddWithValue("@patern", patern);
            con.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            con.Close();
            return dt;
            
        }

        internal void DodajRadarskuKaznu(string tablice, int prekoracenje, DateTime vrijeme, Korisnik korisnik)
        {
            try
            {

                
                cmd = new MySqlCommand("insert into radarskakazna(registracijskeTablice,prekoracenjeBrzine,vrijemePreksaja,policajacJMB) values(@tablice,@prekoracenje,@vrijeme,@JMB)", con);
                cmd.Parameters.AddWithValue("@tablice", tablice);
                cmd.Parameters.AddWithValue("@prekoracenje", prekoracenje);
                cmd.Parameters.AddWithValue("@vrijeme", vrijeme);
                cmd.Parameters.AddWithValue("@JMB", korisnik.getJMB());
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

        internal (string tablice, int prekoracenje, DateTime vrijeme) getKaznaById(int idKazne)
        {
            String tablice=null;
            int prekoracenje=-1;
            DateTime vrijeme =new DateTime();
            try
            {

                cmd = new MySqlCommand("select * from radarskakazna where idRadarskaKazna=@ID", con);
                cmd.Parameters.AddWithValue("@ID", idKazne);
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tablice = (string)reader[1];
                    prekoracenje = (int)reader[2];
                    vrijeme = (DateTime)reader[4];
                    
                }
                con.Close();
                

            }
            catch (Exception ex)
            {
                Console.WriteLine("Greskaaa");

                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);

                con.Close();
            }
            return (tablice,prekoracenje,vrijeme);
        }

        internal void AzurirajRadarskuKaznu(int idKazne, string tablice, int prekoracenje, DateTime vrijeme)
        {
            try
            {


                cmd = new MySqlCommand("update radarskakazna set registracijskeTablice=@tablice,prekoracenjeBrzine=@prekoracenje,vrijemePreksaja=@vrijeme where idRadarskaKazna=@id", con);
                cmd.Parameters.AddWithValue("@tablice", tablice);
                cmd.Parameters.AddWithValue("@prekoracenje", prekoracenje);
                cmd.Parameters.AddWithValue("@vrijeme", vrijeme);
                cmd.Parameters.AddWithValue("@id", idKazne);
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

        internal void IzbrisiRadarskuKaznu(int id)
        {
            try
            {
                cmd = new MySqlCommand("delete from radarskakazna  where idRadarskaKazna=@id", con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                int rez = cmd.ExecuteNonQuery();
                con.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                con.Close();
            }
        }
    }
}
