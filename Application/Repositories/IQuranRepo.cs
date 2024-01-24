
namespace Application.Repositories
{
    public interface IQuranRepo
    {
        Task<IEnumerable<string>> GetSurahList(bool arabicNames = true);
    }
}