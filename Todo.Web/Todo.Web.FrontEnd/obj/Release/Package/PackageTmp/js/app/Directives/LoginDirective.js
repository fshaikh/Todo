(function () {
    var LoginDirective = function () {
        return {
            restrict:'E',  // Can only be used as an HTML element
            templateUrl:'LoginTemplate.html'
        }
    };

    // register the login directive with the app
    app.directive('loginForm', LoginDirective);
}());

// Usage:
// <login-form/>