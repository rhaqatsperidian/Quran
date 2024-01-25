using Application;
using Application.Repositories.Interfaces;
using DbModels;
using Microsoft.AspNetCore.Mvc;

namespace Quran_Api.Controllers
{
    /// <summary>
    /// Controller for handling Quran data and Surah-related HTTP requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class QuranDataController : ControllerBase
    {
        private readonly IDatabaseRepository<QuranData> _repo;   // Repository for QuranData entity
        private readonly IQuranRepo _quranDataRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuranDataController"/> class.
        /// </summary>
        /// <param name="repository">Repository for QuranData entity.</param>
        /// <param name="surahRepo">Repository for Surah entity.</param>
        public QuranDataController(IDatabaseRepository<QuranData> repository, IQuranRepo quranDataRepo)
        {
            _repo = repository;
            _quranDataRepo = quranDataRepo;
        }

        /// <summary>
        /// Gets a list of QuranData entities.
        /// </summary>
        /// <returns>An action result containing the list of QuranData entities.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllQuranData()
        {
            // Retrieve QuranData entities from the repository with a limit of 10.
            var allEntities = await _repo.GetRepository().GetAll(10);

            // Return a 200 OK response with the retrieved entities.
            return Ok(allEntities);
        }

        /// <summary>
        /// Gets a list of Surah names.
        /// </summary>
        /// <returns>An action result containing the list of Surah names.</returns>
        [HttpGet("SurahList")]
        public async Task<IActionResult> GetSurahList()
        {
            // Extract Surah names from the entities.
            var surahNames = await _quranDataRepo.GetSurahList();

            // Return a 200 OK response with the list of Surah names.
            return Ok(surahNames);
        }

        /// <summary>
        /// Retrieves Quran data for a specific Surah based on the provided Surah ID.
        /// </summary>
        /// <param name="surahId">The ID of the Surah for which Quran data is requested.</param>
        /// <returns>An action result containing the Quran data for the specified Surah.</returns>
        [HttpGet("SurahText/{surahId}")]
        public async Task<IActionResult> GetQuranDataBySurah(int surahId)
        {
            // Retrieve Quran data for the specified Surah ID from the repository
            var surahData = await _quranDataRepo.GetQuranDataBySurah(surahId);

            // Return a 200 OK response with the Quran data for the specified Surah
            return Ok(surahData);
        }
    }
}
