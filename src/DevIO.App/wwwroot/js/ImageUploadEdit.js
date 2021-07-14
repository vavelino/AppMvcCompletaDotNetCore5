$('#ImageUpload').change(function () {
  $('#img_name').text(this.files[0].name);
  $('#img_name')[0].style.display = 'block';
});
