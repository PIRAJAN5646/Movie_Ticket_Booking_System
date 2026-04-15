import { Component, signal } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-signin',
  standalone: true,
  imports: [RouterLink, FormsModule],
  templateUrl: './signin.html',
  styleUrls: ['./signin.css']
})
export class SignInComponent {
  isLogin = signal(true);
  email = '';
  password = '';
  name = '';
  phone = '';
  showPassword = signal(false);
  isLoading = signal(false);
  errorMessage = signal('');
  successMessage = signal('');

  // Film poster seeds for mosaic background
  posters = [
    'cobra','kingdom','ozark','dark','crown','witcher','avatar','stranger',
    'squid','friends','mindhunter','vikings','westworld','house','breaking','blacklist',
    'suits','power','prison','wire','sopranos','shield','peaky','narcos',
    'dexter','hannibal','fargo','yellowstone','mandalorian','loki','rings','dune'
  ].map(s => `https://picsum.photos/seed/${s}/160/240`);

  constructor(private auth: AuthService, private router: Router) {}

  toggleMode() {
    this.isLogin.update(v => !v);
    this.errorMessage.set('');
    this.successMessage.set('');
    this.email = '';
    this.password = '';
    this.name = '';
    this.phone = '';
  }

  togglePassword() { this.showPassword.update(v => !v); }

  onSubmit() {
    if (!this.email || !this.password) {
      this.errorMessage.set('Please fill in all required fields.');
      return;
    }
    this.errorMessage.set('');
    this.successMessage.set('');
    this.isLoading.set(true);

    if (this.isLogin()) {
      this.auth.login(this.email, this.password).subscribe({
        next: () => {
          this.isLoading.set(false);
          this.router.navigate(['/']);
        },
        error: (err) => {
          this.isLoading.set(false);
          this.errorMessage.set(err?.error?.message ?? 'Invalid email or password. Please try again.');
        }
      });
    } else {
      if (!this.name) { this.errorMessage.set('Please enter your full name.'); this.isLoading.set(false); return; }
      this.auth.register(this.name, this.email, this.password, this.phone).subscribe({
        next: () => {
          this.isLoading.set(false);
          this.successMessage.set('🎉 Account created! You received ₹1,000 wallet credits. Redirecting...');
          setTimeout(() => this.router.navigate(['/']), 2000);
        },
        error: (err) => {
          this.isLoading.set(false);
          this.errorMessage.set(err?.error?.message ?? 'Registration failed. Please try again.');
        }
      });
    }
  }
}
