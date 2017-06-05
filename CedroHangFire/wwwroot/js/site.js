$("#TypeJob").change(function () {

    var valSel = $(this).val();

    if (valSel == 0)
    {
        $('#DivCronExpression').hide();
        $('#DivDateTime').hide();
    }
    else if (valSel == 1)
    {
        $('#DivCronExpression').hide();
        $('#DivDateTime').show();
    }
    else if (valSel == 2)
    {
        $('#DivCronExpression').show();
        $('#DivDateTime').hide();
    }

});