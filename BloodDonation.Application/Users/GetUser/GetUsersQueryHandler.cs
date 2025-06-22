using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Application.Abstraction.Query;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.Users.GetUser;

public sealed class GetUsersQueryHandler(IDbContext context)
    : IQueryHandler<GetUsersQuery, Page<GetUsersResponse>>
{
    public async Task<Result<Page<GetUsersResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var query = context.Users
            .Where(u => u.Role != UserRole.Admin);

        var totalCount = await query.CountAsync(cancellationToken);

        var result = await query
            .ApplyPagination(request.PageNumber, request.PageSize)
            .Select(u => new GetUsersResponse
            {
                Id = u.UserId,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role,
                Gender = u.Gender,
                Status = u.Status,
                BloodType = u.BloodType,
                DateOfBirth = u.DateOfBirth,
                Address = u.Address,
                Phone = u.Phone,
                IsDonor = u.IsDonor
            })
            .ToListAsync(cancellationToken);

        return new Page<GetUsersResponse>(
            result,
            totalCount,
            request.PageNumber,
            request.PageSize);
    }
}