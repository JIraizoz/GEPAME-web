using System;
using System.Collections.Generic;
using System.Text;

namespace GEPAME.LD
{
    public struct TipoIncidencia
    {
        private string codigo;
        private string descripcion;

        public TipoIncidencia(string codigo, string descripcion)
        {
            this.codigo = codigo;
            this.descripcion = descripcion;
        }

        public string Codigo { get => codigo; set => codigo = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}
