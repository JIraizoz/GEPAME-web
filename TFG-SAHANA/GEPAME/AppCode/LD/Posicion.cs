using System;
using System.Collections.Generic;
using System.Text;

namespace GEPAME.LD
{
    class Posicion
    {
        private Vehiculo vehiculo;
        private DateTime fecha;
        private string utm;
        private Incidencia incidencia;

        public Posicion()
        {
        }

        public Posicion(Vehiculo idVehiculo, DateTime fecha, string utm, Incidencia incidencia)
        {
            this.Vehiculo = idVehiculo;
            this.Fecha = fecha;
            this.Utm = utm;
            this.Incidencia = incidencia;
        }

        public DateTime Fecha { get => fecha; set => fecha = value; }
        public string Utm { get => utm; set => utm = value; }
        internal Vehiculo Vehiculo { get => vehiculo; set => vehiculo = value; }
        internal Incidencia Incidencia { get => incidencia; set => incidencia = value; }
    }
}
