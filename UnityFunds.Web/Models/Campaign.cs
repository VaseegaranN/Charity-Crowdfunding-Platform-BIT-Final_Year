using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace UnityFunds.Web.Models;
[Table("Campaigns")]
public class Campaign {
 [Key] public int Id {get;set;}
 [Required, StringLength(160)] public string Title {get;set;} = "";
 [Required, StringLength(4000)] public string Description {get;set;} = "";
 [Required, StringLength(80)] public string Category {get;set;} = "General";
 [Required, StringLength(64)] public string NgoId {get;set;} = "";
 [Required, StringLength(160)] public string NgoName {get;set;} = "";
 [Column(TypeName="decimal(18,2)")] public decimal GoalAmount {get;set;}
 [Column(TypeName="decimal(18,2)")] public decimal RaisedAmount {get;set;}
 [StringLength(500)] public string? ImageUrl {get;set;}
 [Required, StringLength(20)] public string Status {get;set;} = "Pending";
 public DateTime CreatedAtUtc {get;set;} = DateTime.UtcNow;
 public DateTime? EndDateUtc {get;set;}
}
