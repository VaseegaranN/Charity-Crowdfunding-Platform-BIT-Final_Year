using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnityFunds.Web.Models;

[Table("public_posts")]
public class PublicPost
{
    [Key]
    public string pp_id { get; set; } = string.Empty;
    public string pp_user_id { get; set; } = string.Empty;
    public string pp_username { get; set; } = string.Empty;
    public string pp_userphone { get; set; } = string.Empty;
    public string pp_userloc { get; set; } = string.Empty;
    public string pp_date { get; set; } = string.Empty;
    public string pp_type { get; set; } = string.Empty;
    public string pp_userpost_description { get; set; } = string.Empty;
    public string pp_media { get; set; } = string.Empty;
    public DateTime pp_time { get; set; }
}
