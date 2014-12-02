console.info("FILE LOADED: app.login.js");

var app = angular.module("Login", ["ngCookies"]);

console.info("LOGIN MODULE: CREATED");

app.controller(
    "LoginController",
    ['$cookies', '$scope', 'loginService', function ($cookies, $scope, loginService) {
        console.info("LOGIN MODULE: Controller Login... READY");

        $scope.master = {
            Identity: "",
            Credential: ""
        };

        $scope.form = {};

        $scope.login = function () {
            console.info("LOGIN MODULE: Singing in...");
            console.log($scope.form);

            loginService.login($scope.form).then(
                function (token) {
                    $cookies.Auth = token.AccessToken;

                    location.href = 'users.html';
                },
                function(errorMessage){
                console.log(errorMessage);
            });
        };
    }]);

app.service(
    "loginService",
    function ($http, $q) {
        console.info("LOGIN MODULE: Service Login... READY");

        return ({
            login : login
        });

        function login(authRequest) {
            console.info("LOGIN MODULE: login... CALLED");

            var request = $http({
                method: "post",
                url: appConfig.webservice + 'auth/auth',
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                },
                data: $.param(authRequest)
            });

            return (request.then(handleSuccess, handleError));
        };

        function handleError(response) {
            console.info("LOGIN MODULE: HandleError...");
            console.error(response.data);

            if (!angular.isObject(response.data) ||
                !response.data.message) {
                return ($q.reject("An unknown error occurred."));
            }

            return ($q.reject(response.data.message));
        }

        function handleSuccess(response) {
            console.info("LOGIN MODULE: HandleSuccess...");
            console.log(response);

            return (response.data);
        }
    });