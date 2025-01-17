import { Component, OnInit } from '@angular/core';
import { PriceListLine } from 'src/app/models/PriceListLine';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { PriceListAdminService } from '../services/price-list-admin.service';

@Component({
  selector: 'app-admin-price-list',
  templateUrl: './admin-price-list.component.html',
  styleUrls: ['./admin-price-list.component.css']
})
export class AdminPriceListComponent implements OnInit {

  public isBtnAddClicked: boolean;
  public isBtnEditClicked: boolean;
  priceList: PriceListLine[];
  retAdd: string;
  message: string;
  messageEdit: string;

  priceListLine: PriceListLine;
  public editForm: FormGroup;
  public addForm: FormGroup;

  TypeTicket:Array<Object> = [
    {name: "One-hour"},
    {name: "Day"},
    {name: "Mounth"},
    {name: "Year"}
  
  ];

  constructor(private fb: FormBuilder,private PriceListAdminService: PriceListAdminService) {

    this.addForm = this.fb.group({
      pricelist: ['',Validators.required],
      ticket: ['',Validators.required],
      price: ['',Validators.required]
    });

    this.editForm = this.fb.group({
      pricelist: [''],
      ticket: ['',Validators.required],
      price: ['',Validators.required]
    });

    this.isBtnAddClicked = false;
    this.isBtnEditClicked = false;
    this.priceList = new Array<PriceListLine>();
    this.priceListLine = new PriceListLine();
    this.message="";
    this.messageEdit="";
   }

  async ngOnInit() {
    this.priceList = await this.PriceListAdminService.getPriceList();
    this.message="";
    this.messageEdit="";
  }

  public async addPrice(){
    this.isBtnAddClicked = true;
  }

  public async onSubmit(){

    // if(this.addForm.controls['ticket'].value == "One-hour"){
    //   this.priceListLine.TypeOfTicket = 0;
    // } else if(this.addForm.controls['ticket'].value == "Day"){
    //   this.priceListLine.TypeOfTicket = 1;
    // } else if(this.addForm.controls['ticket'].value == "Mounth"){
    //   this.priceListLine.TypeOfTicket = 2;
    // } else if(this.addForm.controls['ticket'].value == "Year"){
    //   this.priceListLine.TypeOfTicket = 3;
    // }
    this.priceListLine.TypeOfTicket = this.addForm.controls['ticket'].value;
    this.priceListLine.Value= this.addForm.controls['price'].value;
    this.priceListLine.ValidFrom = this.addForm.controls['pricelist'].value;
    
    this.PriceListAdminService.addPriceListLine(this.priceListLine).subscribe(data=>{
      this.message=data; 
      this.getPriceList();
      }, err => console.log(err));
  }

  public async onSubmitEdit(){

    // if(this.addForm.controls['ticket'].value == "One-hour"){
    //   this.priceListLine.TypeOfTicket = 0;
    // } else if(this.addForm.controls['ticket'].value == "Day"){
    //   this.priceListLine.TypeOfTicket = 1;
    // } else if(this.addForm.controls['ticket'].value == "Mounth"){
    //   this.priceListLine.TypeOfTicket = 2;
    // } else if(this.addForm.controls['ticket'].value == "Year"){
    //   this.priceListLine.TypeOfTicket = 3;
    // }
    //this.priceListLine.TypeOfTicket = this.editForm.controls['ticket'].value;
    this.priceListLine.Value = this.editForm.controls['price'].value;
    this.priceListLine.ValidFrom = this.editForm.controls['pricelist'].value;
    this.priceListLine.TypeOfTicket = this.editForm.controls['ticket'].value;
    this.priceListLine.TypeOfTicket = this.editForm.controls['ticket'].value;
    this.priceListLine.IDPrice = this.editForm.controls['IDPrice'].value;
    this.priceListLine.IDPriceList = this.editForm.controls['IDPriceList'].value;

   let s =  this.PriceListAdminService.editLine(this.priceListLine).subscribe(data=>{
      this.getPriceList();
      this.messageEdit=data;
     
    }, err => console.log(err));
    console.log(this.messageEdit);
    this.isBtnEditClicked = false;

  }

  public async getPriceList(){
    
    if(this.message == "ok"){
      this.isBtnAddClicked = false;
    this.message=" ";
    }
    if(this.messageEdit == "ok"){
      this.isBtnEditClicked = false;
    this.messageEdit=" ";
    }

    this.priceList = await this.PriceListAdminService.getPriceList();
  }

  public  editLine(priceListLine){
    let t=new Date(Date.now()).toLocaleDateString();
    this.isBtnEditClicked = true;
    this.editForm = this.fb.group({
      pricelist: [t],
      ticket: [priceListLine.TypeOfTicket],
      price: [priceListLine.Value],
      IDPrice:[priceListLine.IDPrice],
      IDPriceList:[priceListLine.IDPriceList],

    });
  }

  public deleteLine(priceListLine){
    this.PriceListAdminService.deleteLine(priceListLine).subscribe(()=>{
      this.getPriceList();
     
    }, err => console.log(err));
  }
}
