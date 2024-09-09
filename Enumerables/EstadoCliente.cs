using System.Runtime.Serialization;

namespace App_Imobiliaria_api.Enumerables;

public enum EstadoCliente
{
    [EnumMember(Value = "Activa")]
    Activa,
    [EnumMember(Value = "Cancelada")]
    Cancelada
}