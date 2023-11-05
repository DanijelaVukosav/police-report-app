using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;

namespace PolicijskaStanica
{
    class Konekcija
    {
        protected string mycon;// = "server =localhost; Uid=root; password = Vukosav99; persistsecurityinfo = True; database =pozoriste; SslMode = none";

        protected MySqlConnection con;// = new MySqlConnection(mycon);
        protected MySqlCommand cmd = null;

        public Konekcija()
        {
           // mycon = "server =localhost; Uid=root; password = ; persistsecurityinfo = True; database =saobracajnapolicija; SslMode = none";
            mycon = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
            con = new MySqlConnection(mycon);
        }
    }
}
