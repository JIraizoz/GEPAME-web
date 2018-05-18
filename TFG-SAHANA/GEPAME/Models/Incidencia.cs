using System;
using System.Collections.Generic;

namespace GEPAME.Models
{
    public partial class Incidencia
    {
        public Incidencia()
        {
            AsignacionIncidencia = new HashSet<AsignacionIncidencia>();
            Posicion = new HashSet<Posicion>();
        }

        public string TipoIncidencia { get; set; }
        public string IdIncidencia { get; set; }
        public string Utm { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        public string Descripcion { get; set; }

        public TipoIncidencia TipoIncidenciaNavigation { get; set; }
        public ICollection<AsignacionIncidencia> AsignacionIncidencia { get; set; }
        public ICollection<Posicion> Posicion { get; set; }
    }
}
