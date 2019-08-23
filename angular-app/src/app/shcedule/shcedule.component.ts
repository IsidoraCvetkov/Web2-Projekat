import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Line } from 'src/app/models/line';
import { ScheduleService } from 'src/app/services/schedule.service';
import { ScheduleLine } from '../models/ScheduleLine';

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
  schedule: ScheduleLine[];
  sl : ScheduleLine;
  scheduleLine: ScheduleLine;

  selectedLine;
  selectedDay;
  selectedNumber;

  TypeLine:Array<Object> = [
    {name: "Town"},
    {name: "Suburban"},

  ];

  TypeDay:Array<Object> = [
    {name: "Work day"},
    {name: "Weekend"},

  ];

  constructor( private fb: FormBuilder, private scheduleService: ScheduleService) { 
   
    this.ScheduleForm = this.fb.group({
      line: ['', Validators.required],
      day: ['', Validators.required],
      number: ['', Validators.required]

    });

    this.lines = new Array<Line>();
    this.empty = true;
    this.schedule = new Array<ScheduleLine>();
    this.sl = new ScheduleLine();
  }

  async ngOnInit() {
    
    let typeOfLine = this.ScheduleForm.controls['line'].value;
    
    console.log(this.ScheduleForm.controls['line'].value);
    this.lines = await this.scheduleService.getScheduleLines(typeOfLine);
    //this.schedule = await this.scheduleService.getSchedule(typeOfLine,typeOfDay,Number);
  }

  public async typeSelected(){
    let typeOfLine = this.ScheduleForm.controls['line'].value;
    
    this.lines = await this.scheduleService.getScheduleLines(typeOfLine);
    
  }

  public async ScheduleShow(){
    let typeOfLine = this.ScheduleForm.controls['line'].value;
    let typeOfDay = this.ScheduleForm.controls['day'].value;
    let Number = this.ScheduleForm.controls['number'].value;
    //this.times = await this.scheduleService.getSchedule(typeOfLine,typeOfDay,Number);
    this.schedule = await this.scheduleService.getSchedule(typeOfLine,typeOfDay,Number);
    if(this.schedule.length == 0){
      this.empty = true;
      this.message = "There is no departures for this line and type of day.";
    }
    // }else if(this.times == null){
    //   this.empty = true;
    //   this.message = "Something went wrong, please try again."
    // }
    else{
      this.empty = false;
      this.parser = this.times.split(" ");
      
    }
  }
}
