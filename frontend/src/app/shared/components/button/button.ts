import { Component, input, output } from '@angular/core';
import { NgClass } from '../../../../../node_modules/@angular/common/types/_common_module-chunk';

@Component({
  imports: [],
  selector: 'app-button',
  styleUrl: './button.css',
  templateUrl: './button.html',
})
export class ButtonComponent  {
  label = input<string>("Button");
  type = input<'default' |  'confirm' | 'cancel'>('default');
  styles = input<string>('');
  clickEvent = output<void>();

  onClick(): void {
    this.clickEvent.emit();
  }
}