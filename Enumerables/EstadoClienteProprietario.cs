using System.Runtime.Serialization;

namespace App_Imobiliaria_api.Enumerables;

public enum EstadoClienteProprietario
{
    [EnumMember(Value = "Activa")]
    Activa,
    [EnumMember(Value = "Cancelada")]
    Cancelada
}
