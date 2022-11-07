using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace Ste.Framework.Common;

public static class Utility
{
    public static Random Rand = new Random();
    public static string RandomNumber(int length)
    {
        const string chars = "123456789";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[Rand.Next(s.Length)]).ToArray());
    }

    public static int RandomNumber(int min, int max)
    {
        return Rand.Next(min, max);
    }

    public static string ToPersianNumber(string numbers)
    {
        string[] persian = { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
        for (int j = 0; j < persian.Length; j++)
            numbers = numbers.Replace(j.ToString(), persian[j]);
        return numbers;
    }

    public static string ToEnNumber(string numbers)
    {
        string[] persian = { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };
        string[] arabic = { "٠", "١", "٢", "٣", "٤", "٥", "٦", "٧", "٨", "٩" };
        for (int j = 0; j < persian.Length; j++)
        {
            numbers = numbers.Replace(persian[j], j.ToString());
            numbers = numbers.Replace(arabic[j], j.ToString());
        }
        return numbers;
    }

    public static class ConfigurationHelper
    {
        public static IConfiguration? Config;
        public static void Initialize(IConfiguration? configuration)
        {
            Config = configuration;
        }
    }

    public static string? HashPassword(string? password)
    {
        byte[] salt;
        byte[] buffer2;
        if (password == null)
        {
            return null;
        }
        using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
        {
            salt = bytes.Salt;
            buffer2 = bytes.GetBytes(0x20);
        }
        byte[] dst = new byte[0x31];
        Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
        Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
        return Convert.ToBase64String(dst);
    }

    public static bool VerifyHashedPassword(string? hashedPassword, string? password)
    {
        byte[] buffer4;
        if (hashedPassword == null)
        {
            return false;
        }
        if (password == null)
        {
            return false;
        }
        byte[] src = Convert.FromBase64String(hashedPassword);
        if ((src.Length != 0x31) || (src[0] != 0))
        {
            return false;
        }
        byte[] dst = new byte[0x10];
        Buffer.BlockCopy(src, 1, dst, 0, 0x10);
        byte[] buffer3 = new byte[0x20];
        Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
        using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
        {
            buffer4 = bytes.GetBytes(0x20);
        }
        return AreHashesEqual(buffer3, buffer4);
    }

    private static bool AreHashesEqual(byte[] firstHash, byte[] secondHash)
    {
        int minHashLength = firstHash.Length <= secondHash.Length ? firstHash.Length : secondHash.Length;
        var xor = firstHash.Length ^ secondHash.Length;
        for (int i = 0; i < minHashLength; i++)
            xor |= firstHash[i] ^ secondHash[i];
        return 0 == xor;
    }
}