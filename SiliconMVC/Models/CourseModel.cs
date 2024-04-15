namespace SiliconMVC.Models;

public class CourseModel
{
    public int Id { get; set; }

    public string ImageUrl { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public decimal Price { get; set; }

    public decimal DiscountPrice { get; set; }

    public int Hours { get; set; }

    public int TotalBuyers { get; set; } 

    public int TotalLikes { get; set; } 

    public bool BestSeller { get; set; } = false;
    
    public string LikesInPercent() {
        double likes = ((double)TotalLikes / (double)TotalBuyers) * 100;
        likes = Convert.ToInt16(likes);
        return likes.ToString();
    }

    public string TotalBuysInK() {
        string totalInK = Math.Round((double)TotalBuyers / 1000, 1).ToString();
        totalInK = totalInK.Replace(',','.');
        return totalInK;
    }
}
