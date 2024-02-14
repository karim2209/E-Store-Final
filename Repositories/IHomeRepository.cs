namespace ElectronicsStore
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Electronic>> GetElectronics(string sTerm = "", int genreId = 0);
        Task<IEnumerable<Genre>> Genres();
    }
}