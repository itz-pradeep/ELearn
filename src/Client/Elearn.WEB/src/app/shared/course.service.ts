import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { min, Subject } from 'rxjs';
import { ICourse } from './models/course';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  baseUrl = "http://localhost:8000/api/v1.0/lms/courses/";
  coursesFetched  = new Subject<ICourse[]>();
  constructor(private http: HttpClient) { 
  }

  getAllCourses(){
    console.log(this.baseUrl + "getall");
    return this.http.get<ICourse[]>(this.baseUrl + "getall");
  }

  getCoursesByFilter(technology?:string,durationFromRange?:number,durationToRange?:number){
    const filter = {
      technology: technology == null ? '' : technology,
      durationFromRange: durationFromRange == null ? 0 : durationFromRange,
      durationToRange:durationToRange == null ? 0 : durationToRange
    }
    return this.http.post<ICourse[]>(this.baseUrl + "info",
    filter);
  }

}
