import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-stream-card',
  standalone: true,
  imports: [],
  templateUrl: './stream-card.html',
  styleUrls: ['./stream-card.css']
})
export class StreamCardComponent {
  @Input() stream: any;
}