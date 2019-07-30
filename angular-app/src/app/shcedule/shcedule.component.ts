import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Line } from 'src/app/models/line';

@Component({
  selector: 'app-shcedule',
  templateUrl: './shcedule.component.html',
  styleUrls: ['./shcedule.component.css']
})
export class ShceduleComponent implements OnInit {

  public typeOfDayForm: FormGroup;
  public typeOfLineForm: FormGroup;
  public lineForm: FormGroup;
  public ScheduleForm: FormGroup;

  public lines: Line[];
  public times: string;
  public parser:string[];
  public message: string;
  public empty: boolean;


  TypeLine:Array<Object> = [
    {name: "City"},
    {name: "Village"},
];

  TypeDay:Array<Object> = [
    {name: "Work day"},
    {name: "Saturday"},
    {name: "Sunday"},
  ];

  constructor(public fb: FormBuilder) { 
    this.ScheduleForm = this.fb.group({
      line: ['', Validators.required],
      day: ['', Validators.required],
      number: ['', Validators.required]
  
    });
  }

  async ngOnInit() {
    
  }

  public async typeSelected(){
    let typeOfLine = this.ScheduleForm.controls['line'].value;
  }

  public async ScheduleShow(){
  }
}
