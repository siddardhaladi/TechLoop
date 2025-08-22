
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormArray, Validators, ReactiveFormsModule } from '@angular/forms';
import { QuestionService, QuestionDto, OptionDto } from '../question.service';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-question-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterModule],
  templateUrl: './question-form.component.html',
  styleUrls: ['./question-form.component.css']
})
export class QuestionFormComponent {
  questionForm: FormGroup;
  successMessage: string = '';
  courses = [
    // { id: 1, name: 'Course 1', assessmentId: 1 },
    // { id: 2, name: 'Course 2', assessmentId: 2 },
    // Add more courses as needed
  ];

  constructor(private fb: FormBuilder, private questionService: QuestionService, private router: Router) {
    this.questionForm = this.fb.group({
      courseId: [null, Validators.required],
      assessmentId: [{ value: '', disabled: true }, Validators.required],
      questionText: ['', Validators.required],
      questionType: ['mcq', Validators.required],
      correctAnswer: ['', Validators.required],
      options: this.fb.array([])  // Only for mcq
    });
  }

  get options(): FormArray {
    return this.questionForm.get('options') as FormArray;
  }

  addOption() {
    this.options.push(this.fb.group({
      text: ['', Validators.required],
      isCorrect: [false]
    }));
  }

  removeOption(index: number) {
    this.options.removeAt(index);
  }

  updateAssessmentId() {
    const selectedCourseId = this.questionForm.get('courseId')?.value;
    this.questionForm.patchValue({ assessmentId: selectedCourseId });
  }

  submit() {
    const formValue: QuestionDto = this.questionForm.getRawValue();
    if (formValue.questionType === 'fill') {
      formValue.options = [];
    }
    this.questionService.postQuestion(formValue).subscribe({
      next: (res) => {
        this.successMessage = 'Question Submitted Successfully';
        this.questionForm.reset();
        this.router.navigate(['/view-questions']);
      }
    });
  }
}
