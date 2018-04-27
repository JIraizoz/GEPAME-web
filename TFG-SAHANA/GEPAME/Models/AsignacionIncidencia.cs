using System;
using System.Collections.Generic;

namespace GEPAME.Models
{
    public partial class AsignacionIncidencia
    {
        public string IdVehiculo { get; set; }
        public string TipoIncidencia { get; set; }
        public string IdIncidencia { get; set; }

        public Vehiculo IdVehiculoNavigation { get; set; }
        public Incidencia Incidencia { get; set; }
    }
}
