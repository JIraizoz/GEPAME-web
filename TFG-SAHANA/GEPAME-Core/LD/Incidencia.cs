using System;
using System.Collections.Generic;
using System.Text;

namespace GEPAMECore.LD
{
    class Incidencia
    {
        private TipoIncidencia tipo;
        private string id;
        private string utm;
        private DateTime fecha;
        private bool estado;
        private string descripcion;

        public Incidencia() { }

        public Incidencia(TipoIncidencia tipo, string id, string utm, DateTime fecha, bool estado, string descripcion)
        {
            this.tipo = tipo;
            this.id = id ?? throw new ArgumentNullException(nameof(id));
            this.utm = utm ?? throw new ArgumentNullException(nameof(utm));
            this.fecha = fecha;
            this.estado = estado;
            this.descripcion = descripcion ?? throw new ArgumentNullException(nameof(descripcion));
        }

        public string Id { get => id; set => id = value; }
        public string Utm { get => utm; set => utm = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public bool Estado { get => estado; set => estado = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
        internal TipoIncidencia Tipo { get => tipo; set => tipo = value; }
    }
}
