$('#ImageUpload').change(function () {
  $('#img_name').text(this.files[0].name);
  $('#img_name')[0].style.display = 'block';
});

$('#ImageUpload').attr('data-val', 'true');
$('#ImageUpload').attr('data-val-required', 'Preencha o campo Imagem');
