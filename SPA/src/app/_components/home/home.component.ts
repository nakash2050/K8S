import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/_services/employee.service';
import { EmployeeModel } from 'src/app/_models/employee.model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  employees: EmployeeModel[];

  constructor(
    private employeeService: EmployeeService,
    private router: Router
  ) { }

  ngOnInit() {
    this.employeeService.getAllEmployees()
    .subscribe(data => {
      this.employees = data;
    });
  }

  deleteEmployee(empId: string): void {
    this.employeeService.deleteEmployee(empId)
    .subscribe(isSuccess => {
      if(isSuccess) {
        this.employees = this.employees.filter(emp => emp.employeeId != +empId);
      }
    });
  }

  navigateToUpdate(empId: string): void {
    this.router.navigate(['update-employee', empId]);
  }
}
