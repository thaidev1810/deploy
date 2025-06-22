using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Application.Abstraction.Query;
using BloodDonation.Domain.Common;
using BloodDonation.Domain.QuestionForm.Errors;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.QuestionForm.GetUserHealthFormDetail;

public class GetUserHealthFormDetailQueryHandler(IDbContext context) : IQueryHandler<GetUserHealthFormDetailQuery, GetUserHealthFormDetailResponse>
{
    
    // public async Task<Result<GetUserHealthFormDetailResponse>> Handle(GetUserHealthFormDetailQuery request, CancellationToken cancellationToken)
    // {
    //     var healthForm = await context.HealthForms
    //         .Include(hf => hf.User)
    //         .Include(hf => hf.Answers)
    //         .ThenInclude(a => a.Question)
    //         .Where(hf => hf.FormId == request.FormId)
    //         .Select(hf => new GetUserHealthFormDetailResponse
    //         {
    //             FormId = hf.FormId,
    //             UserId = hf.UserId,
    //             Name = hf.User != null ? hf.User.Name : string.Empty,
    //             CreatedAt = hf.CreatedAt,
    //             IsApproved = hf.IsApproved,
    //             ApprovedBy = hf.ApprovedBy,
    //             ApprovedAt = hf.ApprovedAt,
    //             ApprovedByStaffName = hf.ApprovedByStaffName,
    //             TotalQuestions = hf.Answers.Count,
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
    //         return Result.Failure<GetUserHealthFormDetailResponse>(QuestionFormErrors.HealthFormNotFound);
    //     }
    //
    //     return Result.Success(healthForm);
    // }
    
    public async Task<Result<GetUserHealthFormDetailResponse>> Handle(GetUserHealthFormDetailQuery request, CancellationToken cancellationToken)
    {
        // Lấy HealthForm chính theo FormId, bao gồm User và tổng số câu hỏi
        var healthForm = await context.HealthForms
            .Include(hf => hf.User)
            .Include(hf => hf.Answers)
            .Where(hf => hf.FormId == request.FormId)
            .Select(hf => new
            {
                Form = hf,
                UserName = hf.User != null ? hf.User.Name : string.Empty,
                TotalQuestions = hf.Answers.Count
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (healthForm == null)
        {
            return Result.Failure<GetUserHealthFormDetailResponse>(QuestionFormErrors.HealthFormNotFound);
        }

        // Phân trang câu trả lời (Questions)
        var pagedAnswers = await context.HealthAnswers
            .Where(a => a.FormId == request.FormId)
            .OrderBy(a => a.Question.Content) // hoặc theo thứ tự bạn muốn
            .ApplyPagination(request.PageNumber, request.PageSize)
            .Select(a => new QuestionAnswerDto
            {
                Content = a.Question.Content,
                Answer = a.Answer
            })
            .ToListAsync(cancellationToken);

        var response = new GetUserHealthFormDetailResponse
        {
            FormId = healthForm.Form.FormId,
            UserId = healthForm.Form.UserId,
            Name = healthForm.UserName,
            CreatedAt = healthForm.Form.CreatedAt,
            Status = healthForm.Form.Status.ToString(),
            ApprovedBy = healthForm.Form.ApprovedBy,
            ApprovedAt = healthForm.Form.ApprovedAt,
            ApprovedByStaffName = healthForm.Form.ApprovedByStaffName,
            TotalQuestions = healthForm.TotalQuestions,
            Questions = pagedAnswers
        };

        return Result.Success(response);
    }

}