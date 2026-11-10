using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnityFunds.Web.Models;

[Table("admin_details")]
public class AdminDetails
{
    [Key]
    public int admin_id { get; set; }
    public string admin_name { get; set; } = string.Empty;
    public string admin_email { get; set; } = string.Empty;
    public string admin_phone_number { get; set; } = string.Empty;
    public string admin_pass { get; set; } = string.Empty;
    public byte[]? admin_profile_picture { get; set; }
}
