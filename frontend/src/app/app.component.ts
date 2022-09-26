import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  editor = new FormGroup({
    taskId : new FormControl(),
    projectId : new FormControl(),
    taskTitle : new FormControl(),
    taskDescription: new FormControl(),
    totalEstimate : new FormControl(),
    employees : this.fb.array([]),
  });

  project_options;
  employee_options;
  
  employees = [];


  constructor(private http:HttpClient, private fb:FormBuilder){

    this.http.get(this.api('projecttask/getprojects')).subscribe(s=>{
      this.project_options = s;
    })

    
    this.http.get(this.api('projecttask/getemployees')).subscribe(s=>{
      this.employee_options = s;
    })
    
    this.employees.push({});

  }

  
  api(resource){
    return 'https://localhost:7123/' + resource;
  }

  setRecord(id){

    
  this.total_estimate_validation ='';
    this.employees = null;
    this.http.get(this.api('projecttask/gettask?id='+id)).subscribe((s:any)=>{

      this.editor.patchValue(s);

      this.employees = s.employees;

    })
  }

  add(){

    this.employees.push({

    });
  }

  clear(){
    
    this.employees = null;
    this.employees = [{}];
    this.editor.reset({});

  }

  delete(index){
    this.employees.splice(index,1);
    if(this.employees.length==0)
      this.employees.push({});
  }

  
  total_estimate_validation;

  totalEstimate(){

    if(!this.employees) return 0;
    let total = 0;
    this.employees.forEach(o=> {
      let t = parseInt(o.estimatedHours);
      total += isNaN(t)? 0 : t;
    });
    



    return total;
  }


  save(){

    this.total_estimate_validation = '';
    
    if(this.editor.invalid)
      return alert('Please supply required fields.');
    
    let data = this.editor.value;
    let total_estimate = this.totalEstimate();

    if(data.totalEstimate != total_estimate)
      return this.total_estimate_validation = "Total estimate is not equal to the line employees total estimated hours.";
    
    data.employees = this.employees;
    
    this.http.post(this.api('projecttask/savetask'),data).subscribe((s:any)=>{
      this.setRecord(s.taskId);
    })
  }

}
