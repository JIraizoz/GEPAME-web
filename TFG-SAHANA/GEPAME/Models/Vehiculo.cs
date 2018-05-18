using System;
using System.Collections.Generic;

namespace GEPAME.Models
{
    public partial class Vehiculo
    {
        public Vehiculo()
        {
            AccesoVehiculo = new HashSet<AccesoVehiculo>();
            AsignacionIncidencia = new HashSet<AsignacionIncidencia>();
            Posicion = new HashSet<Posicion>();
        }

        public string Id { get; set; }
        public string Vin { get; set; }
        public string Matricula { get; set; }
        public int? Anyo { get; set; }
        public bool Desplegado { get; set; }
        public bool EnServicio { get; set; }
        public string TipoVehiculo { get; set; }

        public TipoVehiculo TipoVehiculoNavigation { get; set; }
        public ICollection<AccesoVehiculo> AccesoVehiculo { get; set; }
        public ICollection<AsignacionIncidencia> AsignacionIncidencia { get; set; }
        public ICollection<Posicion> Posicion { get; set; }
    }
}
