using System.Data;
using System.Text.Json;
using static Dapper.SqlMapper;

namespace Sehaty_Plus.Application.Common.Types;

public class AnyTypeHandler<T> : TypeHandler<T>
{
    public override T Parse(object value)
    {
        if (string.IsNullOrEmpty(value?.ToString()))
            return default;
        return JsonSerializer.Deserialize<T>(value.ToString()!)!;
    }

    public override void SetValue(IDbDataParameter parameter, T value)
    {
        parameter.Value = JsonSerializer.Serialize(value);
    }
}
