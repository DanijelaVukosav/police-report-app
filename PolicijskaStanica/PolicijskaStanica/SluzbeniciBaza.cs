using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PolicijskaStanica
{
    class SluzbeniciBaza:Konekcija 
    {
        public SluzbeniciBaza() : base() { }

        public DataTable listaPolicajaca()
        {
            cmd = new MySqlCommand("select JMB,Ime,Prezime,Cin,username from Policajac", con);

            con.Open();
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            con.Close();
            return dt;
        }

        internal bool azurirajPolicajaca(Policajac policajac)
        {
            try
            {
                cmd = new MySqlCommand("update policajac set Ime=@ime,Prezime=@prezime,Cin=@cin,username=@username where JMB=@JMB", con);
                cmd.Parameters.AddWithValue("@ime", policajac.getIme());
                cmd.Parameters.AddWithValue("@prezime",policajac.getPrezime());
                cmd.Parameters.AddWithValue("@cin", policajac.getCin());
                cmd.Parameters.AddWithValue("@username", policajac.getUsername());
                cmd.Parameters.AddWithValue("@JMB", policajac.getJMB());
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

        

        internal bool izbrsiPolicajca(string jmb)
        {
            try
            {
                cmd = new MySqlCommand("delete from policajac  where JMB=@JMB", con);
                cmd.Parameters.AddWithValue("@JMB", jmb);
                con.Open();
                int rez = cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                
                con.Close();
                return false;
                 
            }
        }

        internal List<Tema> ucitajTeme()
        {
            List<Tema> teme = new List<Tema>();
           
            try
            {
                cmd = new MySqlCommand("select * from tema", con);
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    teme.Add(new Tema((int)reader[0],(String)reader[1], (String)reader[4], (String)reader[2], (String)reader[3]) );
                }
                
                con.Close();

            }
            catch (Exception ex)
            {
               
                con.Close();
            }
            return teme;

        }
        internal Tema getTemaByID(int idTema)
        {
            Tema tema = null;
            try
            {
                cmd = new MySqlCommand("select * from tema where idTema=@ID", con);
                cmd.Parameters.AddWithValue("@ID", idTema);
                con.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    tema = new Tema((int)reader[0],(String)reader[1], (String)reader[4], (String)reader[2], (String)reader[3]);
                }

                con.Close();

            }
            catch (Exception ex)
            {

                con.Close();
            }
            return tema;
        }

        internal void promijeniTemuSluzbeniku(int idTeme,Korisnik korisnik)
        {
            Console.WriteLine("<<<<<<<<<<<< " + "Udje u funkciju" + ">>>>>>>>>>>>>>");
            Console.WriteLine("<<<<<<<<<<<< " + idTeme + ">>>>>>>>>>>>>>");

            try
            {
                if(korisnik.isAdmin())
                    cmd = new MySqlCommand("update administrator set idTema=@ID where username=@username", con);
                else
                    cmd = new MySqlCommand("update policajac set idTema=@ID where username=@username", con);
                cmd.Parameters.AddWithValue("@ID", idTeme);
                cmd.Parameters.AddWithValue("@username", korisnik.getUsername());
                
                con.Open();
                int rez = cmd.ExecuteNonQuery();
                Console.WriteLine("<<<<<<<<<<<< " + rez + ">>>>>>>>>>>>>>");
                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                
                con.Close();
            }
        }

        internal int dodajPolicajca(Policajac policajac)
        {
           int sifra= new Random().Next(100000, 999999);
            try
            {
                cmd = new MySqlCommand("insert into policajac(JMB,Ime,Prezime,Cin,username,password) values(@jmb,@ime,@prezime,@cin,@username,@sifra)", con);
                cmd.Parameters.AddWithValue("@ime", policajac.getIme());
                cmd.Parameters.AddWithValue("@prezime", policajac.getPrezime());
                cmd.Parameters.AddWithValue("@cin", policajac.getCin());
                cmd.Parameters.AddWithValue("@username", policajac.getUsername());
                cmd.Parameters.AddWithValue("@jmb", policajac.getJMB());
                cmd.Parameters.AddWithValue("@sifra", sifra);
                con.Open();
                int rez = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                con.Close();
            }
            return sifra;
        }
    }
}
