import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Log } from '../interfaces/log';
import { LogDto } from '../interfaces/logDto';
import { Observable } from 'rxjs';
import { ResponseModel } from '../interfaces/responseModel';
import { environment } from '../../environments/environment';
import { TokenService } from './token.service';
import { AlertDto } from '../interfaces/alertDto';

@Injectable({
  providedIn: 'root'
})
export class LogService {
  baseUrl = `${environment.baseUrl}/Log`;

  constructor(private http: HttpClient,
    private tokenService: TokenService
  ) {
  }

  // headers = this.tokenService.getHeaders();
  // httpOptions = {
  //   headers: this.headers
  // };


  getAllLogs(): Observable<ResponseModel<LogDto>> {
    return this.http.get<ResponseModel<LogDto>>(`${this.baseUrl}/GetAllLogs`);
  }

  updateLog(log: Partial<Log>) {
    return this.http.post<ResponseModel<Log>>(`${this.baseUrl}/UpdateLog`, log);
  }

  getLogById(id: string) {
    return this.http.get<ResponseModel<Log>>(`${this.baseUrl}/getLogById/${id}`);
  }

  deleteLog(id: string) {
    return this.http.post<ResponseModel<Log>>(`${this.baseUrl}/DeleteLog`, { id: id });
  }

  logListen(): Observable<ResponseModel<AlertDto>>{
    return this.http.post<ResponseModel<AlertDto>>(`${this.baseUrl}/LogListen`, {});
  }


}
