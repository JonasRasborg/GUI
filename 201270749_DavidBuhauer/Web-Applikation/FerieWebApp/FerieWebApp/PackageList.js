function PackageList() {
    var amount;
    var subject;
    var vactype;
    var days;

    this.SetAmount = function(value) {
        amount = value;
    }

    this.GetAmount = function() {
        return amount;
    }

    this.SetSubject = function(value) {
        subject = value;
    }

    this.GetSubject = function() {
        return subject;
    }

    this.SetVacationType = function (value) {
        vactype = value;
    }

    this.GetVacationType = function () {
        return vactype;
    }

    this.SetDays = function (value) {
        days = value;
    }

    this.GetDays = function () {
        return days;
    }

}