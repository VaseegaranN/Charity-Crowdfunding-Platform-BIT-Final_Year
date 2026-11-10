using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnityFunds.Web.Models;

[Table("donor_details")]
public class DonorDetails
{
    [Key]
    public int donor_id { get; set; }
    public string donor_user_name { get; set; } = string.Empty;
    public string donor_user_email { get; set; } = string.Empty;
    public string donor_user_phone { get; set; } = string.Empty;
    public string donor_user_street { get; set; } = string.Empty;
    public string donor_user_city { get; set; } = string.Empty;
    public string donor_user_state { get; set; } = string.Empty;
    public string donor_user_zip { get; set; } = string.Empty;
    public string donor_user_country { get; set; } = string.Empty;
    public string donor_user_pwd { get; set; } = string.Empty;
    public string donor_user_gender { get; set; } = string.Empty;
    public string fundingType { get; set; } = string.Empty;
    public string donationAmount { get; set; } = "-";
    public string paymentType { get; set; } = string.Empty;
    public string upiId { get; set; } = "-";
    public string cardNumber { get; set; } = "-";
    public string expiry { get; set; } = "-";
    public string cvCode { get; set; } = "-";
    public string childhood_pet { get; set; } = "-";
    public string vacation_destination { get; set; } = "-";
    public string favourite_hobby { get; set; } = "-";
}
