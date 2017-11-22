﻿import { Component, Input } from '@angular/core';
import { trigger, state, style, animate, transition } from '@angular/animations';
import { ApiRoutesService } from '../../services/apiRoutes.service';
import { IUserId } from '../../models/account';

@Component({
	selector: 'bgc-quick-user-actions',
	templateUrl: './bgcQuickUserActions.html',
	styleUrls: ['bgcQuickUserActions.css'],
	animations: [
		trigger('rollOut', [
			state('rolled',
				style({
					transform: 'rotateX(0)'
				})
			),
			state('hidden',
				style({
					transform: 'rotateX(-90deg)'
				})
			),
			transition('rolled => hidden', animate('200ms linear')),
			transition('hidden => rolled', animate('250ms linear'))
		])
	]
})

export class BgcQuickUserActionsControl {
	@Input() public items: QuickActionItem[];
	@Input() public userName: string;
	private rolledOut: boolean = false;
	private listState: string = 'hidden';

	constructor(private router: ApiRoutesService) {
		
	}

	rollOut() {
		this.rolledOut = !this.rolledOut;
		this.listState = this.rolledOut ? 'rolled' : 'hidden';
	}

	/**
	 * Invokes action if present and performs routing, if allowed by action.
	 * @param index index of selected item
	 */
	onSelect(index: number) {
		let item = this.items[index];

		if (item.action) {
			let redirect = item.action();
			if (!redirect)
				return;
		}
		this.router.redirect(item.route);
	}
}

export class QuickActionItem {
	public name: string;
	public route: string;
	public action: (() => boolean)|undefined;

	constructor(name: string, route: string, action?: () => boolean) {
		this.name = name;
		this.route = route;
		this.action = action;
	}
}