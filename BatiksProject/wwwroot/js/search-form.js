$("#cari_gambar").click(function(event){
    $("#files").click();
    return false;
});
$("#files").change(function (event){
    $("#cari").submit();
});