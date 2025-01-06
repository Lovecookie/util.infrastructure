using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Util.Infrastructure.Env;

public class EnvConfigurationProvider : FileConfigurationProvider
{
    public EnvConfigurationProvider(FileConfigurationSource source) : base(source)
    {
    }

    public override void Load(Stream stream)
    {
        foreach(var item in EnvReader.LoadStream(stream))
        {
            Data[item.Key] = item.Value;
        }
    }
}
