import { Component } from '@angular/core';

@Component({
  imports: [],
  selector: 'app-category-filter',
  standalone: true,
  templateUrl: './category-filter.html',
  styleUrls: ['./category-filter.css']
})
export class CategoryFilterComponent {
  categories = ['Recommended', 'Coming Soon', 'English', 'Hindi', 'Tamil', 'Telugu', 'Kannada'];
  selected = 'Recommended';

  select(cat: string) {
    this.selected = cat;
  }
}