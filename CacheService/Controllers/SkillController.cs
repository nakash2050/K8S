using System;
using System.Threading.Tasks;
using CacheService.Cache;
using CacheService.Domain;
using CacheService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CacheService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly ICacheRepository cacheRepository;

        public SkillController(ICacheRepository cacheService)
        {
            this.cacheRepository = cacheService;
        }

        [HttpGet]
        [Cache(600)]
        public async Task<ActionResult<Skill[]>> GetAsync()
        {
            var skills = await cacheRepository.GetAllSkills();
            return Ok(skills);
        }

        [HttpGet("{id}")]
        [Cache(600)]
        public async Task<ActionResult<Skill[]>> GetSkillById(int id)
        {
            var skill = await cacheRepository.GetSkillById(id);

            if (skill is null)
            {
                return NoContent();
            }

            return Ok(skill);
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var skills = new Skill[]
            {
                new Skill{SkillName = "C#"},
                new Skill{SkillName = "Java"},
                new Skill{SkillName = "Python"},
                new Skill{SkillName = "Ruby"},
                new Skill{SkillName = "Perl"},
                new Skill{SkillName = "Go"},
                new Skill{SkillName = "Rust"}
            };

            var count = await cacheRepository.AddSkills(skills);
            return Ok(count);
        }

        [HttpGet("seed")]
        public async Task<IActionResult> SeedDatabase()
        {
            var result = await cacheRepository.SeedDatabase();
            return Ok(result);
        }
    }
}
