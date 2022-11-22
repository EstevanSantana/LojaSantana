using System.ComponentModel.DataAnnotations;

namespace LS.Domain.Clientes.Enum
{
    public enum ESexo
    {
        [Display(Name = "Não Informado")]
        NaoInformado,
        [Display(Name = "Masculino")]
        Masculino = 1,
        [Display(Name = "Feminino")]
        Feminino = 2,
    }
}
