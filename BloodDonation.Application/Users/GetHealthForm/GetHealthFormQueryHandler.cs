using Microsoft.EntityFrameworkCore;
using BloodDonation.Application.Abstraction.Authentication;
using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Application.Abstraction.Query;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.QuestionForm.Errors;

namespace BloodDonation.Application.Users.GetHealthForm
{
    public class GetHealthFormQueryHandler(IDbContext context, IUserContext userContext) : IQueryHandler<GetHealthFormQuery, GetHealthFormResponse>
    {
        
        // public async Task<Result<GetHealthFormResponse>> Handle(GetHealthFormQuery request, CancellationToken cancellationToken)
        // {
        //     var currentUserId = userContext.UserId;
        //
        //     var healthForm = await context.HealthForms
        //         .Include(hf => hf.Answers)
        //             .ThenInclude(a => a.Question)
        //         .Where(hf => hf.UserId == currentUserId)
        //         .OrderByDescending(hf => hf.CreatedAt) // lấy form mới nhất nếu có nhiều
        //         .Select(hf => new GetHealthFormResponse
        //         {
        //             FormId = hf.FormId,
        //             CreatedAt = hf.CreatedAt,
        //             IsApproved = hf.IsApproved,
        //             ApprovedBy = hf.ApprovedBy,
        //             ApprovedAt = hf.ApprovedAt,
        //             ApprovedByStaffName = hf.ApprovedByStaffName,
        //             Questions = hf.Answers.Select(a => new QuestionAnswerDto
        //             {
        //                 Content = a.Question.Content,
        //                 Answer = a.Answer
        //             }).ToList()
        //         })
        //         .FirstOrDefaultAsync(cancellationToken);
        //
        //     if (healthForm == null)
        //     {
        //         return Result.Failure<GetHealthFormResponse>(QuestionFormErrors.HealthFormNotFound);
        //     }
        //
        //     return Result.Success(healthForm);
        // }
        public async Task<Result<GetHealthFormResponse>> Handle(GetHealthFormQuery request, CancellationToken cancellationToken)
        {
            var currentUserId = userContext.UserId;

            var healthForm = await context.HealthForms
                .Include(hf => hf.Answers)
                .ThenInclude(a => a.Question)
                .Where(hf => hf.UserId == currentUserId)
                .OrderByDescending(hf => hf.CreatedAt)
                .FirstOrDefaultAsync(cancellationToken);

            if (healthForm == null)
            {
                return Result.Failure<GetHealthFormResponse>(QuestionFormErrors.HealthFormNotFound);
            }

            var totalQuestions = healthForm.Answers.Count;

            var query = context.HealthAnswers
                .Where(a => a.Form.UserId == currentUserId)
                .ApplyPagination(request.PageNumber, request.PageSize)
                .Select(a => new QuestionAnswerDto
                {
                    Content = a.Question.Content,
                    Answer = a.Answer
                });

            var pagedAnswers = await query.ToListAsync(cancellationToken);


            var response = new GetHealthFormResponse
            {
                FormId = healthForm.FormId,
                CreatedAt = healthForm.CreatedAt,
                Status = healthForm.Status.ToString(),
                ApprovedBy = healthForm.ApprovedBy,
                ApprovedAt = healthForm.ApprovedAt,
                ApprovedByStaffName = healthForm.ApprovedByStaffName,
                TotalQuestions = totalQuestions,
                Questions = pagedAnswers
            };

            return Result.Success(response);
        }

    }
}
