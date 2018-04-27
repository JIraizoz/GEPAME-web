using System;
using System.Collections.Generic;

namespace GEPAME.Models
{
    public partial class TipoVehiculo
    {
        public TipoVehiculo()
        {
            Vehiculo = new HashSet<Vehiculo>();
        }

        public string Codigo { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Vehiculo> Vehiculo { get; set; }
    }
}
