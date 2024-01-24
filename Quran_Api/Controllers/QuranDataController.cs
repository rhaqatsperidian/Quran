using Application;
using Application.Repositories;
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
    }
}
