﻿import { Inject, Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';
import { ApiRoutesService, Routes, ApiRoutes } from './apiRoutes.service';
import { AuthRequestHandler } from './requestHandler';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { MemeModel, MemeState, MemeReaction } from '../models/memes';
import { AuthGuard } from '../auth/auth.guard';

@Injectable()
export class BgcMemeService extends AuthRequestHandler {

	constructor(http: Http, @Inject('BASE_URL') baseUrl: string, auth: AuthGuard,
		private router: ApiRoutesService)
	{
		super(http, baseUrl, auth);
	}

	public setUserMemeReaction(reaction: MemeReaction): Observable<MemeState> {
		return this.authPost<MemeReaction, MemeState>(ApiRoutes.MemeReaction, reaction);
	}

	public getMemePage(pageIndex: number): Observable<MemeModel[]> {
		let userId = this.auth.getLoggedUserIds().id;
		return this.get<MemeModel[]>(ApiRoutes.PageMemes + `/${pageIndex}/${userId}`);
	}

	public getNewMemeCount(pageIndex: number, memes: MemeModel[]): Observable<number> {
		let firstMemeId = memes[0].core.id;
		return this.get<ItemCount>(ApiRoutes.CountNewMemes + `/${pageIndex}/${firstMemeId}`)
			.map(r => r.count);
	}
}

interface ItemCount {
	count: number;
}