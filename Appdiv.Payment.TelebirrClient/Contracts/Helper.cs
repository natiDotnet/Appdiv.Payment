using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.Json;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.OpenSsl;

namespace Appdiv.Payment.TelebirrClient.Contracts;

public static class Helper
{
    public static JsonSerializerOptions SerializeOptions = new JsonSerializerOptions
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        WriteIndented = true
    };
    private static readonly List<string> ExcludeFields = new List<string>
    {
        "sign", "sign_type", "header", "refund_info", "openType", "raw_request"
    };

    public static string Sign(object request, string? privateKey = null)
    {
        var jsonRequest = JsonSerializer.Serialize(request, Helper.SerializeOptions);
        var requests = JsonSerializer.Deserialize<Dictionary<string, JsonElement>>(jsonRequest);
        List<string> join = [];
        foreach (var kvp in requests)
        {
            if (privateKey is not null && ExcludeFields.Contains(kvp.Key)) continue;

            if (kvp.Key == "biz_content" && kvp.Value.ValueKind == JsonValueKind.Object)
            {
                // Handle nested object (biz_content)
                foreach (var subKvp in kvp.Value.EnumerateObject())
                {
                    join.Add($"{subKvp.Name}={subKvp.Value}");
                }
            }
            else
            {
                join.Add($"{kvp.Key}={kvp.Value}");
            }
        }
        if (privateKey is null) return string.Join('&', join);

        join.Sort();
        var data = string.Join('&', join);
        return SignWithRSA(data, privateKey);
    }
    public static string SignWithRSA(string data,
    string key, string signType = "SHA256withRSA")
    {
        if (signType != "SHA256withRSA")
        {
            throw new ArgumentException("Only allowed to the type SHA256withRSA hash", nameof(signType));
        }
        if (string.IsNullOrEmpty(data))
        {
            throw new ArgumentNullException("Parameter data should not be empty");
        }
        if (string.IsNullOrEmpty(key))
        {
            throw new ArgumentNullException("Parameter key should not be empty");
        }
        try
        {
            byte[] keyBytes = Convert.FromBase64String(key);
            using (var rsa = RSA.Create())
            {
                rsa.ImportRSAPrivateKey(keyBytes, out _);

                using (var sha256 = SHA256.Create())
                {
                    byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                    byte[] hashBytes = sha256.ComputeHash(dataBytes);
                    byte[] signatureBytes = rsa.SignHash(hashBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                    return Convert.ToBase64String(signatureBytes);
                }
            }
        }
        catch (FormatException formatException)
        {
            throw new FormatException("Invalid key format", formatException);
        }
        catch (Exception ex)
        {
            throw new Exception("Error occured during signing", ex);
        }
    }

    private static string SignWithRSeA(string data, string key, string signType)
    {
        if (signType != "SHA256withRSA")
            throw new ArgumentException("Only allowed to the type SHA256withRSA hash");

        // Clean and validate the Base64 key
        //var base64 = Convert.ToBase64String(key);

        if (!IsBase64String(key))
            throw new FormatException("The provided key is not a valid Base64 string.");

        byte[] keyBytes = Convert.FromBase64String(key);
        Console.WriteLine("rrrrr");
        using (RSA rsa = RSA.Create())
        {
            rsa.FromXmlString(key);
            //rsa.ImportFromPem(key.ToCharArray());
            Console.WriteLine("piza");
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hash = sha256.ComputeHash(dataBytes);
                byte[] signature = rsa.SignHash(hash, HashAlgorithmName.SHA256, RSASignaturePadding.Pss);
                return Convert.ToBase64String(signature);
            }
        }
    }

    private static bool IsBase64String(string input)
    {
        Span<byte> buffer = new Span<byte>(new byte[input.Length]);
        return Convert.TryFromBase64String(input, buffer, out _);
    }

    public static string RandomText()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, chars.Length)
                                    .Select(s => s[Random.Shared.Next(s.Length)])
                                    .ToArray());
    }

    public static long GetCurrentTimestamp()
    {
        return DateTimeOffset.UtcNow
            .ToUnixTimeSeconds();
    }

    public static string ComputeSha256Hash(string rawData)
    {
        using SHA256 sHA = SHA256.Create();
        byte[] array = sHA.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < array.Length; i++)
        {
            stringBuilder.Append(array[i].ToString("x2"));
        }

        return stringBuilder.ToString();
    }

    public static string RsaEncryptWithPublic(string clearText, string publicKey)
    {
        publicKey = "-----BEGIN PUBLIC KEY-----\n" + publicKey + "\n-----END PUBLIC KEY-----\n";
        byte[] bytes = Encoding.UTF8.GetBytes(clearText);
        Pkcs1Encoding pkcs1Encoding = new Pkcs1Encoding(new RsaEngine());
        using (StringReader reader = new StringReader(publicKey))
        {
            AsymmetricKeyParameter parameters = (AsymmetricKeyParameter)new PemReader(reader).ReadObject();
            pkcs1Encoding.Init(forEncryption: true, parameters);
        }

        byte[] inArray;
        using (MemoryStream memoryStream = new MemoryStream(bytes))
        {
            using MemoryStream memoryStream2 = new MemoryStream();
            int num = 0;
            int num2 = bytes.Length;
            int num3 = 0;
            while (num2 - num > 0)
            {
                if (num2 - num > 117)
                {
                    byte[] array = new byte[117];
                    memoryStream.Read(array, 0, 117);
                    byte[] array2 = pkcs1Encoding.ProcessBlock(array, 0, array.Length);
                    memoryStream2.Write(array2, 0, array2.Length);
                }
                else
                {
                    byte[] array3 = new byte[num2 - num];
                    memoryStream.Read(array3, 0, num2 - num);
                    byte[] array4 = pkcs1Encoding.ProcessBlock(array3, 0, array3.Length);
                    memoryStream2.Write(array4, 0, array4.Length);
                }

                num3++;
                num = num3 * 117;
            }

            memoryStream2.Position = 0L;
            inArray = memoryStream2.ToArray();
        }

        return Convert.ToBase64String(inArray);
    }

    public static string RsaDecryptWithPublic(string base64Input, string publicKey)
    {
        publicKey = "-----BEGIN PUBLIC KEY-----\n" + publicKey + "\n-----END PUBLIC KEY-----\n";
        byte[] array = Convert.FromBase64String(base64Input);
        Pkcs1Encoding pkcs1Encoding = new Pkcs1Encoding(new RsaEngine());
        using (StringReader reader = new StringReader(publicKey))
        {
            AsymmetricKeyParameter parameters = (AsymmetricKeyParameter)new PemReader(reader).ReadObject();
            pkcs1Encoding.Init(forEncryption: false, parameters);
        }

        byte[] bytes;
        using (MemoryStream memoryStream = new MemoryStream(array))
        {
            using MemoryStream memoryStream2 = new MemoryStream();
            int num = 0;
            int num2 = array.Length;
            int num3 = 0;
            while (num2 - num > 0)
            {
                if (num2 - num > 256)
                {
                    byte[] array2 = new byte[256];
                    memoryStream.Read(array2, 0, 256);
                    byte[] array3 = pkcs1Encoding.ProcessBlock(array2, 0, array2.Length);
                    memoryStream2.Write(array3, 0, array3.Length);
                }
                else
                {
                    byte[] array4 = new byte[num2 - num];
                    memoryStream.Read(array4, 0, num2 - num);
                    byte[] array5 = pkcs1Encoding.ProcessBlock(array4, 0, array4.Length);
                    memoryStream2.Write(array5, 0, array5.Length);
                }

                num3++;
                num = num3 * 256;
            }

            memoryStream2.Position = 0L;
            bytes = memoryStream2.ToArray();
        }

        return Encoding.UTF8.GetString(bytes);
    }

}
