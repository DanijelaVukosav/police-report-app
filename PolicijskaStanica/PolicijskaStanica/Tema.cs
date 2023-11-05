using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolicijskaStanica
{
    public class Tema
    {
        int idTema;
        String naziv;
        String font;
        String bojaPozdine;
        String bojaTeksta;

        public Tema(int idTema, string naziv, string font, string bojaPozdine, string bojaTeksta)
        {
            this.idTema = idTema;
            this.naziv = naziv;
            this.font = font;
            this.bojaPozdine = bojaPozdine;
            this.bojaTeksta = bojaTeksta;
        }
        public String getNaziv()
        {
            return naziv;
        }
        public String getFont()
        {
            return font;
        }
        public String getBojaPozadine()
        {
            return bojaPozdine;
        }
        public String getBojaTeksta()
        {
            return bojaTeksta;
        }
        public int getID()
        {
            return idTema;
        }
    }
}
