// Account service for :Signup, sign in, log off, change password, etc.

(function () {
    var accountService = function ($http) {
        var baseUrl = 'http://localhost/TodoWeb/api/';
        var usersUrl = baseUrl + 'users';
        var accountUrl = baseUrl + 'account';

        var _signup = function (user) {
            // URL:
            // Method : POST
            // Headers: content-type:application/json, Accept: application/json -- Put by default by angularJS
            // Body: user
            // post(url, data, [config]);
            // Return : promise object. It is an object with a then method: promise.then(onSuccess,onError)
            var postPromise = $http.post(usersUrl, user);
            postPromise.then(_postSuccessReponse, _postFailureResponse);
            return postPromise;
        };

        var _signIn = function (loginModel) {
            var postPromise = $http.post(accountUrl + '/login', loginModel);
            postPromise.then(_loginSuccessResponse, _loginFailureResponse);
            return postPromise;
        };

        var _userPropertyExists = function (propertyName, propertyValue) {
            var serviceUrl = usersUrl + "/exists/" + propertyName + "/" + propertyValue;
            var getPromise = $http.get(serviceUrl);
            getPromise.then(_userPropertyExistsSuccess, _userPropertyExistsError);
            return getPromise;
        }

        var _logOff = function (user) {

        };

        var _changePassword = function (changePasswordModel) {

        };

        // Callback function when a POST request is successful
        var _postSuccessReponse = function (response) {
            return response;
        };

        // Callback function when a POST request is a failure
        var _postFailureResponse = function (response) {
            return response;
        };

        // Callback function when a login is successful
        var _loginSuccessResponse = function (response) {
            return response;
        }

        //Callback function when a login is unseccessful
        var _loginFailureResponse = function (response) {
            return response;
        }

        // Callback function when the user propery exists is successful
        var _userPropertyExistsSuccess = function (response) {
            return response.data;
        }

        // Callback function when the user propery exists is unsyccessful
        var _userPropertyExistsError = function (response) {
            return response;
        }


        //var deferred = $q.defer();
        //var promiseObj = deferred.promise;
        //promiseObj.then(onSuccess, onFailure);

        //function onSuccess(result) {
        //    // do something
        //}

        //function onFailure(result) {
        //    // do something
        //}

        //// deferred -  task that will finish in the future. Task has 3 states: pending, fulfilled, rejected. Default state is pending.
        //deferred.resolve(); // Task has succeeded.  State: fulfilled.   This will invoke the success function provided to promise 
        //deferred.reject();  // Task has failed      State: rejected.   This will invoke the success function provided to promise 

        //deferred.promise(); // Promise object. It is an object with a then method: promise.then(onSuccess,onError)

        return {
            Signup: _signup,
            Signin: _signIn,
            LogOff: _logOff,
            ChangePassword: _changePassword,
            IsUserPropertyExists: _userPropertyExists
        };
    };

    // Register the service with the app module
    var appModule = angular.module("todoModule");
    appModule.factory("accountService", accountService);
}());


