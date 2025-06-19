using CourseStudent.Data;
using CourseStudent.DTOs;
using CourseStudent.Models;
using CourseStudent.Services;

using Microsoft.EntityFrameworkCore;

namespace GameApi.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly AppDbContext _ctx;
        public CharacterService(AppDbContext ctx) => _ctx = ctx;

        public async Task<CharacterDetailsDto?> GetCharacterAsync(int id)
        {
            var ch = await _ctx.Characters
                .Include(c => c.BackpackItems!)
                .ThenInclude(b => b.Item)
                .Include(c => c.Titles!)
                .ThenInclude(ct => ct.Title)
                .FirstOrDefaultAsync(c => c.CharacterId == id);

            if (ch == null) return null;

            return new CharacterDetailsDto
            {
                FirstName     = ch.FirstName,
                LastName      = ch.LastName,
                CurrentWeight = ch.CurrentWeight,
                MaxWeight     = ch.MaxWeight,

                BackpackItems = ch.BackpackItems!
                    .Select(b => new BackpackItemDto {
                        ItemName   = b.Item.Name,
                        ItemWeight = b.Item.Weight,
                        Amount     = b.Amount
                    })
                    .ToList(),

                Titles = ch.Titles!
                    .Select(ct => new TitleDto {
                        Title      = ct.Title.Name,
                        AcquiredAt = ct.AcquiredAt
                    })
                    .ToList()
            };
        }
        
        
        
        
        public async Task<string?> AddBackpackItemsAsync(int characterId, List<int> itemIds)
        {
            var character = await _ctx.Characters
                .Include(c => c.BackpackItems!)
                    .ThenInclude(b => b.Item)
                .FirstOrDefaultAsync(c => c.CharacterId == characterId);

            if (character == null)
                return "Character not found";

            if (itemIds == null || !itemIds.Any())
                return "No items provided";

            var distinctIds = itemIds.Distinct().ToList();
            var items = await _ctx.Items
                .Where(i => distinctIds.Contains(i.ItemId))
                .ToListAsync();

            if (items.Count != distinctIds.Count)
                return "One or more items not found";

            var toAdd = itemIds
                .GroupBy(id => id)
                .Select(g => new { ItemId = g.Key, Count = g.Count() })
                .ToList();

            var additionalWeight = toAdd
                .Sum(x => items.Single(i => i.ItemId == x.ItemId).Weight * x.Count);

            if (character.CurrentWeight + additionalWeight > character.MaxWeight)
                return "Not enough carry capacity";

            foreach (var group in toAdd)
            {
                var bp = character.BackpackItems!
                    .FirstOrDefault(b => b.ItemId == group.ItemId);

                if (bp != null)
                {
                    bp.Amount += group.Count;
                }
                else
                {
                    _ctx.Backpacks.Add(new Backpack
                    {
                        CharacterId = characterId,
                        ItemId      = group.ItemId,
                        Amount      = group.Count
                    });
                }
            }

            character.CurrentWeight += additionalWeight;

            await _ctx.SaveChangesAsync();
            return null;
        }
    }
}
