﻿namespace KBZLifeInsuranceCodeTest.Shared.Services.SecurityServices;

public class AesService
{
    private readonly IConfiguration _configuration;

    public AesService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Encrypt(string raw)
    {
        byte[] iv;
        byte[] encrypted;

        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(_configuration.GetSection("EncryptionKey").Value!);
            aes.GenerateIV();
            iv = aes.IV;
            ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (
                    CryptoStream cryptoStream = new CryptoStream(
                        memoryStream,
                        encryptor,
                        CryptoStreamMode.Write
                    )
                )
                {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(raw);
                    }
                    encrypted = memoryStream.ToArray();
                }
            }
            byte[] result = new byte[iv.Length + encrypted.Length];
            Array.Copy(iv, 0, result, 0, iv.Length);
            Array.Copy(encrypted, 0, result, iv.Length, encrypted.Length);
            return Convert.ToBase64String(result);
        }
    }

    public string Decrypt(string encryptedText)
    {
        byte[] fullCipher = Convert.FromBase64String(encryptedText);
        byte[] iv = new byte[16];
        byte[] cipherText = new byte[fullCipher.Length - iv.Length];
        Array.Copy(fullCipher, 0, iv, 0, iv.Length);
        Array.Copy(fullCipher, iv.Length, cipherText, 0, cipherText.Length);

        using Aes aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(_configuration.GetSection("EncryptionKey").Value!);
        aes.IV = iv;
        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
        using MemoryStream memoryStream = new MemoryStream(cipherText);
        using CryptoStream cryptoStream = new CryptoStream(
            memoryStream,
            decryptor,
            CryptoStreamMode.Read
        );
        using StreamReader streamReader = new StreamReader(cryptoStream);
        return streamReader.ReadToEnd();
    }
}
