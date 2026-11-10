using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace UnityFunds.Web.Models;
[Table("Donations")]
public class Donation {
 [Key] public long Id {get;set;}
 public int CampaignId {get;set;}
 public int DonorId {get;set;}
 [Column(TypeName="decimal(18,2)")] public decimal Amount {get;set;}
 [Required, StringLength(24)] public string Status {get;set;} = "Pending";
 [Required, StringLength(80)] public string PaymentReference {get;set;} = Guid.NewGuid().ToString("N");
 [StringLength(80)] public string PaymentProvider {get;set;} = "NotConfigured";
 public DateTime CreatedAtUtc {get;set;} = DateTime.UtcNow;
 public DateTime? PaidAtUtc {get;set;}
}
