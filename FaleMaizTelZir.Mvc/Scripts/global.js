'use strict';

Function.prototype.method = function(nome, funcao) {

    this.prototype[nome] = funcao;

    return this;

};

Number.method('ehNumero', function(valor) {

    return /(?:^\d*$)/.test(valor);

});

Number.method('converter', function (valor) {

    if (!new Number().ehNumero(valor)) return 0;

    return parseInt(valor);

});