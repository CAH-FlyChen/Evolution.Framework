"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var HomeComponent = (function () {
    function HomeComponent() {
    }
    HomeComponent.prototype.ngAfterContentInit = function () {
        this.GetSalaryChart();
        this.GetLeaveChart();
    };
    HomeComponent.prototype.GetSalaryChart = function () {
        var randomScalingFactor = function () { return Math.round(Math.random() * 100); };
        var lineChartData = {
            labels: ["1月", "2月", "3月", "4月", "5月", "6月", "7月", "8月", "8月", "10月", "11月", "12月"],
            datasets: [
                {
                    label: "My First dataset",
                    fillColor: "rgba(220,220,220,0.2)",
                    strokeColor: "rgba(220,220,220,1)",
                    pointColor: "rgba(220,220,220,1)",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "rgba(220,220,220,1)",
                    data: [randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor()]
                },
                {
                    label: "My Second dataset",
                    fillColor: "rgba(151,187,205,0.2)",
                    strokeColor: "rgba(151,187,205,1)",
                    pointColor: "rgba(151,187,205,1)",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "rgba(151,187,205,1)",
                    data: [randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor(), randomScalingFactor()]
                }
            ]
        };
        var canvas = document.getElementById("salarychart");
        var ctx = canvas.getContext("2d");
        var x = new Chart(ctx);
        window.myLine = new Chart(ctx).Line(lineChartData, {
            responsive: false,
            bezierCurve: false
        });
    };
    HomeComponent.prototype.GetLeaveChart = function () {
        var randomScalingFactor = function () { return Math.round(Math.random() * 100); };
        var a_value = randomScalingFactor();
        var b_value = randomScalingFactor();
        var c_value = randomScalingFactor();
        var d_value = randomScalingFactor();
        var doughnutData = [
            {
                value: a_value,
                color: "#F7464A",
                highlight: "#FF5A5E",
                label: "事假"
            },
            {
                value: b_value,
                color: "#46BFBD",
                highlight: "#5AD3D1",
                label: "病假"
            },
            {
                value: c_value,
                color: "#FDB45C",
                highlight: "#FFC870",
                label: "公休假"
            },
            {
                value: d_value,
                color: "#949FB1",
                highlight: "#A8B3C5",
                label: "调休假"
            }
        ];
        var canvas = document.getElementById("leavechart");
        var ctx = canvas.getContext("2d");
        window.myDoughnut = new Chart(ctx).Doughnut(doughnutData, { responsive: false });
        $("#a_value").html(a_value + "小时");
        $("#b_value").html(b_value + "小时");
        $("#c_value").html(c_value + "小时");
        $("#d_value").html(d_value + "小时");
    };
    HomeComponent = __decorate([
        core_1.Component({
            selector: 'home',
            templateUrl: './app/components/home.component.html',
            styleUrls: ['./css/framework-about.css']
        }), 
        __metadata('design:paramtypes', [])
    ], HomeComponent);
    return HomeComponent;
}());
exports.HomeComponent = HomeComponent;
