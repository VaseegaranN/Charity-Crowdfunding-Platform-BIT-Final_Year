using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnityFunds.Web.Models;

[Table("user_reports")]
public class UserReport
{
    [Key]
    public int id { get; set; }
    public string reporter_name { get; set; } = string.Empty;
    public string reported_user { get; set; } = string.Empty;
    public string reason { get; set; } = string.Empty;
    public string status { get; set; } = "Pending";
}
