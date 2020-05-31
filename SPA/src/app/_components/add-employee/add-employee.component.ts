import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/_services/employee.service';
import { Router, ActivatedRoute } from '@angular/router';
import { NgForm } from '@angular/forms';
import { ISkill } from 'src/app/_models/skill.model';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css']
})
export class AddEmployeeComponent implements OnInit {

  skills: ISkill[];
  isRegistered: boolean = false;


  constructor(
    private employeeService: EmployeeService,
    private activatedRoute: ActivatedRoute
  ) { }

  ngOnInit() {
    this.activatedRoute.data.subscribe(data => {
      this.skills = data.skills;
    });
  }

  addEmployee(form: NgForm): void {
    this.employeeService.addNewEmployee(form.value)
    .subscribe(data => {
      this.isRegistered = data != null;
      form.reset();
    });
  }
}
