using LS.Domain.Clientes.Enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace LS.Application.Clientes.Dtos
{
    public class CompletaCadastroClienteDto
    {
        public Guid ClienteId { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataNascimento { get; set; }
        public string Celular { get; set; }
        public string Profissao { get; set; }
        public ESexo Sexo { get; set; }
    }
}
