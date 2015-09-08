angular.module('chamadaFaleMaisTelZirApp')
    .service('chamadaService', ['$http', '$q', function ($http, $q) {

        var salvar = function(chamada) {

            var defer = $q.defer();

            $http.post('/Chamada/Salvar', chamada).success(function (data) {

                defer.resolve(data);

            }).error(function (data) {

                defer.reject(data);

            });

            return defer.promise;

        };

        var retornarChamadas = function() {

            var defer = $q.defer();

            $http.get('/Chamada/RetornarChamadas').success(function (data) {

                defer.resolve(data);

            }).error(function (data) {

                defer.reject(data);

            });

            return defer.promise;

        };

        var marcarChamdasDuplicadas = function (chamadas) {

            chamadas.forEach(function(chamada) {
                chamada.duplicado = false;
            });

            chamadas.forEach(function(chamada) {
                
                if (!chamada.duplicado) {

                    var chamadasDuplicadas = chamadas.filter(function(item) {

                        return (item.Origem === chamada.Origem && item.Destino === chamada.Destino) && item !== chamada;

                    });

                    if (chamadasDuplicadas.length > 0) {

                        chamada.duplicado = true;

                        chamadasDuplicadas.forEach(function (item) {

                            item.duplicado = true;

                        });

                    }

                }

            });

        };

        var calcularLigacaoComPlano = function(tempo, valorPorMinuto, plano) {

            if (tempo <= plano) return 0;

            return ((tempo - plano) * (valorPorMinuto + (valorPorMinuto * 0.1)));

        };

        var calcularLigacaoSemPlano = function(tempo, valorPorMinuto) {

            return tempo * valorPorMinuto;

        };

    return {
        salvar: salvar,
        retornarChamadas: retornarChamadas,
        marcarChamdasDuplicadas: marcarChamdasDuplicadas,
        calcularLigacaoComPlano: calcularLigacaoComPlano,
        calcularLigacaoSemPlano: calcularLigacaoSemPlano
    };

}
]);