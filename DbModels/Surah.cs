using Dapper.Contrib.Extensions;

namespace DbModels
{
    [Table("surah")]
    public class Surah
    {
        [Key]
        public int surahId { get; set; }
        public string surahName { get; set; }
        public string surahIntroduction { get; set; }
        public int surahTotalRuku { get; set; }
        public int surahTotalAyaat { get; set; }
        public string surahPlace { get; set; }
        public int surahTarteebNuzool { get; set; }
        public int surahParaId { get; set; }
        public int surahManzil { get; set; }
        public int surahVisible { get; set; }
        public string mushtamil_para { get; set; }
        public int total_words { get; set; }
        public int total_letters { get; set; }
    }
}
