namespace CourseStudent.Models;

public class Item
{
    public int ItemId   { get; set; }
    public string Name  { get; set; } = null!;
    public int Weight   { get; set; }

    public ICollection<Backpack> Backpacks { get; set; } = new List<Backpack>();
}