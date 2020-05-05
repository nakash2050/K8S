import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';
import { EmployeeService } from 'src/app/_services/employee.service';

@Component({
  selector: 'app-update-employee',
  templateUrl: './update-employee.component.html',
  styleUrls: ['./update-employee.component.css']
})
export class UpdateEmployeeComponent implements OnInit {
  updateForm: FormGroup;
  empId: string;

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private router: Router
  ) { }

  ngOnInit() {
    this.empId = this.route.snapshot.params['id'];

    this.updateForm = this.buildUpdateForm();

    this.employeeService.getEmployeeById(this.empId)
      .subscribe(data => {
        this.updateForm.patchValue(data);
      });
  }

  get updateFormControls(): any {
    return this.updateForm.controls;
  }

  buildUpdateForm(): FormGroup {
    return this.fb.group({
      employeeId: [null],
      firstName: [null, Validators.required],
      lastName: [null, Validators.required],
      designation: [null, Validators.required]
    });
  }

  updateEmployeeDetails(): void {
    this.employeeService.updateEmployeeDetaisl(this.empId, this.updateForm.value)
      .subscribe(isUpdated => {
        if (isUpdated) {
          this.router.navigate(['home']);
        }
      });
  }
}
