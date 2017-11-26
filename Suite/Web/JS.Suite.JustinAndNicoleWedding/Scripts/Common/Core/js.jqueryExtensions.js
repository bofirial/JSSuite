/*******************************************
 * Jumping Salamander JQuery Extensions Module
 *******************************************/

(function (window, JS, $) {
    if (JS) {
        function JQueryExtensions() {

            $.fn.extend({
                serializeJSON: function () {
                    var json = {};

                    $.each($(this).serializeArray(), function () {
                        json[this.name] = this.value;
                    });

                    return json;
                }
            });           
        }

        JS.registerModule("jqueryExtensions", JQueryExtensions);
    }
    else {
        throw "JS Core Module Missing.  Unable to register the JQuery Extensions Module";
    }

})(window, JS, $);