
using System.ComponentModel.DataAnnotations;

namespace UnityFunds.Web.Models.ViewModels;

public class CampaignCreateViewModel
{
    [Required]
    [StringLength(160)]
    public string Title { get; set; } = "";

    [Required]
    [StringLength(4000)]
    public string Description { get; set; } = "";

    [Required]
    [StringLength(80)]
    public string Category { get; set; } = "General";

    [Range(typeof(decimal), "1", "9999999999999999",
        ErrorMessage = "The fundraising goal must be greater than zero.")]
    public decimal GoalAmount { get; set; }

    [Url(ErrorMessage = "Enter a valid image URL.")]
    [StringLength(500)]
    public string? ImageUrl { get; set; }
}
