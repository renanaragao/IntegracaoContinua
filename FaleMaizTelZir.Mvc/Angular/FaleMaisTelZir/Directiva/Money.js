angular.module('faleMaisTelZirApp')
    .directive('money', ['$filter', function ($filter) {

        return {
            restrict: 'A',
            require: 'ngModel',
            scope: {
                money: '@'
            },
            link: function (scope, elem, attrs, ngModel) {
                if (!ngModel) return;

                var casaDecimal = new Number().ehNumero(scope.money) ? parseInt(scope.money) : 2;

                ngModel.$formatters.unshift(function (a) {

                    return $filter('number')(a, casaDecimal);

                });

                elem.priceFormat({
                    prefix: '',
                    centsSeparator: '.',
                    thousandsSeparator: ',',
                    centsLimit: casaDecimal,
                    allowNegative: true
                });

                ngModel.$parsers.unshift(function () {
                    elem.priceFormat({
                        prefix: '',
                        centsSeparator: '.',
                        thousandsSeparator: ',',
                        centsLimit: casaDecimal,
                        allowNegative: true
                    });

                    return parseFloat(elem.val().replace(',', ''));
                });
            }
        };
    }]);

