import { Component, OnInit } from '@angular/core';
import { FileUploadService } from './file-upload.service';
import { FileToUpload } from './file-to-upload';

@Component({
  selector: 'app-file-upload',
  templateUrl: './file-upload.component.html',
  styleUrls: ['./file-upload.component.css']
})
export class FileUploadComponent implements OnInit {

  readonly MAX_SIZE: number = 5368709120;
  theFile: any = null;
  messages: string[] = [];

  constructor(private uploadService: FileUploadService) { }

  ngOnInit(): void {
  }

  onFileChange(event:any) {
    this.theFile = null;
    if (event.target.files && event.target.files.length > 0) {
        // Don't allow file sizes over 1MB
        if (event.target.files[0].size < this.MAX_SIZE) {
            // Set theFile property
            this.theFile = event.target.files[0];
        }
        else {
            // Display error message
            this.messages.push("File: " + event.target.files[0].name + " is too large to upload.");
        }
    }
}


    readAndUploadFile(theFile: any) {
      let file = new FileToUpload();
      
      // Set File Information
      file.Name = theFile.Name;
      file.Size = theFile.Size;
      file.Path = theFile.Path;
      file.Type = theFile.Type;
      file.IsPrivate = theFile.IsPrivate;
      
      // Use FileReader() object to get file to upload
      // NOTE: FileReader only works with newer browsers
      let reader = new FileReader();
      
      // Setup onload event for reader
      reader.onload = () => {

          // POST to server
          this.uploadService.uploadFile(file).subscribe(resp => { 
              this.messages.push("Upload complete"); });
      }
      
      // Read the file
      reader.readAsDataURL(theFile);
      
    }

    uploadFile(): void {
      this.readAndUploadFile(this.theFile);
  }
}
