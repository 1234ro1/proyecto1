using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proyecto1.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Extensions.Hosting;

namespace proyecto1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class equiposController : ControllerBase
    {
        private readonly equiposContext equiposContext;
    
        public equiposController(equiposContext equiposContexto) 
        {
               equiposContext = equiposContexto;
        }
        [HttpGet]
        [Route("GetAll")]

        public IActionResult Get()
        {

            List<equipos> listadoEquipo = (from e in equiposContext.equipos
                                           select e).ToList();
            if (listadoEquipo.Count == 0)
            {
                return NotFound();
            }
            return Ok(listadoEquipo);

        }
        [HttpGet]
        [Route("GetByID/{id}")]
        public IActionResult Get(int id)
        {
            equipos? equipos = (from e in equiposContext.equipos
                               where e.id_equipos ==id
                               select e).FirstOrDefault();
            if(equipos == null) 
            {
                return NotFound();
            }
            return Ok(equipos);
        }
        [HttpGet]
        [Route("Find/{filtro}")]
        public IActionResult FindByDescripcion(string filtro)
        {
            equipos? equipos = (from e in equiposContext.equipos
                                where e.descripcion.Contains(filtro) 
                                select e).FirstOrDefault();
            if (equipos == null)
            {
                return NotFound();
            }
            return Ok(equipos);
        }
        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarEquipo([FromBody]equipos equipo)
        {
            try 
            { 
              equiposContext.equipos.Add(equipo);
                equiposContext.SaveChanges();
                return Ok();

            }
            catch (Exception ex) 
            {
              return BadRequest(ex.Message);
            }

        }
        [HttpGet]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarEquipo(int id, [FromBody]equipos equipoModificar)
        {
            equipos? equipoActual = (from e in equiposContext.equipos
                                where e.id_equipos == id
                                select e).FirstOrDefault();
            if (equipoActual == null)
            {
                return NotFound();
            }
            equipoActual.nombre=equipoModificar.nombre;
            equipoActual.descripcion = equipoModificar.descripcion;
            equipoActual.marca_id = equipoModificar.marca_id;
            equipoActual.tipo_equipo_id = equipoModificar.tipo_equipo_id;
            equipoActual.anio_compra = equipoModificar.anio_compra;
            equipoActual.costo = equipoModificar.costo;

            equiposContext.Entry(equipoActual).State = EntityState.Modified;
            equiposContext.SaveChanges();
            return Ok(equipoModificar);
        }
        [HttpDelete]
        [Route("eliminar/{filtro}")]
        public IActionResult EliminarEquipo(int id)
        {
            equipos? equipos = (from e in equiposContext.equipos
                                where e.id_equipos == id
                                select e).FirstOrDefault();
            if (equipos == null)
            {
                return NotFound();
            }
            equiposContext.equipos.Attach(equipos);
            equiposContext.equipos.Remove(equipos);
            equiposContext.SaveChanges();
            return Ok(equipos);
        }
    }
    
}
