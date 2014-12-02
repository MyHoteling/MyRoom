console.info("FILE LOADED: app.users.js");

var app = angular.module("Users", ["ngCookies"]);

console.info("USERS MODULE: CREATED");

app.controller(
    "UserController",
    ['$cookies', '$scope', 'userService', function ($cookies, $scope, userService) {
        console.info("USERS MODULE: Controller User... READY");
        console.log($cookies.Auth);
        userService.setAuthToken($cookies.Auth);
        
        $scope.master = {
            Id: "",
            Name: "",
            Surname: "",
            Email: "",
            Password: "",
            Active: ""
        };
        $scope.users = [];

        $scope.form = {};

        $scope.newUser = function () {
            console.info("USERS MODULE: Adding User...");

            var tpl = angular.copy($scope.master);

            $scope.users.push(tpl);
            $scope.form = $scope.users[$scope.users.length -1];
        }

        $scope.addUser = function () {
            console.info("USERS MODULE: Saving User...");

            $scope.form.Password = jQuery('#Password').val();

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
            
            userService.removeUser($scope.form)
                       .then(function () {
                           loadRemoteData();
                       }, function (errorMessage) {
                           console.error(errorMessage);
                       });
        };

        loadRemoteData();

        function applyRemoteData(users) {
            $scope.users = users;
        };

        function loadRemoteData() {
            console.info("USERS MODULE: Loading Users...");
            userService.getUsers()
                       .then(function (users) {
                           applyRemoteData(users);
                       });
        };
    }]);

app.service(
    "userService",
    function ($http, $q) {
        console.info("USERS MODULE: Service User... READY");
        
        var Auth = null;

        return ({
            setAuthToken : setAuthToken,
            addUser: addUser,
            getUsers: getUsers,
            removeUser: removeUser
        });

        function setAuthToken(token) {
            Auth = token;
        }
        
        function addUser(user) {
            console.info("USERS MODULE: AddUser... CALLED");
            
            var method = 'post',
                url = 'user';

            if (user.Id == "") {
                delete user.Id;
            } else {
                method = 'put';
                url = 'user/' + user.Id;
            }

            var request = $http({
                method: method,
                url: appConfig.webservice + url,
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                },
                data: $.param(user)
            });

            return (request.then(handleSuccess, handleError));
        }

        function getUsers() {
            console.info("USERS MODULE: GetUsers... CALLED");
            
            var request = $http({
                method: "get",
                url: appConfig.webservice + 'user',
                headers: {
                    "Access-Token" : Auth
                }
            });

            return (request.then(handleSuccess, handleError));
        }

        function removeUser(user) {
            console.info("USERS MODULE: removeUsers... CALLED");

            var request = $http({
                method: "delete",
                url: appConfig.webservice + 'user/' + user.Id,
                headers: {
                    "Content-Type": "application/x-www-form-urlencoded"
                },
                data: $.param(user)
            });

            return (request.then(handleSuccess, handleError));
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