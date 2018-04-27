using System;
using System.Collections.Generic;

namespace GEPAME.Models
{
    public partial class Posicion
    {
        public string IdVehiculo { get; set; }
        public DateTime Fecha { get; set; }
        public string Utm { get; set; }
        public string IdIncidencia { get; set; }
        public string TipoIncidencia { get; set; }

        public Vehiculo IdVehiculoNavigation { get; set; }
        public Incidencia Incidencia { get; set; }
    }
}
