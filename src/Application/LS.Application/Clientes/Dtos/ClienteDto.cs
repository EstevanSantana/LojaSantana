using System;
using System.ComponentModel.DataAnnotations;

namespace LS.Application.Clientes.Dtos
{
    public class ClienteDto
    {
        public Guid ClienteId { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public string Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Celular { get; set; }
        public string Profissao { get; set; }
        public string Sexo { get; set; }
    }
}
