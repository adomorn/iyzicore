﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace Iyzipay
{
    public class HashGeneratorV2
    {
        private HashGeneratorV2()
        {
        }

        public static String GenerateHash(String apiKey, String secretKey, String randomString, String dataToEncrypt)
        {
            HashAlgorithm algorithm = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            var computedHash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(dataToEncrypt));
            var computedHashAsHex = BitConverter.ToString(computedHash).Replace("-", "").ToLower();
            var authorizationString = "apiKey:" + apiKey + "&randomKey:" + randomString + "&signature:" + computedHashAsHex;
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(authorizationString));
        }
    }
}
