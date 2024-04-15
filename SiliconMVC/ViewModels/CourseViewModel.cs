using SiliconMVC.Models;

namespace SiliconMVC.ViewModels;

public class CourseViewModel
{
    public IEnumerable<CourseModel> Courses { get; set; } = [];
}
