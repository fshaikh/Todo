(function(){
    var LoginRouteController = function ($scope,$location) {
        // Attach model(state + behavior) to $scope. For eg:
        $scope.message = "Sign Up";   // Defining message property

        $scope.routeToSignUp = function () {
            // Route to sign up page. Change the location.This will trigger the route engine defined in app.routes.js
            //$location.path("/signup");
            window.location = "Signup/Index.html";
        };

        $scope.routeToSignIn = function () {
            window.location = "Login/LoginIndex.html";
        };
    };

    // Controllers live in modules. Register controller with module
    // Since the js files can be minified in production, explicitly supply the dependencies for this controller when registering.
    // TODO: When more dependencies are added to the controller, add them below
    app.controller("LoginRouteController", ["$scope","$location",LoginRouteController]);
}());




