namespace CourseStudent.Models;

public class Backpack
{
    public int CharacterId { get; set; }
    public int ItemId      { get; set; }
    public int Amount      { get; set; }

    public Character Character { get; set; } = null!;
    public Item      Item      { get; set; } = null!;
}