using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnityFunds.Web.Models;

[Table("logistics_details")]
public class LogisticsDetails
{
    [Key]
    public string logistics_id { get; set; } = string.Empty;
    public string logistics_user_name { get; set; } = string.Empty;
    public string logistics_user_position { get; set; } = string.Empty;
    public string logistics_user_phone { get; set; } = string.Empty;
    public string logistics_user_email { get; set; } = string.Empty;
    public string logistics_user_pwd { get; set; } = string.Empty;
    public string logistics_org_name { get; set; } = string.Empty;
    public string logistics_org_place { get; set; } = string.Empty;
    public string logistics_org_phone { get; set; } = string.Empty;
    public string logistics_org_mail { get; set; } = string.Empty;
    public byte[]? logistics_id_image { get; set; }
    public string logistics_account_req_status { get; set; } = "Pending";
    public string childhood_pet { get; set; } = string.Empty;
    public string vacation_destination { get; set; } = string.Empty;
    public string favourite_hobby { get; set; } = string.Empty;
}
