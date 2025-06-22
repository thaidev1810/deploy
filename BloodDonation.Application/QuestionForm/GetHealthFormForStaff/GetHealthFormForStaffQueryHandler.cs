using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Application.Abstraction.Query;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.QuestionForm;
using BloodDonation.Domain.QuestionForm.Errors;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.QuestionForm.GetHealthFormForStaff;

public class GetHealthFormForStaffQueryHandler(IDbContext context) : IQueryHandler<GetHealthFormForStaffQuery, Page<GetHealthFormForStaffResponse>>
{
    public async Task<Result<Page<GetHealthFormForStaffResponse>>> Handle(GetHealthFormForStaffQuery request, CancellationToken cancellationToken)
    {
        var query = context.HealthForms.AsNoTracking().AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            // Cố gắng parse string sang enum
            if (Enum.TryParse<FormStatus>(request.Status, ignoreCase: true, out var parsedStatus))
            {
                query = query.Where(hf => hf.Status == parsedStatus);
            }
            else
            {
                // Nếu không parse được, có thể trả về lỗi hoặc bỏ qua filter
                // Ở đây mình trả lỗi:
                return Result.Failure<Page<GetHealthFormForStaffResponse>>(QuestionFormErrors.InvalidStatus(request.Status));
            }
        }

        query = query.OrderBy(hf => hf.Status == FormStatus.Pending ? 0 : 1)
            .ThenByDescending(hf => hf.CreatedAt);

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .ApplyPagination(request.PageNumber, request.PageSize)
            .Select(hf => new GetHealthFormForStaffResponse
            {
                FormId = hf.FormId,
                UserId = hf.UserId,
                CreatedAt = hf.CreatedAt,
                Status = hf.Status.ToString(),
                ApprovedByStaffName = hf.ApprovedByStaffName
            })
            .ToListAsync(cancellationToken);

        var page = new Page<GetHealthFormForStaffResponse>(
            items,
            totalCount,
            request.PageNumber,
            request.PageSize);

        return Result.Success(page);
    }


}