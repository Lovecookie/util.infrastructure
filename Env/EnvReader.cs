using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Infrastructure.Env;

// reference: https://rmauro.dev/read-env-file-in-csharp/

public class EnvReader
{
    public static TOutcome<bool> Load(string filePath)
    {
        if (false == File.Exists(filePath))
        {
            return TOutcome.Error<bool>("File not found.");
        }

        foreach (var line in File.ReadAllLines(filePath))
        {
            if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
            {
                continue;
            }

            var parts = line.Split("=", 2);
            if (parts.Length != 2)
            {
                continue;
            }

            var key = parts[0].Trim();
            var value = parts[1].Trim();

            Environment.SetEnvironmentVariable(key, value);
        }

        return TOutcome.Success(true);
    }

    public static IEnumerable<KeyValuePair<string, string>> LoadStream(Stream stream)
    {
        using var reader = new StreamReader(stream);

        while (!reader.EndOfStream)
        {
            var line = reader.ReadLine();

            if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
            {
                continue;
            }

            var parts = line.Split("=", 2);
            if (parts.Length != 2)
            {
                continue;
            }

            var key = parts[0].Trim();
            var value = parts[1].Trim();

            yield return new KeyValuePair<string, string>(key, value);
        }
    }   
}
