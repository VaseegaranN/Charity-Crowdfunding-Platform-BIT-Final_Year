
using System.ComponentModel.DataAnnotations;

namespace UnityFunds.Web.Models.ViewModels;

public class DonorSignupViewModel
{
    [Required]
    [StringLength(100)]
    public string donor_user_name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(254)]
    public string donor_user_email { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string donor_user_phone { get; set; } = string.Empty;

    [Required]
    public string donor_user_gender { get; set; } = string.Empty;

    [Required]
    public string donor_user_street { get; set; } = string.Empty;

    [Required]
    public string donor_user_city { get; set; } = string.Empty;

    [Required]
    public string donor_user_state { get; set; } = string.Empty;

    [Required]
    public string donor_user_zip { get; set; } = string.Empty;

    [Required]
    public string donor_user_country { get; set; } = string.Empty;

    [Required]
    [MinLength(8)]
    [StringLength(128)]
    public string donor_user_pwd { get; set; } = string.Empty;

    [Required]
    public string fundingType { get; set; } = string.Empty;

    [Required]
    public string paymentType { get; set; } = string.Empty;

    [Required]
    public string childhood_pet { get; set; } = string.Empty;

    [Required]
    public string vacation_destination { get; set; } = string.Empty;

    [Required]
    public string favourite_hobby { get; set; } = string.Empty;
}
