using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CKY
{
    class Production
    {
        public string Generador
        {
            get;
            set;
        }
        public string Produccion
        {
            get;
            set;
        }

        public Production(string Gen, string Prod)
        {
            Generador = Gen;
            Produccion = Prod;
        }

    }
}
