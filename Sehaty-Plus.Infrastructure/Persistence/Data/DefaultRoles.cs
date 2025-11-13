namespace Sehaty_Plus.Infrastructure.Persistence.Data
{
    public static class DefaultRoles
    {
        public partial class Admin
        {
            public const string Id = "019a72b4-22b5-752d-99a9-70ba20a93b20";
            public const string Name = nameof(Admin);
            public const string ConcurrencyStamp = "019a72b4-22b5-752d-99a9-70bdfa2b942c";

        }
        public partial class Member
        {
            public const string Id = "019a72b4-22b6-7d48-ae78-8572c7a26b07";
            public const string Name = nameof(Member);
            public const string ConcurrencyStamp = "019a72b4-22b6-7d48-ae78-8573b711cae0";

        }
        public partial class Doctor
        {
            public const string Id = "019a72b4-22b5-752d-99a9-70bbe5beed4c";
            public const string Name = nameof(Doctor);
            public const string ConcurrencyStamp = "019a72b4-22b5-752d-99a9-70beb6aceeca";
        }
        public partial class Patient
        {
            public const string Id = "019a72b4-22b5-752d-99a9-70bc36061c72";
            public const string Name = nameof(Patient);
            public const string ConcurrencyStamp = "019a72b4-22b5-752d-99a9-70bf3e8cc322";
        }
    }
}
