using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnityFunds.Web.Models;

[Table("govn_posts")]
public class GovernmentPost
{
    [Key]
    public string gp_id { get; set; } = string.Empty;
    public string gp_staff_id { get; set; } = string.Empty;
    public string gp_staff_name { get; set; } = string.Empty;
    public string gp_staff_phone { get; set; } = string.Empty;
    public string gp_loc { get; set; } = string.Empty;
    public string gp_date { get; set; } = string.Empty;
    public string gp_type { get; set; } = string.Empty;
    public string gp_desc { get; set; } = string.Empty;
    public string gp_media { get; set; } = string.Empty;
    public DateTime gp_time { get; set; }
}
