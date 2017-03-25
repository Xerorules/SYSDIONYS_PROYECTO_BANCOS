function PasarValores(cod, nom, rucDni) {
    document.getElementById('ContentPlaceHolder1_txtID_CLIENTE').value = cod;
    document.getElementById('ContentPlaceHolder1_txtDESCRIPCION').value = nom;
    document.getElementById('ContentPlaceHolder1_txtCLIENTE').value = rucDni;
    $find('ContentPlaceHolder1_ModalPopupp').hide();
}