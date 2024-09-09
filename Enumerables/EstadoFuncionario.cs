using System.Runtime.Serialization;

namespace App_Imobiliaria_api.Enumerables;

public enum EstadoFuncionario
{
    [EnumMember(Value = "Activo")]
    Activo,
    [EnumMember(Value = "Inactivo")]
    Inactivo
}
