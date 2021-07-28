
function SetModal() {
    //console.log("SetModal");
    $(document).ready(function () {
        $(function () {
            $.ajaxSetup({ cache: false });

            $("a[data-modal]").on("click",
                function (e) {
                    $('#myModalContent').load(this.href,
                        function () {
                            $('#myModal').modal({
                                keyboard: true
                            },
                                'show');
                            bindForm(this);
                        });
                    return false;
                });
        });
    });
}

function bindForm(dialog) {   
    $('form', dialog).submit(function () {
        $.ajax({
            url: this.action,
            type: this.method,
            data: $(this).serialize(),
            success: function (result) {               
                if (result.success) {
                    //console.log("SENA");
                    //console.log(result.url);

                    $('#myModal').modal('hide');
                    document.location.reload(true);
                    //$('#AddressTarget').load(result.url); // Carrega o resultado HTML para a div demarcada
                } else {                  
                    $('#myModalContent').html(result);
                    bindForm(dialog);
                }
            }
        });
        return false;
    });
}

function SearchCep() {
    $(document).ready(function () {

        function cleanZipForm() {
            // Limpa valores do formulário de cep.
            $("#Address_Street").val("");
            $("#Address_Neighborhood").val("");
            $("#Address_City").val("");
            $("#Address_State").val("");
        }

        //Quando o campo cep perde o foco.
        $("#Address_ZipCode").blur(function () {

            //Nova variável "cep" somente com dígitos.
            var cep = $(this).val().replace(/\D/g, '');

            //Verifica se campo cep possui valor informado.
            if (cep != "") {

                //Expressão regular para validar o CEP.
                var ValidateCep = /^[0-9]{8}$/;

                //Valida o formato do CEP.
                if (ValidateCep.test(cep)) {

                    //Preenche os campos com "..." enquanto consulta webservice.
                    $("#Address_Street").val("...");
                    $("#Address_Neighborhood").val("...");
                    $("#Address_City").val("...");
                    $("#Address_State").val("...");

                    //Consulta o webservice viacep.com.br/
                    $.getJSON("https://viacep.com.br/ws/" + cep + "/json/?callback=?",
                        function (dados) {

                            if (!("erro" in dados)) {
                                //Atualiza os campos com os valores da consulta.
                                $("#Address_Street").val(dados.logradouro);
                                $("#Address_Neighborhood").val(dados.bairro);
                                $("#Address_City").val(dados.localidade);
                                $("#Address_State").val(dados.uf);
                            } //end if.
                            else {
                                //CEP pesquisado não foi encontrado.
                                cleanZipForm();
                                //alert("CEP não encontrado.");
                            }
                        });
                } //end if.
                else {
                    //cep é inválido.
                    cleanZipForm();
                    //alert("Formato de CEP inválido.");
                }
            } //end if.
            else {
                //cep sem valor, limpa formulário.
                cleanZipForm();
            }
        });
    });
}

$(document).ready(function () {
    $("#msg_box").fadeOut(2500);
});
