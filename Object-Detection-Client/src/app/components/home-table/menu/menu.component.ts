import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-menu',
  templateUrl: './menu.component.html',
  styleUrls: ['./menu.component.scss']
})
export class MenuComponent {
  @Output() selected = new EventEmitter<string>();
  selectedOption: string = ''; // Seçilen menü seçeneğini saklamak için

  selectOption(option: string) {
    this.selectedOption = option; // Seçilen seçeneği belirlemek için
    this.selected.emit(option); // Ana bileşene seçeneği iletmek için 
  }
}