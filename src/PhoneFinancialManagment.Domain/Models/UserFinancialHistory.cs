using PhoneFinancialManagment.Domain.Enums;

namespace PhoneFinancialManagment.Domain.Models;

public class UserFinancialHistory
{
    public int HistoryId { get; set; }
    public HistoryType Type { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public decimal Balance { get; set; }
    public DateTime DateRegistered { get; set; }
}