using System;
using System.Threading.Tasks;
using CacheService.Domain;

namespace CacheService.Services
{
    public interface ICacheRepository
    {
        Task<Skill[]> GetAllSkills();
        Task<Skill> GetSkillById(int id);
        Task<int> AddSkills(Skill[] skills);
        Task<string> SeedDatabase();
    }
}
