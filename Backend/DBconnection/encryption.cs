using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32.SafeHandles;

namespace Backend.DBconnection;

public class encryption
{
    public static string encryptor(string word) //encrypt string with SHA256 encryption
    {
        using var hash = SHA256.Create();
        var byteArray = hash.ComputeHash(Encoding.UTF8.GetBytes(word));
        return Convert.ToHexString(byteArray);
    }
}