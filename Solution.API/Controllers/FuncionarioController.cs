﻿using Microsoft.Ajax.Utilities;
using Solution.BS;
using Solution.DAL;
using Solution.API.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Solution.API.Controllers
{
    [RoutePrefix("api/Funcionario")]
    public class FuncionarioController : ApiController
    {
        //Instaciar una clase funcianario desde BS para poder acceder a los metodos del CRUD
        clsFuncionario clsF = new clsFuncionario();


        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAllFuncionarios()
        {
            ICollection<ConsultarFuncionarioResult> funcionarios = clsF.ConsultarFuncionario().ToList();

            if (funcionarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(funcionarios);
        }


        [HttpGet]
        [Route("{IdFuncionario:int}")]
        public IHttpActionResult GetFuncionario(int IdFuncionario)
        {
            ICollection<ConsultaFuncionarioResult> funcionarios = clsF.ConsultaFuncionario(IdFuncionario);

            if (funcionarios.Count == 0)
            {
                return NotFound();
            }

            return Ok(funcionarios.FirstOrDefault());
        }


        [HttpPost]
        [Route("")]
        public IHttpActionResult PostFuncionario(Funcionario funcionario)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");


            bool result = clsF.CrearFuncionario(
                    funcionario.IdOficina,
                    funcionario.NombreCompleto,
                    funcionario.Usuario,
                    funcionario.Contrasena,
                    funcionario.CorreoElectronico);

            //TODO: Validar que el correo no existe previamente.
            //TODO: Validacion de que el usuario es unico.
            //TODo: Encriptado de la contraseña.

            if (!result)
            {
                return BadRequest("Unable to create new");
            }
            return Ok(funcionario);
        }


        [HttpPut]
        [Route("")]
        public IHttpActionResult PutFuncionario(Funcionario funcionario)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            bool result = clsF.ActualizarFuncionario(
                     funcionario.IdFuncionario,
                     funcionario.IdOficina,
                     funcionario.NombreCompleto,
                     funcionario.Usuario,
                     funcionario.Contrasena,
                     funcionario.CorreoElectronico);

            if (!result)
            {
                return BadRequest("Unable to update new");
            }
            return Ok(funcionario);
        }


        [HttpDelete]
        [Route("{IdFuncionario:int}")]
        public IHttpActionResult DeleteFuncionario(int IdFuncionario)
        {
            if (IdFuncionario <= 0)
                return BadRequest("Not a valid student id");

            bool result = clsF.EliminarFuncionario(IdFuncionario);
            if (!result)
            {
                return BadRequest("Unable to delete");
            }
            return Ok();
        }
    }

}
