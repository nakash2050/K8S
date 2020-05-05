import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/_services/employee.service';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent implements OnInit {

  isRegistered: boolean = false;


  constructor(
    private employeeService: EmployeeService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  addEmployee(form: NgForm): void {
    this.employeeService.addNewEmployee(form.value)
    .subscribe(data => {
      this.isRegistered = data != null;
      form.reset();
    });
  }
}
