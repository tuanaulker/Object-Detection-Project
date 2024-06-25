import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ResponseModel } from '../interfaces/responseModel';
import { UserOperationDto } from '../interfaces/userOperationDto';
import { AddUser } from '../interfaces/add-user';

@Injectable({
  providedIn: 'root'
})
export class UserService {


  baseUrl = `${environment.baseUrl}/User`;
  constructor(private http: HttpClient) { 

  }

  getAllUsers(): Observable<ResponseModel<UserOperationDto>> {
    return this.http.get<ResponseModel<UserOperationDto>>(`${this.baseUrl}/GetAllUsers`);
  }

  addUser(newUser: AddUser): Observable<ResponseModel<any>> {
    debugger
    return this.http.post<ResponseModel<any>>(`${this.baseUrl}/AddUser`, newUser);
  }

  updateUser(updatedUser: UserOperationDto): Observable<ResponseModel<any>> {
    return this.http.post<ResponseModel<any>>(`${this.baseUrl}/UpdateUser`, updatedUser);
  }
  
 
  deleteUser(userId: string): Observable<ResponseModel<any>> {
    return this.http.post<ResponseModel<any>>(`${this.baseUrl}/DeleteUser`, { userId });
  }
}
