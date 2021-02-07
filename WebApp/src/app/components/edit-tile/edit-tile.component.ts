import {
  AfterViewInit,
  Component,
  ElementRef,
  EventEmitter,
  Input,
  Output,
  ViewChild,
} from '@angular/core';

@Component({
  selector: 'app-edit-tile',
  templateUrl: './edit-tile.component.html',
  styleUrls: ['./edit-tile.component.scss'],
})
export class EditTileComponent implements AfterViewInit {
  @Input() readonly: boolean = false;
  @Input() fontSize: string | number = '16px';
  @Input() padding: string | number = '10px';
  @Input() textAlign: string = 'center';
  @Input() border: boolean = true;
  @Input() content: string = '';
  @Input() isEditing = false;
  @Output() contentChange = new EventEmitter<string>();
  @Output() remove = new EventEmitter<any>();
  @Output() activate = new EventEmitter<any>();

  @ViewChild('iContent') iInputRef!: ElementRef<any>;

  confirmRemoval = false;
  confirmRemovalTimer: any;

  constructor() {}

  ngAfterViewInit() {
    if (this.isEditing) {
      this.onEdit(undefined, true);
    }
  }

  onClick(event: Event) {
    if (!this.isEditing) this.activate.emit();
  }

  onInputKeyPress(event: KeyboardEvent) {
    if (this.isEditing && event.key === 'Enter') {
      this.onEditStop();
    }
  }

  onEdit(event?: Event, overrideCheck?: boolean) {
    if (event) {
      event.stopPropagation();
      event.preventDefault();
    }
    if (this.isEditing && !overrideCheck) return;
    this.isEditing = true;
    this.iInputRef.nativeElement.select();
    this.iInputRef.nativeElement.focus();
  }

  onEditStop(event?: Event) {
    if (event) {
      event.stopPropagation();
      event.preventDefault();
    }
    this.isEditing = false;
    this.iInputRef.nativeElement.selectionStart = 0;
    this.iInputRef.nativeElement.selectionEnd = 0;
    this.changeContent(this.content);
  }

  onRemove(event?: Event) {
    if (event) {
      event.stopPropagation();
      event.preventDefault();
    }
    if (!this.confirmRemoval) {
      this.confirmRemoval = true;
      this.confirmRemovalTimer = setTimeout(
        () => (this.confirmRemoval = false),
        4000
      );
    } else {
      if (this.confirmRemovalTimer) {
        clearTimeout(this.confirmRemovalTimer);
        this.confirmRemovalTimer = null;
      }
      this.remove.emit();
    }
  }

  changeContent(v: string) {
    this.content = v;
    this.contentChange.emit(v);
  }
}
