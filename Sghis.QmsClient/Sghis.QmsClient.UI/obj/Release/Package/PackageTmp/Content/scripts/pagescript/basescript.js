function BaseScript() {
    self = this;
    self.qms = {};
    self.url = "";

    self.setOrganizationDetail = function (organization) {
        if (localStorage["ORGANIZATION"] == undefined) {
            localStorage.setItem("ORGANIZATION", JSON.stringify(organization));
        }
    }

    self.getOrganizationDetail = function () {
        if (localStorage["ORGANIZATION"] != undefined) {
            return JSON.parse(localStorage.getItem("ORGANIZATION"));
        }
    }

    self.qms.getClientInfo = function (ip) {

        if (localStorage["QMSCLIENT"] == undefined) {
            return new Promise(function (resolve, reject) {
                var headercookie = base.qms.getCookie("__sghis")
                $.ajax({
                    url: base.qms.getApiAddress() + "q/admin/getclient",
                    type: 'get',
                    headers: { "__sghis": headercookie },
                    data: {
                        ipAddress: ip
                    },
                    success: function (data) {
                        if (data != "") {
                            localStorage.setItem("QMSCLIENT", JSON.stringify(data));
                        }
                        resolve(data);
                    },
                    error: function (data) {
                        console.log("fail to get qms client info");
                        reject(data);
                    }
                });
            });
        } else {
            return new Promise(function (resolve, reject) {
                resolve(JSON.parse(localStorage.getItem("QMSCLIENT")));
            });
        }

    }

    self.qms.getHubAddress = function () {

        if (localStorage["QMSHUB"] == undefined) {
            return "Not set";
        } else {
            return localStorage.getItem("QMSHUB");
        }
    }

    self.qms.getApiAddress = function () {
        if (localStorage["QMSAPI"] == undefined) {
            return "Not set";
        } else {
            return localStorage.getItem("QMSAPI");
        }
    }

    self.qms.getClientIPAddress = function () {

        if (localStorage["CLIENTIPADDRESS"] == undefined) {
            return "Not set";
        } else {
            return localStorage.getItem("CLIENTIPADDRESS");
        }
    }

    self.qms.setHubAddress = function (url) {

        if (localStorage["QMSHUB"] == undefined) {
            localStorage.setItem("QMSHUB", url);
        }
    }

    self.qms.setApiAddress = function (url) {
        if (localStorage["QMSAPI"] == undefined) {
            localStorage.setItem("QMSAPI", url);
        }
    }

    self.qms.setClientIPAddress = function (ip) {
        if (localStorage["CLIENTIPADDRESS"] == undefined) {
            localStorage.setItem("CLIENTIPADDRESS", ip);
        }
    }

    self.getCookie = function (name) {
        var nameEQ = name + "=";
        var ca = document.cookie.split(';');
        for (var i = 0; i < ca.length; i++) {
            var c = ca[i];
            while (c.charAt(0) === ' ') c = c.substring(1, c.length);
            if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length, c.length);
        }
        return null;

    }

    self.qms.getCookie = function () {
        return base.getCookie("__sghis");
    }

    self.setMenuEvents = function () {

        $("li.treeview").click(function () {
            $(".treeview").removeClass("active");
            $(this).addClass("active");
        })

        $("ul.treeview-menu li a").click(function () {
            $("ul.treeview-menu li a").removeClass("text-aqua");
            $(this).addClass("text-aqua");
        })

        $("#sidebar-search").keyup(function () {

            if ($(this).val().trim().length > 0) {
                var searchterm = $(this).val();

                var searchedElem = $('.treeview-menu li').filter(function () {
                    return $(this).text().toLowerCase().indexOf(searchterm.toLowerCase()) > -1;
                })

                var excludedElem = $('.treeview-menu li').filter(function () {
                    return $(this).text().toLowerCase().indexOf(searchterm.toLowerCase()) == -1;
                })
                debugger
                $(excludedElem).closest("li.treeview")
                    .removeClass("active")
                    .removeClass("menu-open");
                $(excludedElem).closest("ul.treeview-menu")
                    .hide();
                $(excludedElem).hide();


                $(searchedElem).closest("li.treeview")
                    .addClass("active")
                    .addClass("menu-open");
                $(excludedElem).closest("ul.treeview-menu")
                    .show();

                $(searchedElem).show();

                //}
            } else {
                $("li.treeview.menu-open li").show();
                $("li.treeview.menu-open ul.treeview-menu").hide();
                $("li.treeview")
                    .removeClass("active")
                    .removeClass("menu-open");
            }
        })


    }

    self.ResetSettings = function () {
        localStorage.removeItem("CLIENTIPADDRESS");
        localStorage.removeItem("QMSAPI");
        localStorage.removeItem("QMSHUB");
        localStorage.removeItem("QMSCLIENT");
        localStorage.removeItem("ORGANIZATION");

        window.location.reload();
    };
}