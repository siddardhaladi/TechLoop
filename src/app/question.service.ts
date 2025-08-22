import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface OptionDto {
  text: string;
  isCorrect: boolean;
}

export interface QuestionDto {
  questionId?: number;
  assessmentId: number;
  questionText: string;
  questionType: string; // 'mcq' or 'fill'
  correctAnswer: string;
  options?: OptionDto[];
}

@Injectable({
  providedIn: 'root'
})
export class QuestionService {
  private baseUrl = 'https://localhost:7219/api/Questions';

  constructor(private http: HttpClient) {}
  

  getQuestionsByAssessmentId(assessmentId: number): Observable<QuestionDto[]> {
        return this.http.get<QuestionDto[]>(`${this.baseUrl}/GetByAssessment/${assessmentId}`);
      }
    

  postQuestion(dto: QuestionDto): Observable<String> {
    return this.http.post(`${this.baseUrl}/Post`, dto,{responseType:'text'});
  }
}
