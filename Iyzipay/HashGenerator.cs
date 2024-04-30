using System;
using System.Security.Cryptography;
using System.Text;

namespace Iyzicore;

public static class HashGenerator
{
    public static string GenerateHash(string apiKey, string secretKey, string randomString, BaseRequest request)
    {
        var hashStr = apiKey + randomString + secretKey + request.ToPKIRequestString();
        var computeHash = SHA1.HashData(Encoding.UTF8.GetBytes(hashStr));
        return Convert.ToBase64String(computeHash);
    }
}