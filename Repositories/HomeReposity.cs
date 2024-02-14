using Microsoft.EntityFrameworkCore;

namespace ElectronicsStore.Repositories
{    
    public class HomeReposity : IHomeRepository
    {
        private readonly ApplicationDbContext _db;
        public HomeReposity(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Genre>> Genres()
        {
            return await _db.Genres.ToListAsync();
        }
        public async Task<IEnumerable<Electronic>> GetElectronics(string sTerm = "", int genreId = 0)
        {
            sTerm = sTerm.ToLower();
            IEnumerable<Electronic> electronics = await (from electronic in _db.Electronics
                               join genre in _db.Genres
                               on electronic.GenreId equals genre.Id
                               where string.IsNullOrWhiteSpace(sTerm) || (electronic != null && electronic.BrandName.ToLower().StartsWith(sTerm))
                               select new Electronic
                               {
                                   Id = electronic.Id,
                                   Image = electronic.Image,
                                   AuthorName = electronic.AuthorName,
                                   BrandName = electronic.BrandName,
                                   GenreId = electronic.GenreId,
                                   Price = electronic.Price,
                                   GenreName = genre.GenreName
                               }
                               ).ToListAsync();
            if(genreId > 0)
            {
                electronics = electronics.Where(a => a.GenreId == genreId).ToList();
            }
            return electronics;
        }
    }
}
