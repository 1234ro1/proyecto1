﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using proyecto1.Models;


namespace webApiPractica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class carerrasController : ControllerBase
    {
        private readonly equiposContext _equiposContext;
        public carerrasController(equiposContext equiposContexto)
        {
            _equiposContext = equiposContexto;
        }

        //Create a new career 
        [HttpPost]
        [Route("AddCareer")]
        public IActionResult addCareer([FromBody] carrera carerra)
        {
            try
            {
                _equiposContext.carrera.Add(carerra);
                _equiposContext.SaveChanges();
                return Ok(carerra);
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
                var listaCarrera = (from c in _equiposContext.carrera
                                    join f in _equiposContext.facultades on c.facultad_id equals f.facultad_id

                                    select new
                                    {
                                        c.carrera_id,
                                        c.nombre_carrera,
                                        c.facultad_id,
                                        f.nombre_facultad,
                                        c.estado
                                    }).ToList();

                if (listaCarrera.Count == 0)
                {
                    return NotFound();
                }
                return Ok(listaCarrera);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        //Update
        [HttpPut]
        [Route("update/{id}")]
        public IActionResult updateCareer(int id, [FromBody] carrera carreraModificar)
        {
            try
            {
                //Check if in the database exist this ID
                carrera? carreraAct = (from ca in _equiposContext.carrera where ca.carrera_id == id select ca).FirstOrDefault();


                if (carreraAct == null) return NotFound();

                //If the ID exist, do the following:
                carreraAct.nombre_carrera = carreraModificar.nombre_carrera;


                _equiposContext.Entry(carreraAct).State = EntityState.Modified;
                _equiposContext.SaveChanges();
                return Ok(carreraAct);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteCarrera(int id)
        {
            try
            {
                //Check if in the database exist this ID
                carrera? carreraBorrar = (from cb in _equiposContext.carrera where cb.carrera_id == id select cb).FirstOrDefault();


                if (carreraBorrar == null) return NotFound();

                _equiposContext.carrera.Attach(carreraBorrar);
                _equiposContext.carrera.Remove(carreraBorrar);
                _equiposContext.SaveChanges();
                return Ok(carreraBorrar);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}