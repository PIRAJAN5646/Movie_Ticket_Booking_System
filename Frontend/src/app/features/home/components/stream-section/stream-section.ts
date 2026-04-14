import { Component } from '@angular/core';
import { StreamCardComponent } from '../stream-card/stream-card';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-stream-section',
  standalone: true,
  imports: [StreamCardComponent,CommonModule],
  templateUrl: './stream-section.html',
  styleUrls: ['./stream-section.css']
})
export class StreamSectionComponent {
  streams = [1,2,3,4];
}