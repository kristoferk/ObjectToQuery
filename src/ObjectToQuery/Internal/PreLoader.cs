using System;
using System.Threading.Tasks;

namespace ObjectToQuery.Internal
{
    internal class PreLoader
    {
        internal Task PreloadInt()
        {
            typeof(int).GetDefaultValue();
            return Task.FromResult(0);
        }

        internal Task PreloadString()
        {
            typeof(string).GetDefaultValue();
            return Task.FromResult(0);
        }

        internal Task PreloadDecimal()
        {
            typeof(decimal).GetDefaultValue();
            return Task.FromResult(0);
        }

        internal Task PreloadGuid()
        {
            typeof(Guid).GetDefaultValue();
            return Task.FromResult(0);
        }

        internal Task PreloadDateTime()
        {
            typeof(DateTime).GetDefaultValue();
            return Task.FromResult(0);
        }

        internal Task PreloadDateTimeOffset()
        {
            typeof(DateTimeOffset).GetDefaultValue();
            return Task.FromResult(0);
        }

        internal Task PreloadType(Type type)
        {
            type.GetPropertiesForType();
            return Task.FromResult(0);
        }
    }
}