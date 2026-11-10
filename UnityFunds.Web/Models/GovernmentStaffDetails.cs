using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnityFunds.Web.Models;

[Table("govn_staff_details")]
public class GovernmentStaffDetails
{
    [Key]
    public string govn_staff_id { get; set; } = string.Empty;
    public string govn_staff_name { get; set; } = string.Empty;
    public string govn_staff_phone { get; set; } = string.Empty;
    public string govn_staff_email { get; set; } = string.Empty;
    public string govn_staff_location { get; set; } = string.Empty;
    public string govn_staff_password { get; set; } = string.Empty;
    public string govn_staff_work_dept { get; set; } = string.Empty;
    public string govn_staff_mpin { get; set; } = string.Empty;
}
