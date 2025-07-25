import { Component, OnInit } from '@angular/core';
import { MenuCardComponent } from '../../components/menu-card/menu-card.component';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-menu-page',
  imports: [MenuCardComponent, CommonModule, RouterOutlet],
  templateUrl: './menu-page.component.html',
  styleUrl: './menu-page.component.scss',
})
export class MenuPageComponent implements OnInit {
  menuItems = [
    {
      id: 1,
      image: 'https://example.com/image1.jpg',
      name: 'Item 1',
      description: 'This is item 1',
      price: 10.99,
    },
    {
      id: 2,
      image: 'https://example.com/image2.jpg',
      name: 'Item 2',
      description: 'This is item 2',
      price: 9.99,
    },
    {
      id: 3,
      image: 'https://example.com/image3.jpg',
      name: 'Item 3',
      description: 'This is item 3',
      price: 12.99,
    },
    {
      id: 4,
      image: 'https://example.com/image4.jpg',
      name: 'Item 4',
      description: 'This is item 4',
      price: 11.99,
    },
  ];
  role: string = 'admin';

  constructor() {}

  ngOnInit(): void {}

  showMoreInfo(item: any): void {
    console.log('Show more info:', item);
  }

  addRemoveItem(item: any): void {
    console.log('Add/Remove item:', item);
  }

  editItem(item: any): void {
    console.log('Edit item:', item);
  }

  removeItem(item: any): void {
    console.log('Remove item:', item);
  }
}
