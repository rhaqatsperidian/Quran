using Application;
using DbModels;
using Microsoft.AspNetCore.Mvc;

namespace Quran_Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuranDataController : ControllerBase
    {
        private readonly IDatabaseRepository<QuranData> _repository;

        public QuranDataController(IDatabaseRepository<QuranData> repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEntities()
        {
            var allEntities = await _repository.GetRepository().GetAll(10);
            return Ok(allEntities);
        }

        // Other API actions...
    }

}
