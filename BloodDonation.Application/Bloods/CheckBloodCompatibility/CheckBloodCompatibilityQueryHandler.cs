using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.Bloods;
using BloodDonation.Domain.Bloods.Errors;
using BloodDonation.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.Bloods.CheckBloodCompatibility;

public class CheckBloodCompatibilityQueryHandler(IDbContext context) : IQueryHandler<CheckBloodCompatibilityQuery, CheckBloodCompatibilityResponse>
{
    public async Task<Result<CheckBloodCompatibilityResponse>> Handle(CheckBloodCompatibilityQuery request,
        CancellationToken cancellationToken)
    {
        var fromType = await context.BloodTypes.FirstOrDefaultAsync(b => b.Name == request.FromBloodType, cancellationToken);
        var toType = await context.BloodTypes.FirstOrDefaultAsync(b => b.Name == request.ToBloodType, cancellationToken);

        if (fromType is null || toType is null)
        {
            return Result.Failure<CheckBloodCompatibilityResponse>(BloodErrors.BloodTypeNotFound);
        }
        
        var isCompatible = IsCompatible(request.FromBloodType.ToUpperInvariant(), request.ToBloodType.ToUpperInvariant(), request.ComponentType);

        
        return Result.Success(new CheckBloodCompatibilityResponse
        {
            FromBloodType = fromType.Name,
            ToBloodType = toType.Name,
            ComponentType = request.ComponentType,
            IsCompatible = isCompatible
        });
    }

    private bool IsCompatible(string from, string to, BloodComponentType component)
    {
        return component switch
        {
            BloodComponentType.Whole or BloodComponentType.RBC => (from, to) switch
            {
                ("O+", var t) => new[] { "O+", "A+", "B+", "AB+" }.Contains(t),
                ("A-", var t) => new[] { "A-", "A+", "AB-", "AB+" }.Contains(t),
                ("A+", var t) => new[] { "A+", "AB+" }.Contains(t),
                ("B-", var t) => new[] { "B-", "B+", "AB-", "AB+" }.Contains(t),
                ("B+", var t) => new[] { "B+", "AB+" }.Contains(t),
                ("AB-", var t) => new[] { "AB-", "AB+" }.Contains(t),
                ("AB+", "AB+") => true,
                ("O-", _) => true,
                _ => false
            },

            BloodComponentType.Plasma => (from, to) switch
            {
                ("AB+", _) => true,
                ("AB-", var t) => new[] { "O-", "A-", "B-", "AB-" }.Contains(t),
                ("A+", var t) => new[] { "O+", "A+" }.Contains(t),
                ("A-", var t) => new[] { "O-", "A-" }.Contains(t),
                ("B+", var t) => new[] { "O+", "B+" }.Contains(t),
                ("B-", var t) => new[] { "O-", "B-" }.Contains(t),
                ("O+", "O+") => true,
                ("O-", "O-") => true,
                _ => false
            },

            BloodComponentType.Platelet => (from, to) switch
            {
                ("O-", "O-") => true,
                ("O+", var t) => new[] { "O+", "A+", "B+", "AB+" }.Contains(t),
                ("A-", var t) => new[] { "A-", "A+", "AB-", "AB+" }.Contains(t),
                ("A+", var t) => new[] { "A+", "AB+" }.Contains(t),
                ("B-", var t) => new[] { "B-", "B+", "AB-", "AB+" }.Contains(t),
                ("B+", var t) => new[] { "B+", "AB+" }.Contains(t),
                ("AB-", var t) => new[] { "AB-", "AB+" }.Contains(t),
                ("AB+", "AB+") => true,
                _ => false
            },

            _ => false
        };
    }
}