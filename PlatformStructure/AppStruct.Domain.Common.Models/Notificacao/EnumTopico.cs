using System;
using System.Runtime.Serialization;

namespace AppStruct.Domain.Common.Models.Notificacao
{
    public enum EnumTopico : long
    {
        Indefinido = 0,
        //  *******************************
        //  *** Cadastros : 1001 : 2000 ***
        //  *******************************
        Cadastros_Solicitacao_Cadastro_Cliente = 1001,
        Cadastros_Solicitacao_Cadastro_Conta = 1002
    }
}
