import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './_components/home/home.component';
import { AddEmployeeComponent } from './_components/add-employee/add-employee.component';
import { UpdateEmployeeComponent } from './_components/update-employee/update-employee.component';
import { SkillResolver } from './_resolvers/skill.resolver';


const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent
  },
  {
    path: 'add-employee',
    component: AddEmployeeComponent,
    resolve: { 
      skills: SkillResolver 
    }
  },
  {
    path: 'update-employee/:id',
    component: UpdateEmployeeComponent,
    resolve: { 
      skills: SkillResolver 
    }
  },
  {
    path: '',
    component: HomeComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
