import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';

@Component({
  imports: [CommonModule],
  selector: 'app-category-filter',
  standalone: true,
  templateUrl: './category-filter.html',
  styleUrls: ['./category-filter.css']
})
export class CategoryFilterComponent {
  categories = ['Recommended', 'Coming Soon', 'English', 'Hindi'];
  selected = 'Recommended';
}