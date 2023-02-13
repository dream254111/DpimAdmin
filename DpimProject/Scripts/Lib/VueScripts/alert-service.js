const $alert = function (title, message, alert_type, callback) {
    const title_text = `<span class="text-${alert_type}"><i class="fa fa-${alert_type === 'danger' ? 'times-circle' : alert_type === 'success' ? 'check-circle' : alert_type === 'info' ? 'info-circle' : alert_type === 'warning' ? 'exclamation-circle' : 'info-circle'}"></i> ${title}</span>`;
    $.alert({
        title: title_text,
        content: message,
        btnClass: 'btn-info',
        theme: 'modern',
        animation: 'scale',
        closeAnimation: 'scale',
        animateFromElement: false,
        draggable: false,
        autoClose: 'ok|5000',
       buttons: {
            ok: {
                text: 'OK',
                btnClass: 'btn-info'

            }
        },
        onClose: () => {
            if (callback) {
                callback();
            }
        }
    });
};


const $confirm = (text, ok_function, no_fuction) => {
    $.confirm({
        title: '<i class="fa fa-question-circle"></i>',
        content: text,
        theme: 'material',
        animation: 'opacity',
        closeAnimation: 'opacity',
        animateFromElement: false,
        draggable: false,
        closeIcon: true,
        buttons: {
            yes: {
                text: 'Yes',
                btnClass: 'btn-primary',
                action: () => {
                    if (ok_function) {
                        ok_function();
                    }
                }
            },
            no: {
                text: 'No',
                btnClass: 'btn-danger',
                action: () => {
                    if (no_fuction) {
                        no_fuction();
                    }
                }
            }
        }
    });
};
const $confirm2 = (title ,text, okFunction, noFunction) => {
    $.confirm({
        title: title,
        content: text,
        theme: 'supervan',
        columnClass: 'col-md-12',
        buttons: {
            ok: {
                text: "Yes",
                btnClass: 'btn-default',
                action: () => {
                    if (okFunction) {
                        okFunction();
                    }
                }
            },
            no: {
                text: "No",
                btnClass: "btn-danger",
                action: () => {
                    if (noFunction) {
                        noFunction();
                    }
                }
            }
        }
    });
};
const $approve = function (text, ok_function, no_fuction) {
    $.confirm({
        title: `<span style="color:blue"><i class="fa fa-check"></i> Submit Approve</span>`,
        content: text,
        theme: 'material',
        animation: 'opacity',
        closeAnimation: 'opacity',
        backgroundDismiss: true,
        animateFromElement: false,
        draggable: false,
        closeIcon: true,
        buttons: {
            yes: {
                text: 'Yes',
                btnClass: 'btn-primary',
                action: () => {
                    if (ok_function) {
                        ok_function();
                    }
                }
            },
            no: {
                text: 'No',
                btnClass: 'btn-danger',
                action: () => {
                    if (no_fuction) {
                        no_fuction();
                    }
                }
            }
        }
    });
};
const $prompt = function (text, callback) {
    $.confirm({
        title: 'Prompt!',
        content: '<p>' + text + '</p>' +
            '<form action="" class="formName">' +
            '<div class="form-group">' +
            '<textarea type="text" placeholder="" class="name form-control" rows="3"></textarea>' +
            '</div>' +
            '</form>',
        theme: 'material',
        animation: 'opacity',
        closeAnimation: 'opacity',
        animateFromElement: false,
        buttons: {
            formSubmit: {
                text: 'OK',
                btnClass: 'btn-blue',
                action: () => {
                    var name = this.$content.find('.name').val();
                    callback(name);
                }
            },
            cancel: () => {

            }
        },
        onContentReady: () => {
            var jc = this;
            this.$content.find('form').on('submit', function (e) {
                e.preventDefault();
                jc.$$formSubmit.trigger('click');
            });
        }
    });
}