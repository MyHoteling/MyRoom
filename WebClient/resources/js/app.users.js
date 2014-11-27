console.info("FILE LOADED: app.users.js");

var app = angular.module("Users", []);

console.info("USERS MODULE: CREATED");

app.controller(
    "UserController",
    function ($scope, userService) {
        console.info("USERS MODULE: Controller User... READY");

        $scope.users = [];

        $scope.form = {
            id: "",
            name: "",
            surname: "",
            email: "",
            password: "",
            active: ""
        };

        loadRemoteData();

        function applyRemoteData (users) {
            $scope.users = users;
        }

        function loadRemoteData() {
            userService.getUsers()
                       .then(function (users) {
                           applyRemoteData(users);
                       });
        }
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

        function addUser() {
            console.info("USERS MODULE: AddUser... CALLED");
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
            console.err(response.data);

            if (!angular.isObject(response.data) ||
                !response.data.message) {
                return ($q.reject("An unknown error occurred."));
            }

            return ($q.reject(response.data.message));
        }

        function handleSuccess(response) {
            console.info("USERS MODULE: HandleSuccess...");
            console.err(response);

            return (response.data);
        }
    });