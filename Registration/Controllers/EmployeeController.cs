using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Registration.Business.Contracts;
using Registration.DTO;

namespace Registration.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddNewEmployee(AddEmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await employeeRepository.AddNewEmployee(employeeDTO);
                return CreatedAtRoute(nameof(GetEmployeeById), new { id = result.EmployeeId }, result);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, AddEmployeeDTO employeeDTO)
        {
            if (ModelState.IsValid)
            {
                var result = await employeeRepository.UpdateEmployeeDetails(id, employeeDTO);
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var result = await employeeRepository.GetAllEmployees();
            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            var result = await employeeRepository.GetEmployeeDetail(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await employeeRepository.DeleteEmployee(id);
            return Ok(result);
        }
    }
}
