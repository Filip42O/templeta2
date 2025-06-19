namespace CourseStudent.Models;

public class Title
{
    public int TitleId { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<CharacterTitle> CharacterTitles { get; set; }
        = new List<CharacterTitle>();
}