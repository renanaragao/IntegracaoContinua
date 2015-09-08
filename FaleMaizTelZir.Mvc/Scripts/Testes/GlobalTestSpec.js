describe('Global - ', function() {

    it('Deve verificar se é um número', function() {

        expect(new Number().ehNumero('34')).toEqual(true);

        expect(new Number().ehNumero('34d')).toEqual(false);

    });

    it('Deve converter uma string em um número', function() {

        expect(new Number().converter('34')).toEqual(34);

        expect(new Number().converter('afas')).toEqual(0);

    });

});