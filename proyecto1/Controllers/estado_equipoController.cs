﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyecto1.Models;

namespace webApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class estados_equipoController : ControllerBase
    {
        //Database connection
        private readonly equiposContext _equiposContext;
        public estados_equipoController(equiposContext equiposContext)
        {
            _equiposContext = equiposContext;
        }

        //Create Method
        [HttpPost]
        [Route("AddState")]
        public IActionResult AddState([FromBody] estado_equipo stateEquip)
        {
            try
            {
                _equiposContext.estado_equipos.Add(stateEquip);
                _equiposContext.SaveChanges();
                return Ok(stateEquip);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        //Read Mehtod
        [HttpGet]
        [Route("GetAllState")]
        public ActionResult Get()
        {
            try
            {
                List<estado_equipo> stateEquip = (from sq in _equiposContext.estado_equipos select sq).ToList();

                if (stateEquip.Count == 0)
                {
                    return NotFound();
                }
                return Ok(stateEquip);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //Update
        [HttpPut]
        [Route("upadateState/{id}")]
        public IActionResult updateState(int id, [FromBody] estado_equipo stateEquipModificar)
        {
            try
            {
                estado_equipo? estados = (from se in _equiposContext.estado_equipos where se.id_estados_equipo == id select se).FirstOrDefault();

                if (estados == null) return NotFound();

                estados.descripcion = stateEquipModificar.descripcion;
                estados.estado = stateEquipModificar.estado;

                _equiposContext.Entry(estados).State = EntityState.Modified;
                _equiposContext.SaveChanges();
                return Ok(estados);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //Delete
        [HttpDelete]
        [Route("deleteState/{id}")]
        public IActionResult deleteState(int id)
        {
            try
            {
                estado_equipo? estados = (from se in _equiposContext.estado_equipos where se.id_estados_equipo == id select se).FirstOrDefault();

                if (estados == null) return NotFound();

                _equiposContext.estado_equipos.Attach(estados);
                _equiposContext.estado_equipos.Remove(estados);
                _equiposContext.SaveChanges();
                return Ok(estados);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

    }
}