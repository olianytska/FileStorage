import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FileToUpload } from './file-to-upload';

@Injectable({
  providedIn: 'root'
})

export class FileUploadService {
  readonly API_URL = "http://localhost:44303/api/home/Upload";
  readonly httpOptions = {
      headers: new HttpHeaders({
          'Content-Type': 'application/json',
          'X-Requested-With': 'XMLHttpRequest',
          'MyClientCert': '',        // This is empty
          'MyToken': ''              // This is empty
      })
  };
  constructor(private http: HttpClient) { }

  uploadFile(theFile: FileToUpload) : Observable<any> {
    return this.http.post<FileToUpload>(this.API_URL, theFile, this.httpOptions);
}
}
