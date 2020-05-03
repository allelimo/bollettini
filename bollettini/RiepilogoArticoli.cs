using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bollettini
{
    class RiepilogoArticoli
    {
        public string Prezzo { get; set; }
        public string Tipo { get; set; }
        public string Descrizione { get; set; }

        public RiepilogoArticoli(string prezzo, string tipo, string descrizione)
        {
            Prezzo = prezzo;
            Tipo = tipo;
            Descrizione = descrizione;

        }
    }
}
