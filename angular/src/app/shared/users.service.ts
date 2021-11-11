import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from './user.model';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  readonly baseURL = 'http://localhost:61236/api/User'
  formData: User = new User();
  list: User[];
  constructor(private http: HttpClient) { }
  postPaymentDetail() {
    return this.http.post(this.baseURL, this.formData);
  }

  putPaymentDetail() {
    console.log(this.formData);
    return this.http.put(`${this.baseURL}/${this.formData.userID}`, this.formData);
  }

  deletePaymentDetail(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }

  refreshList() {
    this.http.get(this.baseURL)
      .toPromise()
      .then(res =>this.list = res as User[]);
  }
}
