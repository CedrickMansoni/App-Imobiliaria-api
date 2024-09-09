using System.Runtime.Serialization;

namespace App_Imobiliaria_api.Enumerables;

public enum TipoPublicidadeImovel
{
    [EnumMember(Value = "Arrendamento")]
    Arrendamento,
    [EnumMember(Value = "Venda")]
    Venda
}
