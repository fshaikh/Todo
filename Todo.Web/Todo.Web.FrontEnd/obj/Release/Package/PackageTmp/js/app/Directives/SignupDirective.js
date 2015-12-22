(function () {
    var SignupDirective = function () {
        return {
            restrict: 'E',  // Can only be used as an HTML element
            templateUrl:'SignupTemplate.html'
        }
    };

    // register the directive with the app
    app.directive('signupForm', SignupDirective);
}());

// Usage:
// <signup-form/>