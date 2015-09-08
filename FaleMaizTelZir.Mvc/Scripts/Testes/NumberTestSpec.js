describe("Number - ", function () {

    var scope,compile;

    beforeEach(function () {

        module('faleMaisTelZirApp');

    });

    beforeEach(inject(function ($rootScope, $compile) {
        scope = $rootScope.$new();

        compile = $compile;

    }));

    it('Deve fazer o binding tipado na model.', function () {
        var input = angular.element('<form name="form"><input type="text" name="quantidade" number ng-model="quantidade"/></form>');

        compile(input)(scope);
        scope.$digest();

        scope.form.quantidade.$setViewValue('4');
        expect(scope.quantidade).toEqual(4);
    });

});
