using System;
using System.Collections.Generic;
using System.Text;

namespace GEPAMECore.LD
{

    class Vehiculo
    {
        private string id;
        private string vin;
        private string matricula;
        private string anno;
        private bool desplegado;
        private bool enServicio;
        private TipoVehiculo tipoVehiculo;

        public Vehiculo(string id, string vin, string matricula, string anno, bool desplegado, bool enServicio, TipoVehiculo tipoVehiculo)
        {
            this.Id = id;
            this.Vin = vin;
            this.Matricula = matricula;
            this.Anno = anno;
            this.Desplegado = desplegado;
            this.EnServicio = enServicio;
            this.TipoVehiculo = tipoVehiculo;
        }

        public string Id { get => id; set => id = value; }
        public string Vin { get => vin; set => vin = value; }
        public string Matricula { get => matricula; set => matricula = value; }
        public string Anno { get => anno; set => anno = value; }
        public bool Desplegado { get => desplegado; set => desplegado = value; }
        public bool EnServicio { get => enServicio; set => enServicio = value; }
        internal TipoVehiculo TipoVehiculo { get => tipoVehiculo; set => tipoVehiculo = value; }
    }
}
