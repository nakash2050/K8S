import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { EmployeeModel } from '../_models/employee.model';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  constructor(private httpClient: HttpClient) { }

  addNewEmployee(data: any): Observable<boolean> {
    return this.httpClient.post<boolean>(environment.baseApiUrl + 'employee/add', data);
  }

  getAllEmployees(): Observable<EmployeeModel[]> {
    return this.httpClient.get<EmployeeModel[]>(environment.baseApiUrl + 'employee');
  }

  deleteEmployee(empId: string): Observable<boolean> {
    return this.httpClient.delete<boolean>(environment.baseApiUrl + 'employee/' + empId);
  }

  getEmployeeById(empId: string): Observable<EmployeeModel> {
    return this.httpClient.get<EmployeeModel>(environment.baseApiUrl + 'employee/' + empId);
  }

  updateEmployeeDetaisl(empId: string, empData: EmployeeModel): Observable<boolean> {
    return this.httpClient.put<boolean>(environment.baseApiUrl + 'employee/' + empId, empData);
  }
}
