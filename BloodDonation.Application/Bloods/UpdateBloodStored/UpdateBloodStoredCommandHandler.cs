using BloodDonation.Application.Abstraction.Data;
using BloodDonation.Application.Abstraction.Messaging;
using BloodDonation.Domain.Bloods.Errors;
using BloodDonation.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace BloodDonation.Application.Bloods.UpdateBloodStored;

public class UpdateBloodStoredCommandHandler(IDbContext context)
    : ICommandHandler<UpdateBloodStoredCommand, UpdateBloodStoredResponse>
{
    public async Task<Result<UpdateBloodStoredResponse>> Handle(UpdateBloodStoredCommand command, CancellationToken cancellationToken)
    {
        var bloodType = await context.BloodTypes
            .FirstOrDefaultAsync(b => b.Name == command.BloodTypeName, cancellationToken);
        
        if (bloodType is null)
        {
            return Result.Failure<UpdateBloodStoredResponse>(
                BloodErrors.BloodTypeNotExist(command.BloodTypeName));
        }
        
        var stored = await context.BloodStored
            .FirstOrDefaultAsync(s => s.BloodTypeId == bloodType.BloodTypeId, cancellationToken);

        if (stored is null)
        {
            return Result.Failure<UpdateBloodStoredResponse>(BloodErrors.BloodStoredNotFound(command.BloodTypeName));

        }

        if (command.Quantity.HasValue)
        {
            var newQuantity = stored.Quantity + command.Quantity.Value;
            if (newQuantity < 0)
            {
                return Result.Failure<UpdateBloodStoredResponse>(BloodErrors.QuantityInvalid);
            }

            stored.Quantity = newQuantity;
            stored.LastUpdated = DateTime.UtcNow;
        }

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success(new UpdateBloodStoredResponse(stored));
    }
}