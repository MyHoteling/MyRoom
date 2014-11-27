console.info("FILE LOADED: app.users.js");

var app = angular.module("Users", []);

console.info("USERS MODULE: CREATED");

app.controller(
    "UserController",
    function ($scope, userService) {
        console.info("USERS MODULE: Controller User... READY");

        $scope.master = {};
        $scope.users = [];

        $scope.form = {
            Id: "",
            Name: "",
            Surname: "",
            Email: "",
            Password: "",
            Active: ""
        };

        $scope.addUser = function () {
            console.info("USERS MODULE: Adding User...");
            console.log($scope.form);
            userService.addUser($scope.form)
                       .then(function () {
                           loadRemoteData();

                           $scope.form = angular.copy($scope.master);

                       }, function (errorMessage) {
                           console.error(errorMessage);
                       });
        };

        $scope.deleteUser = function () {
            console.info("USERS MODULE: Deleting User...");
            console.log($scope.form);
        };

        loadRemoteData();

        function applyRemoteData(users) {
            $scope.form = users[0];
            $scope.users = users;
        };

        function loadRemoteData() {
            console.info("USERS MODULE: Loading Users...");
            userService.getUsers()
                       .then(function (users) {
                           applyRemoteData(users);
                       });
        };
    });

app.service(
    "userService",
    function ($http, $q) {
        console.info("USERS MODULE: Service User... READY");

        return ({
            addUser: addUser,
            getUsers: getUsers,
            removeUser: removeUser
        });

        function addUser(user) {
            console.info("USERS MODULE: AddUser... CALLED");

            delete user.Id;

            var request = $http({
                method: "post",
                url: appConfig.webservice + 'user',
                headers: {
                    "Content-Type" : "application/x-www-form-urlencoded"
                },
                data: $.param(user)
            });

            return (request.then(handleSuccess, handleError));
        }

        function getUsers() {
            console.info("USERS MODULE: GetUsers... CALLED");

            var request = $http({
                method: "get",
                url : appConfig.webservice + 'user'
            });

            return (request.then(handleSuccess, handleError));
        }

        function removeUser() {
            console.info("USERS MODULE: removeUsers... CALLED");
        }

        function handleError(response) {
            console.info("USERS MODULE: HandleError...");
            console.error(response.data);

            if (!angular.isObject(response.data) ||
                !response.data.message) {
                return ($q.reject("An unknown error occurred."));
            }

            return ($q.reject(response.data.message));
        }

        function handleSuccess(response) {
            console.info("USERS MODULE: HandleSuccess...");
            console.log(response);

            return (response.data);
        }
    });