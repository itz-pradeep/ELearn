import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { CourseService } from 'src/app/shared/course.service';
import { ICourse } from 'src/app/shared/models/course';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.css']
})
export class CoursesComponent implements OnInit {
  searchForm!: FormGroup;
  courses: ICourse[] = [];
  constructor(private courseService: CourseService) { }

  ngOnInit(): void {
    this.setSearchForm();
    this.setGrid();
  }

  setSearchForm() {
    this.searchForm = new FormGroup({
      technology: new FormControl(null),
      durationFromRange: new FormControl(null),
      durationToRange: new FormControl(null)
    });
  }
  setGrid() {
    
    this.getCourses('',0,0);
    this.courseService.coursesFetched.subscribe({
      next : (data) => {
        console.log(data);
        this.courses = data
      },
      error: (e) => {
        console.log(e)
      }
    });
  }

  searchCourses(){
    const technology = this.searchForm.value.technology;
    const durationFromRange = this.searchForm.value.durationFromRange;
    const durationToRange = this.searchForm.value.durationToRange;

    if(durationFromRange > durationToRange){
      alert("Duration From cannot be greater than duration To.");
      return;
    }

    this.getCourses(technology,durationFromRange,durationToRange);
  }

  getCourses(technology:string,durationFromRange:number,durationToRange:number){
    this.courseService.getCoursesByFilter(technology,durationFromRange,durationToRange)
    .subscribe({
      next: (lp:ICourse[]) => {
        console.log(lp);
        this.courseService.coursesFetched.next(lp);
       
      },
      error : (e) => {
        console.log(e);
      }
    });
  }

}
