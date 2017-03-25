function Actualizar() {
    var btn = document.getElementById("<%=btnACTUALIZARr.ClientID %>");
    btn.click();
}

function mensaje(cod, est) {
    $.msgBox({
        title: "Mensaje",
        content: "Desea cambiar el estado?",
        type: "info",
        buttons: [{ value: "Yes" }, { value: "No" }],
        success: function (result) {
            if (result == "Yes") { //Ok    
                document.getElementById('<%=HiddenField3.ClientID%>').value = cod + '-' + est;
                var objO = document.getElementById('<%=Button3.ClientID %>');
                objO.click();


            }
        }
    });
}