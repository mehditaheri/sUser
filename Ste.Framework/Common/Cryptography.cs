using System.Security.Cryptography;
using System.Text;
using static Ste.Framework.Common.Utility;

namespace Ste.Framework.Common;

public class SymmetricEncryption
{
    public static string Encrypt(string message, string settingName)
    {
        using var aesAlg = Aes.Create();
        aesAlg.Key = Convert.FromBase64String(ConfigurationHelper.Config.GetSection($"{settingName}:key").Value);
        aesAlg.IV = Convert.FromBase64String(ConfigurationHelper.Config.GetSection($"{settingName}:iv").Value);

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using var ms = new MemoryStream();
        using var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
        using (var sw = new StreamWriter(cs))
        {
            sw.Write(message); // Write all data to the stream.
        }
        return Convert.ToBase64String(ms.ToArray());
    }

    public static string Decrypt(string cipherText, string settingName)
    {
        using var aesAlg = Aes.Create();
        aesAlg.Key = Convert.FromBase64String(ConfigurationHelper.Config.GetSection($"{settingName}:key").Value);
        aesAlg.IV = Convert.FromBase64String(ConfigurationHelper.Config.GetSection($"{settingName}:iv").Value);

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var ms = new MemoryStream(Convert.FromBase64String(cipherText));
        using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
        using var sr = new StreamReader(cs);
        return sr.ReadToEnd();
    }

    public static void Test()
    {
        Console.WriteLine("***** Symmetric encryption demo *****");

        var unencryptedMessage = "To be or not to be, that is the question, whether tis nobler in the...";
        Console.WriteLine("Unencrypted message: " + unencryptedMessage);

        // 1. Create a key (shared key between sender and reciever).
        //byte[] key, iv;
        //using (Aes aesAlg = Aes.Create())
        //{
        //    key = aesAlg.Key;
        //    iv = aesAlg.IV;
        //}

        // 2. Sender: Encrypt message using key
        var encryptedMessage = SymmetricEncryption.Encrypt(unencryptedMessage, "TokenAes");
        Console.WriteLine("Sending encrypted message: " + encryptedMessage);

        // 3. Receiver: Decrypt message using same key
        var decryptedMessage = SymmetricEncryption.Decrypt(encryptedMessage, "TokenAes");
        Console.WriteLine("Recieved and decrypted message: " + decryptedMessage);

        Console.Write(Environment.NewLine);
    }
}

public static class AsymmetricEncryption
{
    public static byte[] Encrypt(string message, RSAParameters rsaParameters)
    {
        using var rsaAlg = RSA.Create(rsaParameters);
        return rsaAlg.Encrypt(Encoding.UTF8.GetBytes(message), RSAEncryptionPadding.Pkcs1);
    }

    public static string Decrypt(byte[] cipherText, RSAParameters rsaParameters)
    {
        using var rsaAlg = RSA.Create(rsaParameters);
        var decryptedMessage = rsaAlg.Decrypt(cipherText, RSAEncryptionPadding.Pkcs1);
        return Encoding.UTF8.GetString(decryptedMessage);
    }

    public static void Ttest()
    {
        Console.WriteLine("***** Asymmetric encryption demo *****");

        var unencryptedMessage = "To be or not to be, that is the question, whether tis nobler in the...";
        Console.WriteLine("Unencrypted message: " + unencryptedMessage);

        // 1. Create a public / private key pair.
        RSAParameters privateAndPublicKeys, publicKeyOnly;
        using (var rsaAlg = RSA.Create())
        {
            privateAndPublicKeys = rsaAlg.ExportParameters(includePrivateParameters: true);
            publicKeyOnly = rsaAlg.ExportParameters(includePrivateParameters: false);
        }

        // 2. Sender: Encrypt message using public key
        var encryptedMessage = AsymmetricEncryption.Encrypt(unencryptedMessage, publicKeyOnly);
        Console.WriteLine("Sending encrypted message: " + encryptedMessage);

        // 3. Receiver: Decrypt message using private key
        var decryptedMessage = AsymmetricEncryption.Decrypt(encryptedMessage, privateAndPublicKeys);
        Console.WriteLine("Recieved and decrypted message: " + decryptedMessage);

        Console.Write(Environment.NewLine);
    }
}

public static class MessageSignature
{
    public static byte[] Sign(string message, RSAParameters rsaParameters)
    {
        using var rsaAlg = RSA.Create(rsaParameters);
        return rsaAlg.SignData(Encoding.UTF8.GetBytes(message), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }

    public static bool Verify(string message, byte[] signature, RSAParameters rsaParameters)
    {
        using var rsaAlg = RSA.Create(rsaParameters);
        return rsaAlg.VerifyData(Encoding.UTF8.GetBytes(message), signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }

    public static void Test()
    {
        Console.WriteLine("***** Message signature demo *****");

        var message = "To be or not to be, that is the question, whether tis nobler in the...";
        Console.WriteLine("Message to be verified: " + message);

        // 1. Create a public / private key pair.
        RSAParameters privateAndPublicKeys, publicKeyOnly;
        using (var rsaAlg = RSA.Create())
        {
            privateAndPublicKeys = rsaAlg.ExportParameters(includePrivateParameters: true);
            publicKeyOnly = rsaAlg.ExportParameters(includePrivateParameters: false);
        }

        // 2. Sender: Sign message using private key
        var signature = MessageSignature.Sign(message, privateAndPublicKeys);
        Console.WriteLine("Message signature: " + signature);

        // 3. Receiver: Verify message authenticity using public key
        var isTampered = MessageSignature.Verify(message, signature, publicKeyOnly);
        Console.WriteLine("Message is untampered: " + isTampered.ToString());

        Console.Write(Environment.NewLine);
    }
}