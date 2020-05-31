import { Injectable } from '@angular/core';
import { ISkill } from '../_models/skill.model';
import { Resolve } from '@angular/router';
import { ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router'
import { Observable, of } from 'rxjs';
import { EmployeeService } from '../_services/employee.service';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SkillResolver implements Resolve<ISkill[]> {

  constructor(private employeeService: EmployeeService) { }
  
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<ISkill[]> {
    return this.employeeService.getAllSkills()
            .pipe(catchError((error: Response) => {
                return of(null);
            }));
  }
}
