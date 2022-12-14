{
    function checkday(start,end) {
        var from1 = $("#start").val().split("/");
        /*var dateS = new Date(from1[2], from1[1] - 1, from1[0]);*/
        var from2 = $("#end").val().split("/");
        /*var dateE = new Date(from2[2], from2[1] - 1, from2[0]);*/

        var now = new Date().toISOString().split('T')[0]; ;
      

        if (from1 > now || from2 > now) {
            alert("Date end or Date start must have below persent !");
            return false;
        }
        if (from2 <= from1) {
            alert("Date end more than Date start !");
            return false;
        }
        if (from1 == "" || from2 == "") {
            alert("Must choice Date !");
            return false;
        }
        return true;
    }
}
