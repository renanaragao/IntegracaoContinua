angular.module('faleMaisTelZirApp')
    .directive('number', function () {
        return {
            restrict: 'A',
            scope: {
                typenumber: '@'
            },
            require: 'ngModel',
            link: function (scope, element, attrs, ngModel) {

                if (!ngModel) return;

                var number = new Number();

                ngModel.$formatters.unshift(function (a) {

                    return a;

                });

                $(element).focusout(function () {
                    if (!number.ehNumero($(this).val()))
                        $(this).val('');
                });

                $(element).keydown(function (event) {

                    if (event.keyCode == 46 || event.keyCode == 8 || event.keyCode == 9 || event.keyCode == 27 || event.keyCode == 13 ||
                      (event.keyCode == 65 && event.ctrlKey === true) ||
                      (event.keyCode == 67 && event.ctrlKey === true) ||
                      (event.keyCode == 88 && event.ctrlKey === true) ||
                      (event.keyCode == 86 && event.ctrlKey === true) ||
                      (event.keyCode == 65 && event.metaKey === true) ||
                      (event.keyCode == 67 && event.metaKey === true) ||
                      (event.keyCode == 88 && event.metaKey === true) ||
                      (event.keyCode == 86 && event.metaKey === true) ||
                      (event.keyCode >= 35 && event.keyCode <= 39)) {

                        return;

                    }
                    else {

                        if (event.shiftKey || (event.keyCode < 48 || event.keyCode > 57) && (event.keyCode < 96 || event.keyCode > 105)) {
                            event.preventDefault();
                        }

                    }

                });

                ngModel.$parsers.unshift(function (value) {

                    return scope.typenumber !== 'false' ? number.converter(value) : value;

                });
            }
        };
    });