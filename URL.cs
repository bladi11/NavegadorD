using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavegadorD
{
    class URL
    {
        private string direccion;
        private int noVisitadas;
        private DateTime ultimoAcceso;

        public string Direcccion { get => direccion; set => direccion = value; }

        public int NoVisitadas { get => noVisitadas; set => noVisitadas = value; }

        public DateTime UltimoAcceso { get => ultimoAcceso; set => ultimoAcceso = value; }
    }
}
