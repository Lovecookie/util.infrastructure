



namespace Util.Infrastructure.Crypt;


public class AesEncryption
{
	private static readonly byte[] _key = [
		0xc6,
		0xc6,
		0x4e,
		0x5f,
		0x5a,
		0x01,
		0xbd,
		0xcf,
		0x24,
		0x44,
		0x6f,
		0xe9,
		0x5d,
		0x64,
		0x0f,
		0xee,
		0x95,
		0xee,
		0x3c,
		0x63,
		0xc6,
		0x21,
		0x26,
		0xdb,
		0xb3,
		0x8f,
		0x36,
		0x02,
		0xbb,
		0xb0,
		0x53,
		0x9e
	];


	private static readonly byte[] _iv = [
		0x5b,
		0x74,
		0xdb,
		0x57,
		0x1a,
		0x13,
		0x3c,
		0xcf,
		0x1f,
		0xc0,
		0x61,
		0xb5,
		0x70,
		0x34,
		0x02,
		0x53
	];

	public static byte[] Encrypt(string plainText, byte[]? key, byte[]? iv)
	{
		using Aes aesAlg = Aes.Create();
		aesAlg.Key = key ?? _key;
		aesAlg.IV = iv ?? _iv;

		ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

		using MemoryStream msEncrypt = new MemoryStream();
		using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
		using StreamWriter swEncrypt = new StreamWriter(csEncrypt, Encoding.UTF8);

		swEncrypt.Write(plainText);

		return msEncrypt.ToArray();
	}

	public static async Task<byte[]> EncryptAsync(string plainText, byte[]? key = null, byte[]? iv = null)
	{
		using Aes aesAlg = Aes.Create();
		aesAlg.Key = key ?? _key;
		aesAlg.IV = iv ?? _iv;

		ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

		using MemoryStream msEncrypt = new MemoryStream();
		using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
		using StreamWriter swEncrypt = new StreamWriter(csEncrypt, Encoding.UTF8);

		await swEncrypt.WriteAsync(plainText);
		await swEncrypt.FlushAsync();

		return msEncrypt.ToArray();
	}

	public static async Task<string> EncryptAsString(string plainText, byte[]? key = null, byte[]? iv = null)
	{
		using Aes aesAlg = Aes.Create();
		aesAlg.Key = key ?? _key;
		aesAlg.IV = iv ?? _iv;

		ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

		using (MemoryStream msEncrypt = new MemoryStream())
		{
			using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
			{
				using (StreamWriter swEncrypt = new StreamWriter(csEncrypt, Encoding.UTF8))
				{
					await swEncrypt.WriteAsync(plainText);
					await swEncrypt.FlushAsync();
				}
			}

			return Convert.ToBase64String(msEncrypt.ToArray());
		}
	}

	public static string Decrypt(byte[] cipherText, byte[]? key = null, byte[]? iv = null)
	{
		using Aes aesAlg = Aes.Create();
		aesAlg.Key = key ?? _key;
		aesAlg.IV = iv ?? _iv;

		ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

		using (MemoryStream msDecrypt = new MemoryStream(cipherText))
		{
			using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
			{
				using (StreamReader srDecrypt = new StreamReader(csDecrypt, Encoding.UTF8))
				{
					return srDecrypt.ReadToEnd();
				}
			}
		}
	}

	public static async Task<string> DecryptAsync(byte[] cipherText, byte[]? key = null, byte[]? iv = null)
	{
		using Aes aesAlg = Aes.Create();
		aesAlg.Key = key ?? _key;
		aesAlg.IV = iv ?? _iv;

		ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

		using (MemoryStream msDecrypt = new MemoryStream(cipherText))
		{
			using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
			{
				using (StreamReader srDecrypt = new StreamReader(csDecrypt, Encoding.UTF8))
				{
					return await srDecrypt.ReadToEndAsync();
				}
			}
		}
	}

	public static async Task<string> DecryptAsString(string cipherText, byte[]? key = null, byte[]? iv = null)
	{
		byte[] convertedText = Convert.FromBase64String(cipherText);

		using Aes aesAlg = Aes.Create();
		aesAlg.Key = key ?? _key;
		aesAlg.IV = iv ?? _iv;

		ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

		using (MemoryStream msDecrypt = new MemoryStream(convertedText))
		{
			using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
			{
				using (StreamReader srDecrypt = new StreamReader(csDecrypt, Encoding.UTF8))
				{
					return await srDecrypt.ReadToEndAsync();
				}
			}
		}
	}

	public static async Task<Int64?> DecryptAsInt64(string cipherText, byte[]? key = null, byte[]? iv = null)
	{
		byte[] convertedText = Convert.FromBase64String(cipherText);

		using Aes aesAlg = Aes.Create();
		aesAlg.Key = key ?? _key;
		aesAlg.IV = iv ?? _iv;

		ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

		using (MemoryStream msDecrypt = new MemoryStream(convertedText))
		{
			using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
			{
				using (StreamReader srDecrypt = new StreamReader(csDecrypt, Encoding.UTF8))
				{
					var readedUxt = await srDecrypt.ReadToEndAsync();
					if (!Int64.TryParse(readedUxt, out var uxt))
					{
						return null;
					}

					return uxt;
				}
			}
		}
	}
}
