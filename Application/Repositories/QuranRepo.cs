using Application.Repositories.Interfaces;
using Dapper;
using DbModels;
using DbModels.ModelHelpers;
using Dtos;

namespace Application.Repositories
{
    public class QuranRepo : BaseRepo, IQuranRepo
    {
        public QuranRepo(string? cnnString) : base(cnnString)
        {
        }

        public Task<IEnumerable<SurahListDto>> GetSurahList(bool arabicNames = true)
        {
            if (arabicNames)
            {
                return GetDb().QueryAsync<SurahListDto>($"SELECT DISTINCT {nameof(QuranData.surah_name_urdu)} as SurahName, {nameof(QuranData.surah_id)} as SurahId FROM {TableNameProvider<QuranData>.GetTableName()}");
            }
            else
            {
                return GetDb().QueryAsync<SurahListDto>($"SELECT DISTINCT {nameof(QuranData.surah_name_eng)} as SurahName, {nameof(QuranData.surah_id)} as SurahId FROM {TableNameProvider<QuranData>.GetTableName()}");
            }
        }

        public Task<IEnumerable<QuranData>> GetQuranDataBySurah(int SurahNumber)
        {
            var sql = $@"SELECT * FROM {TableNameProvider<QuranData>.GetTableName()}
                                                        WHERE {nameof(QuranData.surah_id)} = {SurahNumber}
                                                        ORDER BY {nameof(QuranData.aya_id)} ASC";
            return GetDb().QueryAsync<QuranData>(sql);
        }
        //public Task<IEnumerable<string>> GetParaList(bool arabicNames = true)
        //{

        //}
    }
}
