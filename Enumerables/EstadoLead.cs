using System.Runtime.Serialization;

namespace App_Imobiliaria_api.Enumerables;

public enum EstadoLead
{
    [EnumMember(Value = "Pendente")]
    Pendente,
    [EnumMember(Value = "Em atendimento")]
    EmAtendimento,
    [EnumMember(Value = "Concluído")]
    Concluido,
    [EnumMember(Value = "Cancelado")]
    Cancelado
}
