(function () {

    var UniqueCheckDirective = function (accountService) {
        return {
            restrict: 'A',
            require: 'ngModel',
            // link is called when the directive is loaded
            link: function (scope, element, attrs, ngModel) {
                // bind to the blur event of the element to which the directive is applied
                element.bind('blur', function (e) {
                    handleBlurEvent(scope,attrs,ngModel,accountService);
                });
            }
        }
    };

    function handleBlurEvent(scope, attrs, ngModel,accountService) {
        // access the property name and property value
        var propertyObj = scope.$eval(attrs.uniqueCheck);

        // go to the service and check if the value exists
        var promiseObj = accountService.IsUserPropertyExists(propertyObj.name, propertyObj.value);
        promiseObj.then(function (response) {
            console.log(response.data.Exists);
            // change the validity state. This will add the unique to $error. Notifies the form of the state change
            ngModel.$setValidity('unique', !response.data.Exists);

        },
        function (response) {

        });
    }

    // register the directive wit the app module
    app.directive('uniqueCheck', ["accountService",UniqueCheckDirective]);
}());