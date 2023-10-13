using System.Security.Cryptography;
using System.Text;
using Planner.Data.Security;

namespace Planner.Data.Validation;

public class Cryptography
{
	public static async Task<string> DecryptAsync(byte[] encrypted)
	{
		using var aes = Aes.Create();
		aes.Key = DeriveKeyFromPassword(Credentials.Password);
		aes.IV = Credentials.IV;
		using MemoryStream input = new(encrypted);
		await using CryptoStream cryptoStream = new(input, aes.CreateDecryptor(), CryptoStreamMode.Read);
		using MemoryStream output = new();
		await cryptoStream.CopyToAsync(output);
		return Encoding.Unicode.GetString(output.ToArray());
	}
	
	public static async Task<byte[]> EncryptAsync(string clearText)
	{
		using var aes = Aes.Create();
		aes.Key = DeriveKeyFromPassword(Credentials.Password);
		aes.IV = Credentials.IV;
		using MemoryStream output = new();
		await using CryptoStream cryptoStream = new(output, aes.CreateEncryptor(), CryptoStreamMode.Write);
		await cryptoStream.WriteAsync(Encoding.Unicode.GetBytes(clearText));
		await cryptoStream.FlushFinalBlockAsync();
		return output.ToArray();
	}
	
	private static byte[] DeriveKeyFromPassword(string password)
	{
		var emptySalt = Array.Empty<byte>();
		const int iterations = 1000;
		const int desiredKeyLength = 16; // 16 bytes equal 128 bits.
		var hashMethod = HashAlgorithmName.SHA384;
		return Rfc2898DeriveBytes.Pbkdf2(Encoding.Unicode.GetBytes(password),
			emptySalt,
			iterations,
			hashMethod,
			desiredKeyLength);
	}
}