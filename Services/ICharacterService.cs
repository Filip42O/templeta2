using CourseStudent.DTOs;

namespace CourseStudent.Services;

public interface ICharacterService
{
    Task<CharacterDetailsDto?> GetCharacterAsync(int id);
    
    Task<string?> AddBackpackItemsAsync(int characterId, List<int> itemIds);
}