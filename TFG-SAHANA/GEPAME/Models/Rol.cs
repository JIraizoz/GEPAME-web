﻿using System;
using System.Collections.Generic;

namespace GEPAME.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Usuario> Usuario { get; set; }
    }
}
