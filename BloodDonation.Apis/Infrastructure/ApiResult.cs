using BloodDonation.Domain.Common;

namespace BloodDonation.Apis.Infrastructure;

public class ApiResult<TResult>
{
    public bool IsSuccess { get; set; }
    public TResult Data { get; set; }
    
    
    public static ApiResult<TResult> Success(TResult data)
    {
        return new ApiResult<TResult> { IsSuccess = true, Data = data };
    }
}