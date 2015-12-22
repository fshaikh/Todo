(function(){
    var SignupController = function ($scope, $location,accountService) {
        $scope.Signup = function () {
            // validate the model. if fails give appropriate error
            var userPropertyExistsPromise = accountService.IsUserPropertyExists("UserName", $scope.username);
            userPropertyExistsPromise.then(function (response) {
                                           },
                                           function (response) {
                                           });


            // invoke the service 
            var promiseObj = accountService.Signup({
                        Username: $scope.username,
                        Password: $scope.password,
                        Email: $scope.emailaddress,
                        Name:$scope.name
            });
            promiseObj.then(_onSuccess, _onFailure);
        };

        var _onSuccess = function (response) {
            // successful sign up .navigate to the 
        };

        var _onFailure = function (response) {
            alert(response);
        };
    };

    // register the controller with the module
    app.controller("SignupController", ["$scope", "$location","accountService", SignupController]);
}());