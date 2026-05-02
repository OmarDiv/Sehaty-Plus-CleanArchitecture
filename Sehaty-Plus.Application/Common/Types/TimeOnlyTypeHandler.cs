using System.Data;
using static Dapper.SqlMapper;

namespace Sehaty_Plus.Application.Common.Types;

public class TimeOnlyTypeHandler : TypeHandler<TimeOnly>
{
    public override TimeOnly Parse(object value)
    {
        return TimeOnly.FromTimeSpan((TimeSpan)value);
    }

    public override void SetValue(IDbDataParameter parameter, TimeOnly value)
    {
        parameter.DbType = DbType.Time;
        parameter.Value = value.ToTimeSpan();
    }
}
