namespace Insurance_agency.Models.ModelView;

public sealed class PendingPayment
{
    public string OrderId { get; set; } = string.Empty;
    public int UserId { get; set; }
    public long Amount { get; set; }
    public ContractView? NewContract { get; set; }
    public int? ExistingContractId { get; set; }
}
