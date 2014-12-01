console.info("FILE LOADED: app.login.js");

var app = angular.module("Login", []);

console.info("LOGIN MODULE: CREATED");

app.controller(
    "LoginController",
    function ($scope, loginService) {
        console.info("LOGIN MODULE: Controller Login... READY");

        $scope.master = {
            Identity: "",
            Credential: ""
        };

        $scope.form = {};

        $scope.login = function () {
            console.info("LOGIN MODULE: Singing in...");

            loginService.login($scope.form).then(
                function () {
                    console.log("ENTRE");
                },
                function(errorMessage){
                console.log(errorMessage);
            });
        };
    });

app.service(
    "loginService",
    function ($http, $q) {
        console.info("LOGIN MODULE: Service Login... READY");

        return ({
            login : login
        });

        function login(identity, credential) {

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