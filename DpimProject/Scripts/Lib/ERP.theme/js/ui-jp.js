+function ($) {
    $(function () {
        $("[ui-jq]").each(function () {
            var self = $(this);
            var options = [];
            if (self.attr('wh') == "1") {
                h = (window.innerHeight - 190);
                options = eval('[{ "height": "' + h + 'px","size": "0px"}]');
            } else {
                options = eval('[' + self.attr('ui-options') + ']');
            }

            if ($.isPlainObject(options[0])) {
                options[0] = $.extend({}, options[0]);
            }

            uiLoad.load(jp_config[self.attr('ui-jq')]).then(function () {
                self[self.attr('ui-jq')].apply(self, options);
            });
        });
    });
}(jQuery);