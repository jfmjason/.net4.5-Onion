var RemoveCount = 1;
var Count = 1;

qmsjs = {
    TokenToArabic: function ($token) {

        var f = ['9', '8', '7', '6', '5', '4', '3', '2', '1', '0', 'T', 'P'];
        var r = ['&#1641;', '&#1640;', '&#1639;', '&#1638;', '&#1637;', '&#1636;', '&#1635;', '&#1634;', '&#1633;', '&#1632;', 'T', 'P'];

        return $token.replace(/\d/g, function (m) {
            return r[f.indexOf(m)];
        });
    },
    CurrentQueue: function () {
    },
    CurrentQueue2: function () {
        $('.qms_content_token_2').append('<div class="qms_token_queue_2">T1234</div>');
        $('.qms_content_counter_2').append('<div class="qms_counter_queue_2">C1</div>');

        $('.qms_content_token_2').append('<div class="qms_token_queue_2">T5678</div>');

        $('.qms_content_counter_2').append('<div class="qms_counter_queue_2">C2</div>');

        $('.qms_content_token_2').append('<div class="qms_token_queue_2">T9000</div>');
        $('.qms_content_counter_2').append('<div class="qms_counter_queue_2">C3</div>');

        $('.qms_content_token_2').append('<div class="qms_token_queue_2">T9001</div>');
        $('.qms_content_counter_2').append('<div class="qms_counter_queue_2">C4</div>');

        $('.qms_content_token_2').append('<div class="qms_token_queue_2">T9123</div>');
        $('.qms_content_counter_2').append('<div class="qms_counter_queue_2">C5</div>');
    },
    FormatToken: function ($prefix, $value) {
        return $prefix + ("0000" + $value).slice(-4);
    },
    AppendToken: function () {
        console.log();
        //setInterval(() => {
        //    console.log(Count);
        //    $('#markcontainer').append('<div id="' + qmsjs.FormatToken("T", Count) + '" class="qms_marque_content_1">' +
        //        qmsjs.FormatToken("T", Count) + ' - <span class="qms_marquee_arabic_1">' + qmsjs.TokenToArabic(qmsjs.FormatToken("T", Count)) + '</span></div>');
        //    Count = Count + 1;

        //}, 1 * 1000);
    },
    RemoveToken: function () {
        setInterval(() => {

            $('#' + qmsjs.FormatToken("T", RemoveCount)).remove();
            RemoveCount = RemoveCount + 1;

        }, 5 * 1000);
    },

    initZoneIndex: function () {
        var table = $('#tblZone').DataTable({
            processing: true,
            serverSide: true,
            paging: true,
            searching: false,
            ordering: false,
            bLengthChange: false,
            ajax: {
                url: $("#hidAdminUrl").data("getzones"),
                type: "POST",
                datatype: "json",
                data: function (d) {
                    //d.DocumentTypeId = docTypeId;
                    //d.WorkStationIp = workStationIp;
                    //d.RefNo = $("#tbRefTran").val();
                }
            }, columns: [{ "data": "Id" },
            { "data": "Code" },
            { "data": "Name" }
            ]
            //,
            //createdRow: function (row, data, index) {
            //    $(row).addClass('actualrow');

            //}
        });
    }
};