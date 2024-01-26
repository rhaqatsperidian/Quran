using Application.Repositories.Base;
using Application.Repositories.Interfaces;
using Dapper;
using DbModels;
using DbModels.ModelHelpers;
using Dtos;

namespace Application.Repositories.Repos
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
                return Db().QueryAsync<SurahListDto>($"SELECT DISTINCT {nameof(QuranData.surah_name_urdu)} as SurahName, {nameof(QuranData.surah_id)} as SurahId FROM {TblUtils<QuranData>.Table()}");
            }
            else
            {
                return Db().QueryAsync<SurahListDto>($"SELECT DISTINCT {nameof(QuranData.surah_name_eng)} as SurahName, {nameof(QuranData.surah_id)} as SurahId FROM {TblUtils<QuranData>.Table()}");
            }
        }

        public Task<IEnumerable<QuranData>> GetQuranDataBySurah(int SurahId)
        {
            var sql = $@"SELECT * FROM {TblUtils<QuranData>.Table()}
                                                        WHERE {nameof(QuranData.surah_id)} = {SurahId}
                                                        ORDER BY {nameof(QuranData.aya_id)} ASC";
            return Db().QueryAsync<QuranData>(sql);
        }

    }
}
