using DbModels;
using Dtos;

namespace Application.Repositories.Interfaces
{
    public interface IQuranRepo
    {
        Task<IEnumerable<QuranData>> GetQuranDataBySurah(int SurahNumber);
        Task<IEnumerable<SurahListDto>> GetSurahList(bool arabicNames = true);
    }
}