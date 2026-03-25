namespace LibrarySystem.Core.Interfaces
{
    public interface ISearchable
    {
        bool Matches(string searchTerm);
    }
}