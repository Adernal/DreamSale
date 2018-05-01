import { Injectable } from '@angular/core';


@Injectable()
export class URLService {
    serverUrl:string;

    constructor() {
        this.serverUrl = "https://denmakersapi.azurewebsites.net";
    }





}
