using LS.Application.Clientes.Commands;
using LS.Application.Clientes.Dtos;
using LS.Application.Clientes.Queries;
using LS.Domain.Core.Mediator;
using LS.WebApi.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LS.WebApi.V1.Controllers
{
    [Authorize]
    [Route("api/v1/cliente")]
    public class ClienteController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IClienteQuerie _clienteQuerie;

        public ClienteController(IMediatorHandler mediatorHandler, IClienteQuerie clienteQuerie)
        {
            _mediatorHandler = mediatorHandler;
            _clienteQuerie = clienteQuerie;
        }

        [HttpGet("{clienteId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ObterPorId(Guid clienteId)
        {
            if (Guid.Empty == clienteId) return NotFound();

            var cliente = await _clienteQuerie.ObterPorId(clienteId);

            return cliente == null ? NotFound() : CustomResponse(cliente);
        }

        [HttpPut("completa-cadastro")]
        [ProducesResponseType(typeof(CompletaCadastroClienteDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> CompletaCadastroCliente(CompletaCadastroClienteDto clienteDto)
        {
            return CustomResponse(await _mediatorHandler.EnviarComando(
                                    new CompletaCadastroClienteCommand(
                                        clienteDto.ClienteId,
                                        clienteDto.DataNascimento,
                                        clienteDto.Celular,
                                        clienteDto.Profissao,
                                        clienteDto.Sexo)));
        }
    
        [HttpPost("adicionar-endereco")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AdicionarEndereco(AdicionarClienteEnderecoDto enderecoDto)
        {
            return CustomResponse(await _mediatorHandler.EnviarComando(
                                    new CadastroClienteEnderecoCommand(
                                        enderecoDto.ClienteId,
                                        enderecoDto.Logradouro,
                                        enderecoDto.Numero,
                                        enderecoDto.Complemento,
                                        enderecoDto.Bairro,
                                        enderecoDto.Cep,
                                        enderecoDto.Cidade,
                                        enderecoDto.Estado)));
        }
    }
}
