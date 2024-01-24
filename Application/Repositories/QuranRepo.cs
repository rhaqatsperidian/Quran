using Dapper;
using DbModels;
using DbModels.ModelHelpers;

namespace Application.Repositories
{
    public class QuranRepo : BaseRepo, IQuranRepo
    {
        public QuranRepo(string? cnnString) : base(cnnString)
        {
        }

        public Task<IEnumerable<string>> GetSurahList(bool arabicNames = true)
        {
            if (arabicNames)
                return GetDb().QueryAsync<string>($"SELECT DISTINCT {nameof(QuranData.surah_name_urdu)} FROM {TableNameProvider<QuranData>.GetTableName()}");
            else
                return GetDb().QueryAsync<string>($"SELECT DISTINCT {nameof(QuranData.surah_name_eng)} FROM {TableNameProvider<QuranData>.GetTableName()}");
        }
        //public Task<IEnumerable<string>> GetParaList(bool arabicNames = true)
        //{

        //}
    }
}
