import { Component, signal, HostListener } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './header.html',
  styleUrl: './header.css',
})
export class Header {
  city = signal('Mumbai');
  cities = ['Mumbai', 'Delhi', 'Bangalore', 'Chennai', 'Hyderabad', 'Pune', 'Kolkata'];
  showCityDropdown = signal(false);
  showUserMenu = signal(false);

  constructor(public auth: AuthService) {}

  // Close dropdowns when clicking outside
  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent) {
    const target = event.target as HTMLElement;
    if (!target.closest('.city-btn-wrap')) {
      this.showCityDropdown.set(false);
    }
    if (!target.closest('.user-menu-wrap')) {
      this.showUserMenu.set(false);
    }
  }

  selectCity(city: string) {
    this.city.set(city);
    this.showCityDropdown.set(false);
  }

  toggleCityDropdown(event: Event) {
    event.stopPropagation();
    this.showCityDropdown.update(v => !v);
    this.showUserMenu.set(false);
  }

  toggleUserMenu(event: Event) {
    event.stopPropagation();
    this.showUserMenu.update(v => !v);
    this.showCityDropdown.set(false);
  }

  logout() {
    this.showUserMenu.set(false);
    this.auth.logout();
  }

  get walletDisplay(): string {
    const bal = this.auth.currentUser()?.walletBalance ?? 0;
    return '₹' + bal.toLocaleString('en-IN');
  }
}
