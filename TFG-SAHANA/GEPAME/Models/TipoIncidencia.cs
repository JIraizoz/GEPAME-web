using System;
using System.Collections.Generic;

namespace GEPAME.Models
{
    public partial class TipoIncidencia
    {
        public TipoIncidencia()
        {
            Incidencia = new HashSet<Incidencia>();
        }

        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Incidencia> Incidencia { get; set; }
    }
}
