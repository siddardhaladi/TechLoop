import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { QuestionService } from '../question.service';
import { QuestionDto } from '../question.service'; // adjust path as needed
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NavbarComponent } from '../navbar/navbar.component';



//import { QuestionListComponent} from '../question.service';
@Component({
  selector: 'app-question-list',
  imports:[CommonModule,FormsModule,NavbarComponent],
  templateUrl: './question-list.component.html',
  styleUrls: ['./question-list.component.css']
})
export class QuestionListComponent implements OnInit {
  questions: any[] = [];
  score:number | undefined;
  // assessmentId: number=2;

  assessmentId: number | null = null;

q: any;
opt: any;

  constructor(
    private route: ActivatedRoute,
    private questionService: QuestionService
  ) {}

  //  ngOnInit(): void {
  //    this.getQuestions();
  //   }
  
  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('assessmentId');
      this.assessmentId = id ? +id : null;
      if (this.assessmentId) {
        this.getQuestions();
      }
    });
  }
  getQuestions(): void {
    if (this.assessmentId !== null) {
      this.questionService.getQuestionsByAssessmentId(this.assessmentId).subscribe({
        next: (data: QuestionDto[]) => {
          this.questions = data;
        },
        error: (error) => {
          console.error('Error fetching questions:', error);
        }
      });
    }
  }
  


    
// getQuestions(): void {
//     this.questionService.getQuestionsByAssessmentId(this.assessmentId).subscribe({
//       next: (data: QuestionDto[]) => {
//         this.questions = data;
//       },
//       error: (error) => {
//         console.error('Error fetching questions', error);
//       },
//       complete: () => {
//         console.log('Request complete');
//       }
//     });
//   }



allQuestionsAnswered(): boolean {
      return this.questions.every(q => q.userAnswer !== undefined && q.userAnswer !== '');
    }
  
     submitQuiz(): void {
       this.score = this.questions.reduce((total, q) => {
         if (q.questionType === 'fill' && q.userAnswer === q.correctAnswer) {
           return total + 1;
        } else if (q.questionType === 'mcq' && q.options?.some((opt: { text: any; isCorrect: any; }) => opt.text === q.userAnswer && opt.isCorrect)) {
           return total + 1;
        }
       return total;
   }, 0);
   }
}