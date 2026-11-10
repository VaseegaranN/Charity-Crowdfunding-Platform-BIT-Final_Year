using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnityFunds.Web.Models;

[Table("sathy_users")]
public class SathyUser
{
    [Key]
    public int sathy_user_id { get; set; }
    public string sathy_user_name { get; set; } = string.Empty;
    public string sathy_user_dob { get; set; } = string.Empty;
    public string sathy_user_phone_number { get; set; } = string.Empty;
    public string sathy_user_place { get; set; } = string.Empty;
    public string sathy_user_email { get; set; } = string.Empty;
    public string sathy_user_password { get; set; } = string.Empty;
    public string sathy_user_address { get; set; } = string.Empty;
    public string sathy_user_main_area { get; set; } = string.Empty;
    public string sathy_user_acc_status { get; set; } = "UNBLOCKED";
    public string sathy_user_mpin { get; set; } = string.Empty;
}
