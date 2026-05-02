namespace Hajj.Domain.Shared.Interfaces
{
    public interface IStringCode
    {
        public string Code { get; set; }

        public string Prefix => "";
        public int DCount => 3;
    }
}
