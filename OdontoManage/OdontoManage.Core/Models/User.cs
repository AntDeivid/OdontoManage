using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace OdontoManage.Core.Models;

public class User : Base
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public byte[] Salt { get; set; }
    
    public static byte[] GenerateSalt()
    {
        var salt = new byte[16];
        RandomNumberGenerator.Fill(salt);
        return salt;
    }
    
    public static string HashPassword(string password, byte[] salt)
    {
        using var deriveBytes = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256);
        var hash = deriveBytes.GetBytes(32);
        return Convert.ToBase64String(hash);
    }
    
    public void SetPassword(string password)
    {
        Salt = GenerateSalt();
        Password = HashPassword(password, Salt);
    }
}