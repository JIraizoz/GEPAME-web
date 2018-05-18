using System;
using System.Collections.Generic;

namespace GEPAME.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            AccesoVehiculo = new HashSet<AccesoVehiculo>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public int Rol { get; set; }

        public Rol RolNavigation { get; set; }
        public ICollection<AccesoVehiculo> AccesoVehiculo { get; set; }
    }
}
