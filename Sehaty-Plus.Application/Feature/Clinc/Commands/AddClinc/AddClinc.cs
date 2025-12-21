using Sehaty_Plus.Application.Common.Interfaces.Persistence;
using Sehaty_Plus.Application.Feature.Auth.Errors;
using Sehaty_Plus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sehaty_Plus.Application.Feature.Clinc.Commands.AddClinc
{
    public record AddClinicDto(
    string Name,
    string Address,
    string Area,
    string City,
    string Governorate,
    decimal? Latitude,
    decimal? Longitude,
    string? PhoneNumber,
    string? Images,
    string? Amenities,
    bool IsPrimary,
    decimal? ConsultationFee);
    public record AddClinc(string DoctorId, AddClinicDto Dto) : IRequest<Result>;
    public class AddClincHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddClinc, Result>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Result> Handle(AddClinc request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync(cancellationToken);
                var doctor = await _unitOfWork.Doctors.GetByIdAsync(request.DoctorId, cancellationToken);

                if (await _unitOfWork.Clinics.ExistsByNameAsync(request.Dto.Name, null, cancellationToken))
                {
                    return Result.Failure(UserErrors.DisabledUser);
                }
                var clinic = new Clinic
                {
                    Name = request.Dto.Name,
                    Address = request.Dto.Address,
                    Area = request.Dto.Area,
                    City = request.Dto.City,
                    Governorate = request.Dto.Governorate,
                    Latitude = request.Dto.Latitude,
                    Longitude = request.Dto.Longitude,
                    PhoneNumber = request.Dto.PhoneNumber,
                    Images = request.Dto.Images,
                    Amenities = request.Dto.Amenities,
                    IsActive = true
                };
                //// Add clinic to repository
                //await _clinicRepository.AddAsync(clinic, cancellationToken);
                //// Create DoctorClinic relationship
                //var doctorClinic = new DoctorClinic
                //{
                //    DoctorId = request.DoctorId,
                //    ClinicId = clinic.Id,
                //    IsPrimary = request.Dto.IsPrimary,
                //    ConsultationFee = request.Dto.ConsultationFee
                //};
                //// Add DoctorClinic to repository
                //await _doctorClinicRepository.AddAsync(doctorClinic, cancellationToken);
            }
            catch (Exception)
            {

                throw;
            }
            
           
            return Result.Success();
        }
    }

}
