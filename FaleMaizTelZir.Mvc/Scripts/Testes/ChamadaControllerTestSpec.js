describe('ChamadaController - ', function () {

    var httpBackend,
        scope,
        chamaResponse = { Codigo: 23 };

    beforeEach(function () {

        module('faleMaisTelZirApp');

    });

    beforeEach(inject(function ($rootScope, $controller, $httpBackend) {

        scope = $rootScope.$new();

        httpBackend = $httpBackend;

        $controller('chamadaController', { $scope: scope });

        httpBackend.when('POST', '/Chamada/Salvar').respond(chamaResponse);
        httpBackend.when('GET', '/Chamada/RetornarChamadas').respond([{Origem: 12, Destino: 13}, {}]);

    }));

    afterEach(function () {

        httpBackend.verifyNoOutstandingExpectation();
        httpBackend.verifyNoOutstandingRequest();

    });

    it('Deve salvar uma chamada', function() {

        var chamada = {};

        runs(function () {
            scope.salvar(chamada);
            scope.$digest();
            httpBackend.flush();

        });

        waitsFor(function () {

            return chamada.Codigo;

        });

        runs(function () {

            expect(chamada.Codigo).toEqual(23);
            expect(chamada.alterado).toEqual(false);
            expect(scope.alerta.show).toEqual(false);

        });

    });

    it('Deve mostrar um alerta caso ocorra algum erro ao salvar uma chamada', function() {
        
        var chamada = {};

        chamaResponse.erro = 'Erro';

        runs(function () {
            scope.salvar(chamada);
            scope.$digest();
            httpBackend.flush();

        });

        waitsFor(function () {

            return scope.alerta.show;

        });

        runs(function () {

            expect(scope.alerta.show).toEqual(true);
            expect(scope.alerta.mensagem).toEqual('Erro');

        });

    });

    it('Deve carregar as chamadas', function() {

        runs(function() {

            httpBackend.flush();

        });

        waitsFor(function() {

            return scope.chamadas;

        });

        runs(function() {

            expect(scope.chamadas.length).toEqual(2);

        });

    });

    it('Quando selecionar a origem, deve marca a chamada como alterada true', function() {
        
        httpBackend.flush();

        var chamada = {};
        scope.selectOrigem(chamada);
        expect(chamada.alterado).toEqual(true);

    });

    it('Quando selecionar um destino, deve marca a chamada como alterada true', function() {
        
        httpBackend.flush();

        var chamada = {};
        scope.selectDestino(chamada);
        expect(chamada.alterado).toEqual(true);

    });

    it('Quando selecionar o destino, deve marcar as chamadas duplicadas', function() {
        
        httpBackend.flush();

        scope.chamadas = [
            { Origem: 1, Destino: 12 },
            { Origem: 3, Destino: 13 },
            { Origem: 3, Destino: 14 },
            { Origem: 3, Destino: 14 }
        ];

        scope.selectDestino({});

        expect(scope.chamadas[0].duplicado).toEqual(false);
        expect(scope.chamadas[1].duplicado).toEqual(false);
        expect(scope.chamadas[2].duplicado).toEqual(true);
        expect(scope.chamadas[3].duplicado).toEqual(true);

    });

    it('Quando selecionar a origem, deve marcar as chamadas duplicadas', function () {

        httpBackend.flush();

        scope.chamadas = [
            { Origem: 1, Destino: 12 },
            { Origem: 3, Destino: 13 },
            { Origem: 3, Destino: 14 },
            { Origem: 3, Destino: 14 }
        ];

        scope.selectOrigem({});

        expect(scope.chamadas[0].duplicado).toEqual(false);
        expect(scope.chamadas[1].duplicado).toEqual(false);
        expect(scope.chamadas[2].duplicado).toEqual(true);
        expect(scope.chamadas[3].duplicado).toEqual(true);

    });

    it('Deve adicionar uma chamada', function() {

        httpBackend.flush();

        scope.chamadas = [];

        scope.adicionarChamada({});

        expect(scope.chamadas.length).toEqual(1);
    });

    it('Quando selecionar a origem, deve filtrar as chamadas', function() {

        runs(function() {
            delete scope.chamadas;
            httpBackend.flush();

        });

        waitsFor(function() {

            return scope.chamadas.length > 0;

        });

        runs(function() {

            scope.origemSimulacao = 12;
            scope.destinoSimulacao = 13;
            scope.selectOrigemSimulacao();
            scope.$apply();
            expect(scope.chamadaSimulacao).toBeDefined();

        });

    });

    it('Quando selecionar o destino, deve filtrar as chamadas', function () {

        runs(function () {
            delete scope.chamadas;
            httpBackend.flush();

        });

        waitsFor(function () {

            return scope.chamadas.length > 0;

        });

        runs(function () {

            scope.origemSimulacao = 12;
            scope.destinoSimulacao = 13;
            scope.selectDestinoSimulacao();
             
            expect(scope.chamadaSimulacao).toBeDefined();

        });

    });

    it('Quando mudar o valor por minuto, deve marcar a chamada como alterada true', function() {

        httpBackend.flush();

        var chamada = {};

        scope.changeValorPorMinuto(chamada);

        expect(chamada.alterado).toEqual(true);

    });

    it('Deve calcular os valores das chamadas sem plano e com plano', function() {

        httpBackend.flush();

        scope.chamadaSimulacao = {
            ValorPorMinuto: 1.90
        };

        scope.quantidadeMinutoSimulacao = 20;
        scope.planoFaleMais = '20';

        scope.calcularValoresLigacao();

        expect(scope.valorComPlano).toBeCloseTo(0);
        expect(scope.valorSemPlano).toBeCloseTo(38.00);

    });

});