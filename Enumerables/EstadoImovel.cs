using System.Runtime.Serialization;

namespace App_Imobiliaria_api.Enumerables;

public enum EstadoImovel
{
    [EnumMember(Value = "Pendente")]
    Pendente,
    [EnumMember(Value = "Cancelado")]
    Cancelado,
    [EnumMember(Value = "Publicado")]
    Publicado,
    [EnumMember(Value = "Disponível")]
    Disponivel,
    [EnumMember(Value = "Indisponível")]
    Indisponivel
}
