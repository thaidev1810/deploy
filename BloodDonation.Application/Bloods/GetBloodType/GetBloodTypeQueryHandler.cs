using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Application.Abstraction.Query;
using BloodDonation.Application.Bloods.GetBloodStored;
using BloodDonation.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.Bloods.GetBloodType;

public class GetBloodTypeQueryHandler(IDbContext context)
    : IQueryHandler<GetBloodTypeQuery, Page<GetBloodTypeResponse>>
{
    public async Task<Result<Page<GetBloodTypeResponse>>> Handle(GetBloodTypeQuery request, CancellationToken cancellationToken)
    {
        var query = context.BloodTypes;
        var totalCount = await query.CountAsync(cancellationToken);

        var result = await query
            .OrderBy(x => x.Name)
            .ApplyPagination(request.PageNumber, request.PageSize)
            .Select(x => new GetBloodTypeResponse
            {
                BloodTypeId = x.BloodTypeId,
                Name = x.Name,
                Description = x.Description
            })
            .ToListAsync(cancellationToken);
        return new Page<GetBloodTypeResponse>(
            result,
            totalCount,
            request.PageNumber,
            request.PageSize);
    }

}