import { Component } from '@angular/core';
import { ISkill } from '../../entities/skill';
import { SkillService } from '../../services/skill.service';

@Component({
  selector: 'app-skill-table',
  templateUrl: './skill-table.component.html',
  styleUrl: './skill-table.component.css'
})
export class SkillTableComponent {
  public skills: ISkill[] = [];
  public tableFields: string[] = ['skillId', 'name', 'level', 'description'];

  constructor(private _skillService: SkillService) {
    this._skillService.getSkills()
        .subscribe(data => this.skills = data.data);
  }
}
