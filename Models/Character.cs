namespace CourseStudent.Models;

public class Character
{
    public int CharacterId   { get; set; }
    public string FirstName  { get; set; } = null!;
    public string LastName   { get; set; } = null!;
    public int CurrentWeight { get; set; }
    public int MaxWeight     { get; set; }

    public ICollection<Backpack>       BackpackItems   { get; set; } = new List<Backpack>();
    public ICollection<CharacterTitle> Titles          { get; set; } = new List<CharacterTitle>();
}
