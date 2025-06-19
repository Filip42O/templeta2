namespace CourseStudent.DTOs;

public class BackpackItemDto
{
    public string ItemName   { get; set; } = null!;
    public int    ItemWeight { get; set; }
    public int    Amount     { get; set; }
}