using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnityFunds.Web.Models;

[Table("gobi_users")]
public class GobiUser
{
    [Key]
    public int gobi_user_id { get; set; }
    public string gobi_user_name { get; set; } = string.Empty;
    public string gobi_user_dob { get; set; } = string.Empty;
    public string gobi_user_phone_number { get; set; } = string.Empty;
    public string gobi_user_place { get; set; } = string.Empty;
    public string gobi_user_email { get; set; } = string.Empty;
    public string gobi_user_password { get; set; } = string.Empty;
    public string gobi_user_address { get; set; } = string.Empty;
    public string gobi_user_main_area { get; set; } = string.Empty;
    public string gobi_user_acc_status { get; set; } = "UNBLOCKED";
    public string gobi_user_mpin { get; set; } = string.Empty;
}
