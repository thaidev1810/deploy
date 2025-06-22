using BloodDonation.Application.Abstraction.Authentication;
using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.Users.Errors;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.Users.GetDonorInformation;

public class GetDonorInformationQueryHandler(IDbContext context) : IQueryHandler<GetDonorInformationQuery, Page<GetDonorInformationResponse>>
{
    public async Task<Result<Page<GetDonorInformationResponse>>> Handle(GetDonorInformationQuery request,
        CancellationToken cancellationToken)
    {
        // var currentUserId = userContext.UserId;
        var query = context.DonorInformation;
        var totalCount = await query.CountAsync(cancellationToken);


        var donorInfo = await context.DonorInformation
            .Include(d => d.User)
            .Select(d => new GetDonorInformationResponse
            {
                DonorInfoId = d.DonorInfoId,
                Weight = d.Weight,
                Height = d.Height,
                MedicalStatus = d.MedicalStatus,
                LastChecked = d.LastChecked,
                User = d.User == null ? null : new GetUsers
                {
                    UserId = d.User.UserId,
                    Name = d.User.Name,
                    Email = d.User.Email,
                    Phone = d.User.Phone,
                    Address = d.User.Address,
                    DateOfBirth = d.User.DateOfBirth,
                    BloodType = d.User.BloodType,
                    Gender = d.User.Gender,
                    Status = d.User.Status,
                    Role = d.User.Role
                }
            }).ToListAsync(cancellationToken);

        return new Page<GetDonorInformationResponse>(
            donorInfo,
            totalCount,
            request.PageNumber,
            request.PageSize);
    }
}