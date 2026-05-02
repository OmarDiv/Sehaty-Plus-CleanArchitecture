using System.Data;
using System.Text.Json;
using static Dapper.SqlMapper;

namespace Sehaty_Plus.Application.Common.Types;

public class EnumerableIntTypeHandler : TypeHandler<IEnumerable<int>>
{
    public override IEnumerable<int> Parse(object value)
    {
        if (string.IsNullOrEmpty(value?.ToString()))
            return [];
        return JsonSerializer.Deserialize<List<int>>(value.ToString());
    }

    public override void SetValue(IDbDataParameter parameter, IEnumerable<int> value)
    {
        throw new NotImplementedException();
    }
}
