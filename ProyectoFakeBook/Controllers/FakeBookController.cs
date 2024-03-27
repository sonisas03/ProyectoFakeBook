using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace ProyectoFakeBook.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FakeBookController : Controller
    {
        static Dictionary<string, string> usuarios = new Dictionary<string, string>();

        [HttpPost("cadastro")]
        public IActionResult CadastrarUsuario([FromBody] UsuarioDto usuarioDto)
        {
            string usuario = usuarioDto.Usuario;
            string senha = usuarioDto.Senha;
            var resposta = new RespostaCadastro();

                if (usuarios.ContainsKey(usuario))
                {
                    resposta._respostacadastro = false;
                        return Ok(resposta);
                }

                usuarios.Add(usuario, senha);
                    resposta._respostacadastro = true;
                        return Ok(resposta);
        }
        [HttpPost("Iniciar")]
        public IActionResult IniciarUsuario([FromBody] UsuarioDto usuarioDto)
        {
            string usuario = usuarioDto.Usuario;
            string senha = usuarioDto.Senha;
            var resposta = new RespostaInicio();

            if (usuarios.TryGetValue(usuario, out string storedPassword) && senha == storedPassword)
            {
                resposta._respostainicio = true;
                return Ok(resposta);
            }
            else
            {
                resposta._respostainicio = false;
                return Ok(resposta);
            }

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