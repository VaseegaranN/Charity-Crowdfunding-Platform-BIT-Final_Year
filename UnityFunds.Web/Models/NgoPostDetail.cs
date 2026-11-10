using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnityFunds.Web.Models;

[Table("ngo_post_details")]
public class NgoPostDetail
{
    [Key]
    public string ngo_post_id { get; set; } = string.Empty;
    public string ngo_user_id { get; set; } = string.Empty;
    public string ngo_user_name { get; set; } = string.Empty;
    public string ngo_user_email { get; set; } = string.Empty;
    public string ngo_user_phone { get; set; } = string.Empty;
    public string ngo_post_date { get; set; } = string.Empty;
    public string ngo_post_type { get; set; } = string.Empty;
    public string ngo_post_desc { get; set; } = string.Empty;
    public string ngo_post_media { get; set; } = string.Empty;
    public DateTime ngo_post_time { get; set; }
}
