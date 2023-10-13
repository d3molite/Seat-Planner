using Planner.Data.Security;
using System;
using Microsoft.AspNetCore.Components.Forms;
using Planner.Data.Validation;

namespace Planner.Data.IO;

public class FileHandler
{
	public static async Task<MemoryStream> CreateExportFileStream(string input)
	{
		var fileStream = new MemoryStream();
		var writer = new StreamWriter(fileStream);

		var sig = GetRandomSignature();

		await writer.BaseStream.WriteAsync(sig);
		await writer.BaseStream.WriteAsync(await Cryptography.EncryptAsync(input));
		await writer.FlushAsync();
		fileStream.Position = 0;

		return fileStream;
	}

	public static bool VerifyFileExtension(IBrowserFile file)
	{
		return Credentials.Signatures.ContainsKey(GetExtension(file));
	}

	public static async Task<bool> VerifyFileHeader(IBrowserFile file)
	{
		var signature = await GetSignature(file);
		return signature is not null;
	}

	public static async Task<byte[]> GetFileContent(IBrowserFile file)
	{
		var signature = await GetSignature(file);

		await using var stream = file.OpenReadStream();
		var result = new byte[stream.Length];
		_ = await stream.ReadAsync(result, CancellationToken.None);

		return result.Skip(signature!.Length).ToArray();
	}

	private static async Task<byte[]?> GetSignature(IBrowserFile file)
	{
		var stream = file.OpenReadStream();
		var signatures = Credentials.Signatures[GetExtension(file)];

		var maxSignatureLength = signatures.Max(m => m.Length);
		var header = new byte[maxSignatureLength];
		_ = await stream.ReadAsync(header, 0, maxSignatureLength);

		return signatures.Find(sig => header.Take(sig.Length).SequenceEqual(sig));
	}

	private static byte[] GetRandomSignature()
	{
		var random = new Random().Next(0, Credentials.Signatures[".pLAN"].Count);
		return Credentials.Signatures[".pLAN"][random];
	}

	private static string GetExtension(IBrowserFile file)
	{
		return $".{file.Name.Split(".").Last()}";
	}
}