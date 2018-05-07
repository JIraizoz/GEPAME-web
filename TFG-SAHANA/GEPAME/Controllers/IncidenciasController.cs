using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using GEPAME.AD;
using GEPAME.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GEPAME.Controllers
{
    [Produces("application/json")]
    [Route("api/Incidencias")]
    public class IncidenciasController : Controller
    {
        // GET: api/Incidencias
        [HttpGet]
        public IEnumerable<string> Get()
        {
            List<string> incidenciaSer = new List<string>();

            var incidencias = new AD_Incidencia(new AD_GestorSqlServer("Data Source=localhost;Initial Catalog=GEPAME;Integrated Security=True").Connection).getIncidencias();

            XmlSerializer mySerializer = new XmlSerializer(typeof(GEPAME.LD.Incidencia));
            StringWriter sw = new StringWriter();

            foreach (var i in incidencias)
            {
                mySerializer.Serialize(sw, i);
                incidenciaSer.Add(sw.ToString());
                sw.Flush();
            }
            sw.Close();



            return incidenciaSer;
        }
    }
}