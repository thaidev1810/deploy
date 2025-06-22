using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Application.Abstraction.Query;
using BloodDonation.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.Bloods.GetBloodStored;

public class GetBloodStoredQueryHandler(IDbContext context)
    : IQueryHandler<GetBloodStoredQuery, Page<GetBloodStoredResponse>>
{
    public async Task<Result<Page<GetBloodStoredResponse>>> Handle(GetBloodStoredQuery request, CancellationToken cancellationToken)
    {
        var query = context.BloodStored;
        var totalCount = await query.CountAsync(cancellationToken);

        var result = await query
            .OrderBy(x => x.BloodType.Name)
            .ApplyPagination(request.PageNumber, request.PageSize)
            .Select(x => new GetBloodStoredResponse
            {
                StoredId = x.StoredId,
                BloodTypeId = x.BloodTypeId,
                BloodTypeName = x.BloodType.Name,
                Quantity = x.Quantity,
                LastUpdated = x.LastUpdated
            })
            .ToListAsync(cancellationToken);
        return new Page<GetBloodStoredResponse>(
            result,
            totalCount,
            request.PageNumber,
            request.PageSize);
    }

}