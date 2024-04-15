using System.ComponentModel.DataAnnotations;

namespace SiliconMVC.Models;

public class SubscribeModel
{
    [DataType(DataType.EmailAddress)]
    [Required(ErrorMessage = "Enter a valid email address")]
    [RegularExpression("[^@ \\t\\r\\n]+@[^@ \\t\\r\\n]+\\.[^@ \\t\\r\\n]+", ErrorMessage = "The email address is not valid")]
    public string Email { get; set; } = null!;
    public bool DailyNewsletter { get; set; }
    public bool AdvertisingUpdates { get; set; }
    public bool WeekInReview { get; set; }
    public bool EventUpdates { get; set; }
    public bool StartupsWeekly { get; set;}
    public bool Podcasts { get; set; }
}
