namespace Sehaty_Plus.Application.Feature.Specializations.Responses
{
    public record SpecializationDetailedResponse(
        int Id,
        string Name,
        string? Description,
        bool IsActive,
        string CreatedById,
        DateTime CreatedOn,
        string? UpdatedById,
        DateTime? UpdatedOn
        );
}