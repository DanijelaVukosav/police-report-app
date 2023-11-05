using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicijskaStanica
{
    public class Korisnik
    {
        String JMB;
        String username;
        Tema tema;
        bool admin;

        public Korisnik(string jmb,string username, Tema tema,bool isAdmin)
        {
            JMB = jmb;
            this.username = username;
            this.tema = tema;
            admin = isAdmin;
        }
        public Korisnik(string jmb,string username, bool isAdmin)
        {
            JMB = jmb;
            this.username = username;
            admin = isAdmin;
        }
        public Korisnik(string username, bool isAdmin)
        {
            JMB = "";
            this.username = username;
            admin = isAdmin;
        }
        public  String getUsername()
        {
            return username;
        }
        public Tema getTema()
        {
            return tema;
        }
        public bool isAdmin()
        {
            return admin;
        }
        public void setTema(Tema tema)
        {
            this.tema = tema;
        }
        public string getJMB()
        {
            return JMB;
        }
    }
}
