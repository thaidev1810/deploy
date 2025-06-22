using BloodDonation.Domain.Common;

namespace BloodDonation.Domain.Bloods.Errors;

public class BloodErrors
{
    public static readonly Error BloodTypeNotFound = Error.NotFound(
        "BloodType.NotFound",
        "The specified blood type does not exist.");
    
    public static readonly Error QuantityInvalid = Error.Failure(
        "Quantity.Invalid",
        "Quantity must be greater than 0.");
    
    public static Error BloodStoredNotFound(string bloodTypeName) =>
        Error.NotFound(
            "BloodStored.NotFound",
            $"No blood storage record found for blood type '{bloodTypeName}'."
        );
    
    public static Error BloodTypeNotExist(string bloodTypeName) =>
        Error.NotFound("BloodType.NotFound", $"Blood type '{bloodTypeName}' does not exist.");
    
    public static Error IncompatibleBloodTypes(string from, string to, BloodComponentType component) =>
        Error.Failure("BloodType.Incompatible",
            $"Cannot donate {component} from '{from}' to '{to}' due to incompatibility.");
}