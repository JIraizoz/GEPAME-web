using System;
using System.Collections.Generic;

namespace GEPAME.Models
{
    public partial class AccesoVehiculo
    {
        public string IdVehiculo { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }

        public Usuario IdUsuarioNavigation { get; set; }
        public Vehiculo IdVehiculoNavigation { get; set; }
    }
}
