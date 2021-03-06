﻿using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Registration.Business.Contracts;
using Registration.Data;
using Registration.Domain;
using Registration.DTO;
using Registration.Services;
using RestEase;

namespace Registration.Business.Concretes
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;

        public EmployeeRepository(DataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public async Task<EmployeeDTO> AddNewEmployee(AddEmployeeDTO employeeDTO)
        {
            var employee = mapper.Map<Employee>(employeeDTO);

            dataContext.Add(employee);
            await dataContext.SaveChangesAsync();

            var employeeInDb = mapper.Map<EmployeeDTO>(employee);

            return employeeInDb;
        }

        public async Task<bool> DeleteEmployee(int id)
        {
            var employeeInDb = await dataContext.Employees.FindAsync(id);

            if (employeeInDb != null)
            {
                dataContext.Employees.Remove(employeeInDb);
                return await dataContext.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<EmployeeDTO[]> GetAllEmployees()
        {
            var employeesInDb = await dataContext.Employees.ToListAsync();

            if (employeesInDb != null)
            {
                var cacheServiceUrl = Environment.GetEnvironmentVariable("CACHE_SERVICE_URL");
                var cacheApi = RestClient.For<ICacheApi>($"{cacheServiceUrl}/skill");

                var skills = await cacheApi.GetAsync();

                var employeesDTO = mapper.Map<EmployeeDTO[]>(employeesInDb);

                employeesDTO.ToList().ForEach(emp =>
                {
                    emp.SkillName = skills.FirstOrDefault(skill => skill.Id == emp.SkillId).SkillName;
                });

                return employeesDTO;
            }

            return null;
        }

        public async Task<EmployeeDTO> GetEmployeeDetail(int id)
        {
            var employeeInDb = await dataContext.Employees.FindAsync(id);

            if (employeeInDb != null)
            {
                var skillDTO = mapper.Map<EmployeeDTO>(employeeInDb);

                return skillDTO;
            }

            return null;
        }

        public async Task<bool> UpdateEmployeeDetails(int employeeId, AddEmployeeDTO employeeDTO)
        {
            var employeeInDb = await dataContext.Employees.FindAsync(employeeId);

            if (employeeInDb != null)
            {
                mapper.Map(employeeDTO, employeeInDb);
                return await dataContext.SaveChangesAsync() > 0;
            }

            return false;
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

        public async Task<SkillDTO[]> GetAllSkills()
        {
            var cacheServiceUrl = Environment.GetEnvironmentVariable("CACHE_SERVICE_URL");
            ICacheApi cacheApi = RestClient.For<ICacheApi>($"{cacheServiceUrl}/skill");
            var skills = await cacheApi.GetAsync();
            return skills;
        }
    }
}
