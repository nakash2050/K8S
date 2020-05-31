using System.Threading.Tasks;
using Registration.DTO;

namespace Registration.Business.Contracts
{
    public interface IEmployeeRepository
    {
        Task<EmployeeDTO> AddNewEmployee(AddEmployeeDTO employeeDTO);

        Task<bool> UpdateEmployeeDetails(int employeeId, AddEmployeeDTO employeeDTO);

        Task<EmployeeDTO> GetEmployeeDetail(int id);

        Task<EmployeeDTO[]> GetAllEmployees();

        Task<bool> DeleteEmployee(int id);

        Task<string> SeedDatabase();

        Task<SkillDTO[]> GetAllSkills();
    }
}
