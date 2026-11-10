using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnityFunds.Web.Models;

[Table("ngo_details")]
public class NgoDetails
{
    [Key]
    public string ngo_id { get; set; } = string.Empty;
    public string ngo_user_name { get; set; } = string.Empty;
    public string ngo_user_position { get; set; } = string.Empty;
    public string ngo_user_phone { get; set; } = string.Empty;
    public string ngo_user_email { get; set; } = string.Empty;
    public string ngo_user_pwd { get; set; } = string.Empty;
    public string ngo_org_name { get; set; } = string.Empty;
    public string ngo_org_place { get; set; } = string.Empty;
    public string ngo_org_phone { get; set; } = string.Empty;
    public string ngo_org_mail { get; set; } = string.Empty;
    public byte[]? ngo_id_image { get; set; }
    public string ngo_acc_req_status { get; set; } = "Pending";
    public string childhood_pet { get; set; } = string.Empty;
    public string vacation_destination { get; set; } = string.Empty;
    public string favourite_hobby { get; set; } = string.Empty;
}
