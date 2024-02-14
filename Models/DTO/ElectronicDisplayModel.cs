namespace ElectronicsStore.Models.DTO
{
    public class ElectronicDisplayModel
    {
        public IEnumerable<Electronic> Electronics { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public string STerm { get; set; } = "";
        public int GenreId { get; set; } = 0;
    }
}
