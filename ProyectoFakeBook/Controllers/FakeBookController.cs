using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProyectoFakeBook.Models;

namespace ProyectoFakeBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FakeBookController : Controller
    {
        private FakeBookDBContext BaseDeDatos;
        public FakeBookController(FakeBookDBContext _BaseDeDatos) {
            this.BaseDeDatos = _BaseDeDatos;
        }


        [HttpPost("cadastro")]
        public IActionResult CadastrarUsuario([FromBody] Usuario usuario)
        {
            var resposta = new RespostaCadastro();
            var UsuarioExistente = BaseDeDatos.Usuarios.Where(p => p.Usuario1 == usuario.Usuario1).ToList();
            if (UsuarioExistente.Count() > 0)
            {
                resposta._respostacadastro = false;
            }
            else
            {
                resposta._respostacadastro = true;
                BaseDeDatos.Add(usuario);
                BaseDeDatos.SaveChanges();
            }
            return Ok(resposta);
        }
            [HttpPost("Iniciar")]
        public IActionResult IniciarUsuario([FromBody] Usuario usuario)
        {
         
            var resposta = new RespostaInicio();

            var ConferirUsuario = BaseDeDatos.Usuarios.FirstOrDefault(p => p.Usuario1 == usuario.Usuario1 && p.Senha == usuario.Senha);


                if (ConferirUsuario != null)
            {
                resposta._respostainicio = true;
            }
                else {
                resposta._respostainicio = false;
            }
                return Ok(resposta);
        }
        [HttpGet("usuarios")]
        public IActionResult ObterBase()
        {
            var usuarios = BaseDeDatos.Usuarios.ToList();
            return Ok(usuarios);
        }
        [HttpPost("eliminar")]
        public IActionResult EliminarUsuario([FromBody] Usuario usuario)
        {
            var usuarioAEliminar = BaseDeDatos.Usuarios.FirstOrDefault(u => u.Usuario1 == usuario.Usuario1);
            if (usuarioAEliminar == null)
            {
                return NotFound(); 
            }

            BaseDeDatos.Usuarios.Remove(usuarioAEliminar);
            BaseDeDatos.SaveChanges();

            return Ok();
        }


    }
    public class RespostaInicio
    {
        public bool _respostainicio { get; set; }
    }
    public class RespostaCadastro
    {
        public bool _respostacadastro { get; set; }
    }
    public class UsuarioDto
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
    }
    










}