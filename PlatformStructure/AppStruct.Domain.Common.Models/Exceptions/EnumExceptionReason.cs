using System;
using System.Runtime.Serialization;

namespace AppStruct.Domain.Common.Models.Exceptions
{
    public enum EnumExceptionReason : long
    {
        Success = 0,
        //  ***************************
        //  *** Genéricas : 1 : 200 ***
        //  ***************************
        Genericos_Informacoes_Invalidas = 1,

        //  *******************************
        //  *** Parâmetros : 901 : 1000 ***
        //  *******************************
        Parametros_Parametro_Nao_Encontrado = 901,
        Parametros_Endpoint_Nao_Encontrado = 902,
        Parametros_Endpoint_Formato_Invalido = 903,
        //  *******************************
        //  *** Cadastros : 1001 : 2000 ***
        //  *******************************
        Cadastros_Cliente_Nao_Encontrada = 1001,
        Cadastros_Conta_Nao_Encontrada = 1002
    }
}
