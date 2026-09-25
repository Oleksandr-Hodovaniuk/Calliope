import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SHARED_IMPORTS } from './shared/shared.imports';


@Component({
  imports: [RouterOutlet, SHARED_IMPORTS],
  selector: 'app-root',
  styleUrl: './app.css',
  templateUrl: './app.html',
})
export class App {
}
