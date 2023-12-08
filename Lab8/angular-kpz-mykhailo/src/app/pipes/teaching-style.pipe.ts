import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'teachingStyle'
})
export class TeachingStylePipe implements PipeTransform {

  transform(value: string): string {
    // Check if there are two or more capital letters
    const hasTwoOrMoreCapitalLetters = value.match(/[A-Z].*[A-Z]/);

    // If yes, replace them with a '-' in between
    if (hasTwoOrMoreCapitalLetters) {
      return value.replace(/([a-z])([A-Z])/g, '$1-$2');
    }

    // If not, return the original value
    return value;
  }

}
