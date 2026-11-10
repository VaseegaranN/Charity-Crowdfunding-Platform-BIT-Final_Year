using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnityFunds.Web.Models;

[Table("admin_posts")]
public class AdminPost
{
    [Key]
    public string ap_id { get; set; } = string.Empty;
    public string ap_admin_id { get; set; } = string.Empty;
    public string ap_admin_name { get; set; } = string.Empty;
    public string ap_admin_phone { get; set; } = string.Empty;
    public string ap_place { get; set; } = string.Empty;
    public string ap_date { get; set; } = string.Empty;
    public string ap_type { get; set; } = string.Empty;
    public string ap_desc { get; set; } = string.Empty;
    public string ap_media { get; set; } = string.Empty;
    public DateTime ap_time { get; set; }
}
