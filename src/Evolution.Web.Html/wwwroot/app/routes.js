"use strict";
var router_1 = require('@angular/router');
var home_component_1 = require('./components/home.component');
var about_component_1 = require('./components/about.component');
var organize_component_1 = require('./components/organize.component');
var appRoutes = [
    {
        path: '',
        redirectTo: '/Home/Default',
        pathMatch: 'full'
    },
    {
        path: 'Home/Default',
        component: home_component_1.HomeComponent
    },
    {
        path: 'Home/About',
        component: about_component_1.AboutComponent
    },
    {
        path: 'SystemManage/Organize/Index',
        component: organize_component_1.OrganizeComponent
    }
];
exports.routing = router_1.RouterModule.forRoot(appRoutes);
//# sourceMappingURL=routes.js.map