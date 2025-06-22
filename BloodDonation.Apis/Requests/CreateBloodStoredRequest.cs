namespace BloodDonation.Apis.Requests;

public class CreateBloodStoredRequest
{
    public int Quantity { get; set; }
    public string BloodTypeName { get; set; }
}