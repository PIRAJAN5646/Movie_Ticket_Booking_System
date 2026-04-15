import { Component } from '@angular/core';
import { StreamCardComponent } from '../stream-card/stream-card';

@Component({
  selector: 'app-stream-section',
  standalone: true,
  imports: [StreamCardComponent],
  templateUrl: './stream-section.html',
  styleUrls: ['./stream-section.css']
})
export class StreamSectionComponent {
  streams = [
    { id: 1, title: 'Oppenheimer', platform: 'Prime Video', imageUrl: 'https://picsum.photos/seed/opp/220/300', badge: 'NEW' },
    { id: 2, title: '1Rs Film', platform: 'Netflix', imageUrl: 'https://picsum.photos/seed/1rs/220/300', badge: 'HOT' },
    { id: 3, title: 'Royal Hunt', platform: 'Disney+', imageUrl: 'https://picsum.photos/seed/royal/220/300', badge: 'NEW' },
    { id: 4, title: 'Iron Wall', platform: 'Apple TV+', imageUrl: 'https://picsum.photos/seed/iron/220/300', badge: '' },
    { id: 5, title: 'Biome', platform: 'ZEE5', imageUrl: 'https://picsum.photos/seed/biome/220/300', badge: 'NEW' },
  ];
}