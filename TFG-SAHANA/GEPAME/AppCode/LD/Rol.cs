using System;
using System.Collections.Generic;
using System.Text;

namespace GEPAME.LD
{
    struct Rol
    {
        private int id;
        private string descripcion;

        public Rol(int id, string descripcion)
        {
            this.id = id;
            this.descripcion = descripcion;
        }

        public int Id { get => id; set => id = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}
