﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyecto1.Models;

namespace webApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class facultadesController : ControllerBase
    {
        //Database connection
        private readonly equiposContext _equiposContext;
        public facultadesController(equiposContext equiposContexto)
        {
            _equiposContext = equiposContexto;
        }

        //Create a new facultad 
        [HttpPost]
        [Route("AgregarFacultad")]
        public IActionResult addFacultad([FromBody] facultades facultad)
        {
            try
            {
                _equiposContext.facultades.Add(facultad);
                _equiposContext.SaveChanges();
                return Ok(facultad);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        // Read Method
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            try
            {
                List<facultades> listaFacultad = (from f in _equiposContext.facultades select f).ToList();

                if (listaFacultad.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listaFacultad);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //Update
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult updateFacultad(int id, [FromBody] facultades facultadModificar)
        {
            try
            {
                //Check if in the database exist this ID
                facultades? facultad = (from f in _equiposContext.facultades where f.facultad_id == id select f).FirstOrDefault();


                if (facultad == null) return NotFound();

                //If the ID exist, do the following:
                facultad.nombre_facultad = facultadModificar.nombre_facultad;


                _equiposContext.Entry(facultad).State = EntityState.Modified;
                _equiposContext.SaveChanges();
                return Ok(facultad);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                //Check if in the database exist this ID
                facultades? facultad = (from f in _equiposContext.facultades where f.facultad_id == id select f).FirstOrDefault();


                if (facultad == null) return NotFound();

                _equiposContext.facultades.Attach(facultad);
                _equiposContext.facultades.Remove(facultad);
                _equiposContext.SaveChanges();
                return Ok(facultad);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
