using System.Linq;
using System.Threading.Tasks;
using CacheService.Data;
using CacheService.Domain;
using Microsoft.EntityFrameworkCore;

namespace CacheService.Services
{
    public class CacheRepository : ICacheRepository
    {
        private readonly DataContext dataContext;

        public CacheRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<int> AddSkills(Skill[] skills)
        {
            var skillsInDb = dataContext.Skills.ToList();

            if (skillsInDb.Count == 0)
            {
                skills.ToList().ForEach(skill => {
                    dataContext.Skills.AddAsync(skill);
                });
                
                return await dataContext.SaveChangesAsync();
            }

            return 0;
        }

        public Task<Skill[]> GetAllSkills()
        {
            var skillsInDb = dataContext.Skills.ToList();
            return Task.FromResult(skillsInDb.Count() == 0 ? null : skillsInDb.ToArray());
        }

        public async Task<Skill> GetSkillById(int id)
        {
            var skillInDb = await dataContext.Skills.FindAsync(id);
            return skillInDb;
        }

        public async Task<string> SeedDatabase()
        {
            try
            {
                await dataContext.Database.MigrateAsync();
                return "Done!";
            }
            catch (System.Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
