using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnityFunds.Web.Models;

[Table("public_contact_help")]
public class PublicContactHelp
{
    [Key]
    public int pch_id { get; set; }
    public string pch_user_id { get; set; } = string.Empty;
    public string pch_user_name { get; set; } = string.Empty;
    public string pch_user_phone { get; set; } = string.Empty;
    public string pch_user_email { get; set; } = string.Empty;
    public string pch_user_location { get; set; } = string.Empty;
    public string pch_desc { get; set; } = string.Empty;
    public string pch_date { get; set; } = string.Empty;
    public string pch_admin_reply { get; set; } = string.Empty;
}
