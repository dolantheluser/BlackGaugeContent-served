﻿import { Component, Input } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { UserService } from '../../services/user.service';
import { BgcMemeService } from '../../services/bgcMeme.service';
import { MemeModel, Meme, MemeState, MemeReaction } from '../../models/memes'


@Component({
	selector: 'meme-wrapping',
	templateUrl: './memeWrapping.html',
	styleUrls: ['./memeWrapping.css', '../../controls/bgcButtons.css'],
	providers: [BgcMemeService]
})

export class MemeComponent {
	meme: Meme;
	memeState: MemeState;
	voteUnit = 1;
	vote: number;
	hasVoted = false;
	lastVote: boolean;
	blockVote: boolean = false;

	@Input()
	public set Meme(meme: MemeModel) {
		this.meme      = meme.core;
		this.memeState = meme.state;
		this.vote = meme.state.vote;
		if (this.vote !== 0) {
			this.hasVoted = true;
			this.lastVote = this.vote > 0;
		}
	}

	constructor(private _sanitizer: DomSanitizer, private userService: UserService,
		private memeService: BgcMemeService) {
		
	}

	public getMemeSrc(): any {
		return this._sanitizer.bypassSecurityTrustUrl(this.meme.base64);
	}

	voted(nicey: boolean) {
		if (this.userService.isLoggedIn() === false || this.blockVote)
			return;

		if (this.hasVoted === false) {
			this.vote = nicey ? this.voteUnit : -this.voteUnit;
			this.hasVoted = true;
		}
		else
		{
			if (this.lastVote === nicey) {
				this.vote = 0;
				this.hasVoted = false;
			}
			else {
				this.vote = nicey ? this.voteUnit : -this.voteUnit;
			}
		}
		this.lastVote = nicey;
		this.blockVote = true;
		let reaction =
			new MemeReaction(this.userService.getUserIds().id, this.meme.id, this.vote);
		this.memeService.setUserMemeReaction(reaction, data => {
				this.memeState = data;
				this.blockVote = false;
			});
	}

}