import { Directive, ElementRef} from '@angular/core';

@Directive({
  selector: '[appBlueBackground]'
})
export class BlueBackgroundDirective {

  constructor(private _el: ElementRef) {
    this._el.nativeElement.style.backgroundColor = "rgb(54, 54, 244)";
  }

}
