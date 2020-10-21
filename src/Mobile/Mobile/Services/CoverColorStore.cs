using Microsoft.EntityFrameworkCore;
using Mobile.Data;
using Mobile.Models;
using Mobile.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mobile.Services
{
    public class CoverColorStore : ICoverColorStore
    {
        public async Task<ICollection<CoverColor>> GetAll()
        {
            using (var dbContext = new AppDbContext())
            {
                var colors = await dbContext.CoverColors
                    .Select(cc => new CoverColor
                    {
                        Id = cc.Id,
                        BackgroundColor = cc.BackgroundColorFromHex,
                        FontColor = cc.FontColorFromHex
                    })
                    .ToListAsync();

                return colors;
            }
        }
    }
}