using Dapper.Contrib.Extensions;

namespace DbModels
{
    [Table("Quran_data")]
    public class QuranData
    {
        [ExplicitKey]
        public int aya_id { get; set; }
        public int surah_id { get; set; }
        public int para_id { get; set; }
        public int aya_num { get; set; }
        public int page_number { get; set; }
        public string para_name { get; set; }
        public string surah_name_urdu { get; set; }
        public string surah_name_eng { get; set; }
        public string ar { get; set; }
        public string ar_clean { get; set; }
        public string ur_1 { get; set; }
        public string ur_2 { get; set; }
        public string ur_3 { get; set; }
        public string ur_4 { get; set; }
        public string ur_5 { get; set; }
        public string tafseer_1 { get; set; }
        public string tafseer_2 { get; set; }
        public string tafseer_3 { get; set; }
        public string tafseer_4 { get; set; }
        public string tafseer_5 { get; set; }
        public string extra_1 { get; set; }
        public string extra_2 { get; set; }
        public string extra_3 { get; set; }
        public string englishText { get; set; }
        public int ayatSajdaShafai { get; set; }
        public int ayatSajdaHanafi { get; set; }
        public int surahRuku { get; set; }
        public int paraRuku { get; set; }
        public int manzilNo { get; set; }
    }

}
