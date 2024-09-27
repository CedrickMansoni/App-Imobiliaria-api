using System;
using System.Security.Cryptography;
using System.Text;

namespace App_Imobiliaria_api.Repository.Hash;

public class HashPassword
{
    public string CriptografarSenha(string senha)
      {
        var encodedValue = Encoding.UTF8.GetBytes(senha);
        var encryptedPassword = SHA512.Create().ComputeHash(encodedValue);

        var sb = new StringBuilder();
        foreach (var caracter in encryptedPassword)
        {
            sb.Append(caracter.ToString("X2"));
        }
        return sb.ToString();
      }
}