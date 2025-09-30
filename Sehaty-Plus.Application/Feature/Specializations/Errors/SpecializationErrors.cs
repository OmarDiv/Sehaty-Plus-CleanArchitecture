namespace Sehaty_Plus.Application.Feature.Specializations.Errors;

public static class SpecializationErrors
{
    public static readonly Error SpecializationNotFound = new("Specialization.NotFound", "The specified specialization was not found.", StatusCodes.Status404NotFound);

    public static readonly Error SpecializationDuplicate = new("Specialization.Duplicate", "Specialization with the same name already exists.", StatusCodes.Status409Conflict);
    //public static readonly Error SpecializationNotFound = new("Specialization.NotFound", "The specified specialization was not found.", StatusCodes.Status404NotFound);
    //public static readonly Error SpecializationNotFound = new("Specialization.NotFound", "The specified specialization was not found.", StatusCodes.Status404NotFound);

}
