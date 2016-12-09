import { NgModule }      from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';
import { Location, LocationStrategy, HashLocationStrategy } from '@angular/common';
import { Headers, RequestOptions, BaseRequestOptions} from '@angular/http';
import { NgbModule, NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

import { AppComponent }  from './app.component';
import { HomeComponent } from './components/home.component';
import { OrganizeComponent } from './components/organize.component';
import { TabBarComponent } from './core/common/tab-bar.component';
import { AboutComponent } from './components/about.component'; 
import { routing } from './routes';
import { HighlightDirective } from './highlight.directive';
import { KeysPipe } from './core/common/keys-pipe'
import { OrganizeAddComponent } from './components/organize-add.component'

//import { DataService } from './core/services/data.service';
//import { MembershipService } from './core/services/membership.service';
//import { UtilityService } from './core/services/utility.service';
//import { NotificationService } from './core/services/notification.service';

class AppBaseRequestOptions extends BaseRequestOptions {
    headers: Headers = new Headers();

    constructor() {
        super();
        this.headers.append('Content-Type', 'application/json');
        this.body = '';
    }
}

@NgModule({
    imports: [
        NgbModule.forRoot(),
        BrowserModule,
        FormsModule,
        HttpModule,
        routing
    ],
    declarations: [AppComponent, KeysPipe, HomeComponent, AboutComponent,
        HighlightDirective, OrganizeComponent, TabBarComponent, OrganizeAddComponent],
    providers: [NgbActiveModal,
        { provide: LocationStrategy, useClass: HashLocationStrategy },
        { provide: RequestOptions, useClass: AppBaseRequestOptions }],
    bootstrap: [AppComponent],
    entryComponents: [OrganizeAddComponent]
})
export class AppModule { }

