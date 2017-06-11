var pkglist = new PackageList();
var cell6;
document.getElementById("addRow").addEventListener("click", function(event) {
    event.preventDefault();

    pkglist.SetVacationType(document.getElementById("vactype").value);
    pkglist.SetDays(document.getElementById("days").value);
    pkglist.SetAmount(document.getElementById("amount").value);
    pkglist.SetSubject(document.getElementById("subject").value);

    /* Inspirationskilde til .insertRow http://www.sitepoint.com/forums/showthread.php?517740-How-Click-button-add-another-row-table d. 9/06 */

    var newRow = document.getElementById('fullTabel').insertRow(1);
    var cell1 = newRow.insertCell(0);
    var cell2 = newRow.insertCell(1);
    var cell3 = newRow.insertCell(2);
    var cell4 = newRow.insertCell(3);
    var cell5 = newRow.insertCell(4);
    cell6 = newRow.insertCell(5);

    var chkbox = document.createElement("input");
    chkbox.setAttribute("type", "checkbox");

    if (pkglist.GetAmount() == "") {
        pkglist.SetAmount(pkglist.GetDays());
    }

 
    cell1.innerHTML = pkglist.GetVacationType();
    cell2.innerHTML = pkglist.GetDays();
    cell3.innerHTML = pkglist.GetAmount();
    cell4.innerHTML = pkglist.GetSubject();
    cell5.innerHTML = cell5.appendChild(chkbox).value();
    /* Fejl i JavaScript console */
});