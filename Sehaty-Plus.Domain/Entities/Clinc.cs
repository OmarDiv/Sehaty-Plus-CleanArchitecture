namespace Sehaty_Plus.Domain.Entities;
public class Clinic 
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Area { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Governorate { get; set; } = string.Empty;
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Images { get; set; } 
    public string? Amenities { get; set; } // هنا لو في مميزات ف المكان زي وياي فاي او الكلام ده خليك فاكر 
    public bool IsActive { get; set; } = true;
    public ICollection<DoctorClinic> DoctorClinics { get; set; } = [];
}
