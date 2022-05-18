using System;
using System.Threading.Tasks;
using BlogPessoal.src.dtos;
using BlogPessoal.src.modelos;
using BlogPessoal.src.servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPessoal.src.controladores
{
   [ApiController]
   [Route("api/Autenticacao")]
   [Produces("application/json")]
   public class AutenticacaoControlador : ControllerBase
   {
       #region Atributos
       private readonly IAutenticacao _servicos;

       #endregion

       #region Construtores
       public AutenticacaoControlador(IAutenticacao servicos)
       {
           _servicos = servicos;
       }

       #endregion

       #region Metodos

        /// <summary>
        /// Autenticar usuário
        /// </summary>
        /// <param name="autenticacao">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Valida a autenticação</response>
        /// <response code="400">Nega a autenticação</response>   
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UsuarioModelo))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
       [HttpPost]
       [AllowAnonymous]
       public async Task <ActionResult> AutenticarAsync([FromBody] AutenticarDTO autenticacao)
       {
           if(!ModelState.IsValid) return BadRequest();
           
           try
           {
               var autorizacao = await _servicos.PegarAutorizacaoAsync(autenticacao);
               return Ok(autorizacao);
           }
           catch (Exception ex)
           {
                return Unauthorized(ex.Message);
           }
       }

       #endregion

   }

}