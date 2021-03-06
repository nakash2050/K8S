import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmployeeModel } from '../_models/employee.model';
import { SettingsService } from './settings.service';
import { ISkill } from '../_models/skill.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  baseApiUrl: string;

  constructor(private httpClient: HttpClient, private settingsService: SettingsService) { 
    this.baseApiUrl = this.settingsService.settings.baseApiUrl;
  }

  addNewEmployee(data: any): Observable<boolean> {
    data.skillId = +data.skillId;
    return this.httpClient.post<boolean>(this.baseApiUrl + 'employee/add', data);
  }

  getAllEmployees(): Observable<EmployeeModel[]> {
    return this.httpClient.get<EmployeeModel[]>(this.baseApiUrl + 'employee');
  }

  deleteEmployee(empId: string): Observable<boolean> {
    return this.httpClient.delete<boolean>(this.baseApiUrl + 'employee/' + empId);
  }

  getEmployeeById(empId: string): Observable<EmployeeModel> {
    return this.httpClient.get<EmployeeModel>(this.baseApiUrl + 'employee/' + empId);
  }

  updateEmployeeDetails(empId: string, empData: EmployeeModel): Observable<boolean> {
    return this.httpClient.put<boolean>(this.baseApiUrl + 'employee/' + empId, empData);
  }

  getAllSkills(): Observable<ISkill[]> {
    return this.httpClient.get<ISkill[]>(this.baseApiUrl + 'employee/skills');
  }
}
