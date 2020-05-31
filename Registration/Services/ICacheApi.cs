using System.Threading.Tasks;
using Registration.DTO;
using RestEase;

namespace Registration.Services
{
    [SerializationMethods(Query = QuerySerializationMethod.Serialized)]
    public interface ICacheApi
    {
        [AllowAnyStatusCode]
        [Get("")]
        Task<SkillDTO[]> GetAsync();

        [AllowAnyStatusCode]
        [Get("{id}")]
        Task<SkillDTO> GetSkillByIdAsync([Path]int id);
    }
}
