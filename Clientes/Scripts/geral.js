$("#tipo_cliente").change(function () {
    if ($("#tipo_cliente").val() == "fisica") {
        $("#pessoa_juridica").addClass("hidden");
        $("#pessoa_fisica").removeClass("hidden");
    }
    else {
        $("#pessoa_fisica").addClass("hidden");
        $("#pessoa_juridica").removeClass("hidden");
    }
})
$("#nascimento").blur(function () {
    if ($("#nascimento").val() !== '') {
        var aniversario = new Date($("#nascimento").val());
        var hoje = new Date();
        var atualAniversario = new Date(hoje.getFullYear(), aniversario.getMonth(), aniversario.getDate());
        var idade = hoje.getFullYear() - aniversario.getFullYear();

        if (atualAniversario > hoje) { //verifica se o aniversario do ano atual já passou, se não remove um ano da idade
            idade--;
        }
        if (parseInt(idade) < 19) { //verifica se está na idade mínima estipulada
            alert('Atenção!\nIdade mínima para cadastro é de 19 anos.');
            $("#nascimento").val('');
            $("#nascimento").focus();
        }
    }

});

$("#cep").blur(function () {
    if (($("#cep").val()).length != 8) return false;
    if ($("#cep").val() != "") {
        $.get("http://apps.widenet.com.br/busca-cep/api/cep.json", { code: $("#cep").val() }, function (result) {
            if (result.status != 1) {
                alert('Ocorreu um erro ao buscar o CEP.\nPor favor tente novamente.');
                $("#cep").val('');
                $("#cep").focus();
                return false;
            }
            $("#logradouro").val(result.address);
            $("#bairro").val(result.district);
            $("#cidade").val(result.city);
            $("#uf").val(result.state);
            $("#numero").focus();
        });
    }    
});

$(document).ready(function () {
    $("#cep").mask("99999999");
    $("#cpf").mask("999.999.999-99");
    $("#cnpj").mask("999.999.999/9999-99");
});