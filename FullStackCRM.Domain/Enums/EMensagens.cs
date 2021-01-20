using System.ComponentModel;

namespace FullStackCRM.Domain.Enums
{
    public enum EMensagens
    {
        [Description("Operação realizada com sucesso")]
        RealizadaComSucesso = 1,
        [Description("E-mail ou senha inválidos.")]
        EmailOuSenhaInvalidos = 2
    }
}
