using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicijskaStanica
{
    class Policajac
    {
        String JMB;
        String Ime;
        String Prezime;
        String Cin;
        String Username;

        public Policajac()
        {
        }
        public String getIme()
        {
            return Ime;
        }
        public String getPrezime()
        {
            return Prezime;
        }
        public String getJMB()
        {
            return JMB;
        }
        public String getCin()
        {
            return Cin;
        }
        public String getUsername()
        {
            return Username;
        }


        public Policajac(string jMB, string ime, string prezime, string cin, string username)
        {
            JMB = jMB;
            Ime = ime;
            Prezime = prezime;
            this.Cin = cin;
            Username = username;
        }
    }
}
