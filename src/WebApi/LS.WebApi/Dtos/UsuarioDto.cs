using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LS.WebApi.Dtos
{
    public class UsuarioLoginDto
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha { get; set; }
    }

    public class UsuarioTokenDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimDto> Claims { get; set; }
    }

    public class LoginResponseDto
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UsuarioTokenDto UserToken { get; set; }
    }

    public class ClaimDto
    {
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
