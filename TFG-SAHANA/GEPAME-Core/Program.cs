using GEPAMECore.AD;
using System;
using GEPAMECore.LD;
using GEPAMECore.LN;

namespace GEPAME_Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TipoVehiculo tipoVehiculo = new TipoVehiculo("AM0001", "");
            Vehiculo v = new Vehiculo("2","DEF0123456789","1234AAA","2018",false,true,tipoVehiculo);

            //new AD_Posicion(new AD_GestorSqlServer("Data Source=localhost;Initial Catalog=GEPAME;Integrated Security=True").Connection).GetUltimaPosicion("6969JDT");
            LN_Server.Server();

            Console.ReadKey();
        }
    }
}
