using System;
using System.Collections.Generic;
using System.Text;

namespace GEPAME.LD
{
    class Usuario
    {
        private int id;
        private string username;
        private string nombre;
        private string apellidos;
        private string telefono;
        private Rol tipoUsuario;

        public Usuario(int id, string username, string nombre, string apellidos, string telefono, Rol tipoUsuario)
        {
            this.Id = id;
            this.Username = username;
            this.Nombre = nombre;
            this.Apellidos = apellidos;
            this.Telefono = telefono;
            this.TipoUsuario = tipoUsuario;
        }

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Telefono { get => telefono; set => telefono = value; }
        internal Rol TipoUsuario { get => tipoUsuario; set => tipoUsuario = value; }
    }
}
