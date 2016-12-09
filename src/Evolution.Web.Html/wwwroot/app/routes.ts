import { ModuleWithProviders }  from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './components/home.component';
import { AboutComponent } from './components/about.component';
import { OrganizeComponent } from './components/organize.component';

const appRoutes: Routes = [
    {
        path: '',
        redirectTo: '/Home/Default',
        pathMatch: 'full'
    },
    {
        path: 'Home/Default',
        component: HomeComponent
    },
    {
        path: 'Home/About',
        component: AboutComponent
    }
    //{
    //    path: 'SystemManage/Organize/Index',
    //    component: OrganizeComponent
    //}
];

export const routing: ModuleWithProviders = RouterModule.forRoot(appRoutes);
