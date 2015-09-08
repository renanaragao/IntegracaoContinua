angular.module('chamadaFaleMaisTelZirApp')
    .controller('chamadaController', ['$scope', 'chamadaService', function ($scope, chamadaService) {

        $scope.chamadas = [];

        chamadaService.retornarChamadas().then(function(chamadas) {

            $scope.chamadas = chamadas;

        });

        $scope.salvar = function (chamada) {

            chamadaService.salvar(chamada).then(function (data) {

                if (data.erro) {

                    $scope.alerta = {
                        show: true,
                        mensagem: data.erro
                    };

                    return;

                }

                chamada.Codigo = data.Codigo;
                chamada.alterado = false;

                $scope.alerta = {
                    show: false
                };

            });

        };

        $scope.selectOrigem = function(chamada) {

            chamada.alterado = true;
            chamadaService.marcarChamdasDuplicadas($scope.chamadas);
        };

        $scope.selectDestino = function(chamada) {

            chamada.alterado = true;
            chamadaService.marcarChamdasDuplicadas($scope.chamadas);

        };

        $scope.adicionarChamada = function(chamada) {

            $scope.chamadas.push(chamada);

        };

        $scope.selectOrigemSimulacao = function() {

            $scope.chamadaSimulacao = $scope.chamadas.filter(function(chamada) {

                return chamada.Origem == $scope.origemSimulacao && chamada.Destino == $scope.destinoSimulacao;

            })[0];

        };

        $scope.selectDestinoSimulacao = function () {

            $scope.chamadaSimulacao = $scope.chamadas.filter(function (chamada) {

                return chamada.Origem == $scope.origemSimulacao && chamada.Destino == $scope.destinoSimulacao;

            })[0];

        };

        $scope.changeValorPorMinuto = function(chamada) {

            chamada.alterado = true;

        };

        $scope.calcularValoresLigacao = function() {

            $scope.valorComPlano = chamadaService.calcularLigacaoComPlano($scope.quantidadeMinutoSimulacao, $scope.chamadaSimulacao.ValorPorMinuto, parseInt($scope.planoFaleMais));
            $scope.valorSemPlano = chamadaService.calcularLigacaoSemPlano($scope.quantidadeMinutoSimulacao, $scope.chamadaSimulacao.ValorPorMinuto);

        };

    }
]);