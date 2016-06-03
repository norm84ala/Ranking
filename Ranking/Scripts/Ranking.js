/// <reference path="angular.js" />


var app = angular
.module("rankModule", []);
app.controller("topfiveController", function ($scope, $http) {

    $http.get('/DataService.asmx/GetAvailableReportDate')
    .then(function (response) {
        $scope.AvailableReportDate = response.data;

    });
    $scope.getReport = function (modelName, newValue) {
        var selectedReport = JSON.stringify({ selectedReport: $scope.selectedDate });

        $http.post('/DataService.asmx/GetReport', selectedReport)
          .then(function (response) {
              $scope.WebHits = JSON.parse(response.data.d);

          });
    }



});


 
app.controller("logController", function ($scope, $http) {

    $http.get('/DataService.asmx/GetAvailableSites')
    .then(function (response) {
        $scope.AvailableSites = response.data;

    });
    $scope.getLogs= function (modelName, newValue) {
        var selectedSite = JSON.stringify({ selectedSite: $scope.selectedSite });

        $http.post('/DataService.asmx/GetSitesLog', selectedSite)
          .then(function (response) {
              $scope.Weblogs = JSON.parse(response.data.d);


                var logs = [];
                angular.forEach($scope.Weblogs, function (value, key) {

                    var log = { TotalVisit: value.TotalVisit, ReportedDate:new Date(value.ReportedDate) };
                      this.push(log);
                  }, logs);

                $scope.allLogs = logs;


          });
    }



});
