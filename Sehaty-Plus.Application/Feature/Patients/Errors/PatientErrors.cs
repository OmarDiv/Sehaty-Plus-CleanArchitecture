namespace Sehaty_Plus.Application.Feature.Patients.Errors
{
    public static class PateintErrors
    {
        public static readonly Error InvalidNationalId = new("User.InvalidNationalId", "National ID must be exactly 14 digits.", StatusCodes.Status400BadRequest);
        public static readonly Error DuplicatedNationalId = new("User.DuplicatedNationalId", "Another User With The Same National ID Already Exsist.", StatusCodes.Status400BadRequest);
    }
}
