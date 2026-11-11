
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace UnityFunds.Web.Models.ViewModels;

public class LogisticsSignupViewModel
{
    [Required, StringLength(100)]
    public string logistics_user_name { get; set; } = string.Empty;

    [Required, StringLength(100)]
    public string logistics_user_position { get; set; } = string.Empty;

    [Required, StringLength(20)]
    public string logistics_user_phone { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(254)]
    public string logistics_user_email { get; set; } = string.Empty;

    [Required, MinLength(8), StringLength(128)]
    public string logistics_user_pwd { get; set; } = string.Empty;

    [Required, StringLength(150)]
    public string logistics_org_name { get; set; } = string.Empty;

    [Required]
    public string logistics_org_place { get; set; } = string.Empty;

    [Required, StringLength(20)]
    public string logistics_org_phone { get; set; } = string.Empty;

    [Required, EmailAddress, StringLength(254)]
    public string logistics_org_mail { get; set; } = string.Empty;

    [Required]
    public IFormFile? logistics_id_image { get; set; }

    [Required]
    public string childhood_pet { get; set; } = string.Empty;

    [Required]
    public string vacation_destination { get; set; } = string.Empty;

    [Required]
    public string favourite_hobby { get; set; } = string.Empty;
}
