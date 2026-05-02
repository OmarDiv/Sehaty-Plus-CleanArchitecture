using static Dapper.SqlMapper;

namespace Sehaty_Plus.Application.Common.Types;

public class DateOnlyTypeHandler : TypeHandler<DateOnly>
{
    public override DateOnly Parse(object value)
    {
        return DateOnly.FromDateTime((DateTime)value);
    }

    public override void SetValue(System.Data.IDbDataParameter parameter, DateOnly value)
    {
        parameter.DbType = System.Data.DbType.Date;
        parameter.Value = new DateTime(value.Year, value.Month, value.Day);
    }

}
