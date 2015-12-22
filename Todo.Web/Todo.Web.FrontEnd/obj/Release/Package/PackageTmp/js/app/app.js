// Put all applicationw wide initialization here

// Create the app module
var app = angular.module("todoModule", ["ngRoute","ngMessages"]);

// Define configuration for this module
app.config(function ($routeProvider) {
    $routeProvider
        .when("/signup", {
            templateUrl: "Signup.html",
            controller: "SignupController"
            //redirectTo:"/View/Signup.html"
        })
        .when("/signin", {
            templateUrl: "LoginIndex.html",
            controller:"LoginController"
        });
});