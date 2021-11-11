import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { User } from '../shared/user.model';
import { UsersService } from '../shared/users.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  constructor(public userservice: UsersService,private toastr: ToastrService) { }

  ngOnInit(): void {
    this.userservice.refreshList();
  }
  onDelete(id:number){
    if (confirm('Are you sure to delete this record?')) {
      this.userservice.deletePaymentDetail(id)
        .subscribe(
          res => {
            this.userservice.refreshList();
            this.toastr.error("Deleted successfully", 'Payment Detail Register');
          },
          err => { console.log(err) }
        )
    }
  }
  populateForm(form:User){
    this.userservice.formData  = Object.assign({},form);
  }

}
