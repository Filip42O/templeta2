namespace CourseStudent.DTOs;

public class CharacterDetailsDto
{
    public string FirstName     { get; set; } = null!;
    public string LastName      { get; set; } = null!;
    public int    CurrentWeight { get; set; }
    public int    MaxWeight     { get; set; }

    public List<BackpackItemDto> BackpackItems { get; set; } = new();
    public List<TitleDto>        Titles         { get; set; } = new();
}