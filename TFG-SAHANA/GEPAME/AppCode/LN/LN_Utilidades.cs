using GEPAME.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace GEPAME.AppCode.LN
{
    public class LN_Utilidades
    {
        public string GetPosicion()
        {
            string posicion = "";



            return posicion;
        }

        public string GetPosicionIncidencia(GEPAMEContext context)
        {
            string posicion = "";

            // Query for all blogs with names starting with B 
            if (context.Incidencia.Count() > 0)
            {
                var incidencias = from b in context.Incidencia
                                  select b;
                posicion = incidencias.First().Utm;
            }


            return posicion;
        }

        public string GetSiguienteIDIncidencia(GEPAMEContext context, string tipoIncidencia)
        {
            string id = "1";

            if (context.Incidencia.Count() > 0)
            {
                var identificadores = (from i in context.Incidencia
                                       where i.TipoIncidencia == tipoIncidencia
                                       select i).Last().IdIncidencia;

                id = (int.Parse(identificadores) + 1).ToString();
            }

            return id;
        }
    }
}
