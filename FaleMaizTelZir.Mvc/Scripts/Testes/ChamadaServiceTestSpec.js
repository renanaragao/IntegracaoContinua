describe('chamadaService - ', function () {

    var httpBackend,
        scope,
        servico;

    beforeEach(function () {

        module('faleMaisTelZirApp');

    });

    beforeEach(inject(function ($httpBackend, $rootScope, chamadaService) {
        scope = $rootScope.$new();
        servico = chamadaService;
        httpBackend = $httpBackend;

        httpBackend.when('POST', '/Chamada/Salvar').respond({ Codigo: 23 });
        httpBackend.when('GET', '/Chamada/RetornarChamadas').respond([{}, {}]);

    }));

    afterEach(function () {

        httpBackend.verifyNoOutstandingExpectation();
        httpBackend.verifyNoOutstandingRequest();

    });

    it('Deve salvar uma chamada', function () {

        var chamada;

        runs(function () {

            servico.salvar({}).then(function (novaChamda) {
                chamada = novaChamda;
            });

            httpBackend.flush();

        });

        waitsFor(function () {

            return chamada;

        });

        runs(function () {

            expect(chamada.Codigo).toEqual(23);

        });

    });

    it('Deve retornar as chmadas', function () {

        var chamadas;

        runs(function () {

            servico.retornarChamadas().then(function (data) {
                chamadas = data;
            });

            httpBackend.flush();

        });

        waitsFor(function () {

            return chamadas;

        });

        runs(function () {

            expect(chamadas.length).toEqual(2);

        });

    });

    it('Deve marcar as chamadas duplicadas', function () {

        var chamadas = [
            { Origem: 1, Destino: 12, duplicado: true },
            { Origem: 3, Destino: 13, duplicado: true },
            { Origem: 3, Destino: 14, duplicado: true },
            { Origem: 3, Destino: 14, duplicado: true }
        ];

        servico.marcarChamdasDuplicadas(chamadas);

        expect(chamadas[0].duplicado).toEqual(false);
        expect(chamadas[1].duplicado).toEqual(false);
        expect(chamadas[2].duplicado).toEqual(true);
        expect(chamadas[3].duplicado).toEqual(true);

    });

    it('Deve calcular o valor da ligação', function () {

        //Tempo da ligação menor ou igual ao plano
        expect(servico.calcularLigacaoComPlano(20, 1.9, 30)).toEqual(0);
        expect(servico.calcularLigacaoComPlano(30, 1.9, 30)).toEqual(0);

        //Tempo da ligação maior que o plano
        expect(servico.calcularLigacaoComPlano(80, 1.70, 60)).toBeCloseTo(37.40);

        //Sem o plano
        expect(servico.calcularLigacaoSemPlano(20, 1.9)).toEqual(38);

    });

});