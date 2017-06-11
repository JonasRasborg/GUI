



function calcTotal()
{
    var okseculotte = parseInt(document.getElementById("okseculotte").value * (212/100));
    var oksehakket = parseInt(document.getElementById("oksehakket").value * (216 / 100));
    var kylling = parseInt(document.getElementById("kylling").value * (149 / 100));
    var æg = parseInt(document.getElementById("æg").value * (138 / 100));
    var letmælk = parseInt(document.getElementById("letmælk").value * (48 / 100));
    var havregryn = parseInt(document.getElementById("havregryn").value * (368 / 100));
    var rugbrød = parseInt(document.getElementById("rugbrød").value * (200 / 100));
    var grønnebønner = parseInt(document.getElementById("grønnebønner").value * (31 / 100));
    var kartoffel = parseInt(document.getElementById("kartoffel").value * (86 / 100));

    var total = String(okseculotte + oksehakket + kylling + æg + letmælk + havregryn + rugbrød + grønnebønner + kartoffel)



    document.getElementById("total").innerHTML = "Total: " + total;
}



function done()
{
    
    var response = prompt("SAY WHAAT");

}