(function(){

    var LoginController = function ($scope, $location, accountService) {
        $scope.SignIn = function () {
            var loginRequest = {
                'Username': $scope.username,
                'Password' : $scope.password
            };
            var responsePromise = accountService.Signin(loginRequest);
            responsePromise.then(_loginSuccess, _loginError);
        }

        $scope.CancelSignIn = function () {
            // go back to the landing page
            window.location = "View\LandingPage.html";
        }

        $scope.IsShowUsername = function () {
            return true;
        }

        function _loginSuccess(response) {

        }

        function _loginError(response) {

        }

    };

    // register the controller with the module
    app.controller("LoginController", ["$scope", "$location", "accountService", LoginController]);
}());