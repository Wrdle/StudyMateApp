using Mobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mobile.Services.Interfaces
{
    public interface ICoverColorStore
    {
        Task<ICollection<CoverColor>> GetAll();
    }
}
