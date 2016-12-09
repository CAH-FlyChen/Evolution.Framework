import { Component, Input, OnInit } from '@angular/core';
import { Router, ActivatedRoute, UrlSegment, NavigationEnd } from '@angular/router';



@Component({
    selector: 'tab-bar',
    templateUrl: './app/core/common/tab-bar.component.html'
})
export class TabBarComponent{
    tabItems: TabBarItem[] =[];
    @Input() selectedUrlObj: any;
    @Input('selectedUrlObj') set changeSelectedUrlObj(val: any) {
        if (!val) return;
        if (!this.existUrl(val.UrlAddress)) {
            this.tabItems.push({ url: val.UrlAddress, title: val.FullName, isSelected: true });
        }
        this.tabSelectedChangeByUrl(val.UrlAddress);
    }
    
    constructor(
        private router: Router,
        private route: ActivatedRoute
    ) {
        var __this = this;
        this.tabItems.push({ url: "/Home/Default", title: "欢迎首页", isSelected: true });
        this.tabItems.push({ url: "/Home/About", title: "平台介绍", isSelected: false });
        //this.router.events.subscribe(e => {
        //    if (e instanceof NavigationEnd) {
        //        __this.currentSelectedUrl = e.url;
        //    }
        //});
    }

    tabSelectedChange(obj: TabBarItem) {
        this.tabSelectedChangeByUrl(obj.url);
        this.router.navigate([obj.url]);
    }

    tabSelectedChangeByUrl(url: string) {
        for (var i = 0; i < this.tabItems.length; i++) {
            this.tabItems[i].isSelected = this.tabItems[i].url == url;
        }
    }
    existUrl(url: string) {
        for (var i = 0; i < this.tabItems.length; i++) {
            if (this.tabItems[i].url == url) {
                return true;
            }
        }
        return false;
    }
}

export class TabBarItem {
    url: string;
    title: string;
    isSelected: boolean;
}


