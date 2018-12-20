
function PatientQueue() {
    self = this;

    self.Zones = [];

    self.init = function () {
        self.setQueueTable();
        self.setButtonEvents(self);

        self.getCurrentServe()
            .then(function (data) {
                if (data.Mrn) {
                    self.getPatient(data.Mrn).then(function (pdata) {
                        $("#tokenno").html(data.TokenNumber);
                        $("#pin").html(pdata.PIN);
                        $("#gender").html(pdata.SexDescription);
                        $("#age").html(pdata.Age + " " + pdata.AgetTypeDescription + " Old");
                        $("#patientname").html(pdata.Name);
                        $("#curserveid").val(data.Id);
                    });
                }
            })
            .catch(function (e) {
                console.log(e);
            });

        self.getZones().then(function (data) {
            self.Zones = data;
        });
    };

    self.setButtonEvents = function (_self) {
        $('body').on('click', '#calltoken', function () {
            var servedid = $("#curserveid").val();
            var table = $('#queuetable').DataTable();
            var dtdata = table.row(0).data();
            if (table.data().length > 0) {
                _self.callToken(dtdata.Id).then(function (data) {
                    if (servedid > 0) {
                        _self.doneToken(servedid).then(function () {
                            $("#curserveid").val(dtdata.Id)
                        })
                    } else {
                        $("#curserveid").val(dtdata.Id)
                    }
                    _self.getPatient(dtdata.Mrn).then(function (patdata) {

                        $("#tokenno").html(dtdata.TokenNumber);
                        $("#pin").html(patdata.PIN);
                        $("#gender").html(patdata.SexDescription);
                        $("#age").html(patdata.Age + " " + patdata.AgetTypeDescription + " Old");
                        $("#patientname").html(patdata.Name);
                    });
                    table.draw();
                });
            } else {

                _self.doneToken(servedid).then(function () {
                    $("#curserveid").val(0)
                    _self.clearCurrentServe();
                })

            }

        });

        $('body').on('click', '#delaytoken', function () {

            var servedid = $("#curserveid").val();
            
            if (servedid > 0) {
                _self.delayToken(servedid).then(function (data) {
                    var table = $('#queuetable').DataTable();
                    table.draw();
                    _self.clearCurrentServe();
                });
            }
        });

        $('body').on('click', '#donetoken', function () {

            var servedid = $("#curserveid").val();
          
            if (servedid > 0) {
                _self.doneToken(servedid).then(function () {
                    var table = $('#queuetable').DataTable();
                    table.draw();
                    _self.clearCurrentServe();
                })
            }

        });

        $('body').on('click', '.transfertoken', function () {


            swal({
                title: "TRANSFER PATIENT",
     
                html: '<div class="col-md-12">'
                    + '<label class="form-label" style="font-size:14px;">Zone</label>'
                    + '<div class= "col-md-12" > '
                    + '<select id="zoneid" class= "form-control input-sm" style="width:100%;"></select>'
                    + '</div>' 
                    + '</div >',

                showCancelButton: true,
                confirmButtonText: 'Transfer',
                confirmButtonClass: "btn btn-sm",
                showLoaderOnConfirm: true,
                onOpen: function () {
                    var mappedZone = $.map(_self.Zones, function (item) {
                        return { id: item.Id, text: item.Code + " - " + item.Name };
                    });

                    $("#zoneid").html('').select2({ data: mappedZone });
                },
                preConfirm: () => {
                    var table = $('#queuetable').DataTable();
                    var dtrow = $(this).closest('tr');
                    var rowdata = table.row(dtrow).data();
                    var zoneid = $("#zoneid").select2('val');

                    return _self.transferToken(rowdata.Id, zoneid)
                        .then(function (data) {
                            return true;
                        })
                        .catch(error => {
                            return false;
                            console.log(error);
                        });
                },
                allowOutsideClick: false
            }).then((result) => {
                if (result.value) {
                    if (result.value == false) {
                        toaster({ type: 'error', title: 'Unable to transfer patient' });
                    } else {
                        toaster({ type: 'success', title: 'Patient successfuly transfered' });
                    }
                }
            });

        });

        $('body').on('click', '#transfertoken', function () {

            var tokenid = $("#curserveid").val();

            if (tokenid > 0) {
                swal({
                    title: "Transfer token to zone",
                    html: '<div class="col-md-12"><div class="col-md-12"><select id="zoneid" class="form-control"></select></div> <div>',

                    showCancelButton: true,
                    confirmButtonText: 'Transfer',
                    confirmButtonClass: "btn btn-sm",
                    showLoaderOnConfirm: true,
                    onOpen: function () {
                        $("#zoneid").html('');
                        $.each(_self.Zones, function (index, item) {
                            $("#zoneid").append("<option value=" + item.Id + ">" + item.Code + " - " + item.Name + "</option>")
                        });
                        $("#zoneid").select2();
                    },
                    preConfirm: () => {
                        var zoneid = $("#zoneid").select2('val');

                        return _self.transferToken(tokenid, zoneid)
                            .then(function (data) {
                                _self.clearCurrentServe();
                                return true;
                            })
                            .catch(error => {
                                console.log(error);
                                return false;
                            });
                    },
                    allowOutsideClick: false
                }).then((result) => {
                    if (result.value) {
                        if (result.value == false) {
                            toaster({ type: 'error', title: 'Unable to transfer patient' });
                        } else {
                            toaster({ type: 'success', title: 'Patient successfuly transfered' });
                        }
                    }
                });
            }
        });
    };

    self.setQueueTable = function () {
        $('#queuetable').DataTable({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,
            ordering: false,
            bLengthChange: false,
            ajax: {
                url: base.qms.getApiAddress() + "q/token/gettokensdt",
                headers: { "__sghis": base.qms.getCookie() },
                type: "POST",
                datatype: "json",
                data: function (d) {
                    d.Status = '1,3,4';
                }
            },
            columns: [
                {
                    "data": "TokenNumber", class: "text-vertical-middle inqueue-text text-center", title: "TOKEN", render: function (data) {

                        return "<b class='text-navy'>" + data + "</b>"
                    }
                },
                {
                    "data": "Mrn", class: "text-vertical-middle inqueue-text", title: "MRN"
                    , render: function (data) {
                        var org = base.getOrganizationDetail();

                        return org.IssueAuthorityCode + "." + ("0000000000" + data).substr(-10);


                    }
                },
                { "data": "Status", class: "text-vertical-middle inqueue-text", title: "STATUS" },
                { class: "text-center text-vertical-middle", defaultContent: '<button type="button" class="transfertoken btn btn-sm btn-primary inqueue-btn"> Transfer <i class="glyphicon glyphicon-share"></i></button>' }

            ]

        });
    }

    self.clearCurrentServe = function () {
        $("#tokenno").html("N / A");
        $("#pin").html("");
        $("#gender").html("");
        $("#age").html("");
        $("#patientname").html("- -");
        $("#curserveid").val(0)
    };

    self.callToken = function (id) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: base.qms.getApiAddress() + "q/token/call",
                headers: { "__sghis": base.qms.getCookie() },
                type: "POST",
                datatype: "json",
                data: { id: id },
                success: function (data) {
                    resolve(data);
                },
                error: function (e) {
                    console.log(e);
                    reject(e)
                }
            });
        });
    };

    self.delayToken = function (id) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: base.qms.getApiAddress() + "q/token/delay",
                headers: { "__sghis": base.qms.getCookie() },
                type: "POST",
                datatype: "json",
                data: { id: id },
                success: function (data) {
                    resolve(data);
                },
                error: function (e) {
                    console.log(e);
                    reject(e)
                }
            });
        });
    };

    self.doneToken = function (id) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: base.qms.getApiAddress() + "q/token/done",
                headers: { "__sghis": base.qms.getCookie() },
                type: "POST",
                datatype: "json",
                data: { id: id },
                success: function (data) {
                    resolve(data);
                },
                error: function (e) {
                    console.log(e);
                    reject(e)
                }
            });
        });
    };

    self.transferToken = function (id, zoneid) {

        return new Promise(function (resolve, reject) {
            $.ajax({
                url: base.qms.getApiAddress() + "q/token/transfer",
                headers: { "__sghis": base.qms.getCookie() },
                type: "POST",
                datatype: "json",
                data: {
                    id: id,
                    zoneId: zoneid
                },
                success: function (data) {
                    resolve(data);
                },
                error: function (e) {
                    console.log('transfer fail');
                    reject(e)
                }
            });
        });
    };

    self.getPatient = function (registrationNo) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: base.url + "api/patients?registrationno=" + registrationNo,
                type: "GET",
                datatype: "json",
                success: function (data) {
                    resolve(data);
                },
                error: function (e) {
                    console.log(e);
                    reject(e)
                }
            });
        });
    };

    self.getCurrentServe = function () {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: base.qms.getApiAddress() + "q/token/getcurrentserving?ipAddress=" + base.qms.getClientIPAddress(),
                type: "GET",
                datatype: "json",
                success: function (data) {
                    resolve(data);
                },
                error: function (e) {
                    reject(e);
                }
            });
        });
    };

    self.getZones = function () {
        return new Promise(function (resolve, reject) {
            $.ajax({
                url: base.qms.getApiAddress() + "q/admin/getzones",
                type: "GET",
                datatype: "json",
                success: function (data) {
                    resolve(data);
                },
                error: function (e) {
                    reject(e);
                }
            });
        });
    };

}