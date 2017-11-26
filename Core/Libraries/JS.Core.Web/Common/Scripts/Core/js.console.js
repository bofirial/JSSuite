/*******************************************
 * Jumping Salamander Console Module
 *******************************************/

(function (window, JS, $) {
    if (JS) {
        function Console() {

            var self = this;

            this.error = function () {

                if (typeof (window.console) != "undefined" && typeof (window.console.error) != "undefined") {

                    switch (arguments.length) {
                        case 1:
                            window.console.error(arguments[0]);
                            break;
                        case 2:
                            window.console.error(arguments[0], arguments[0]);
                            break;
                        case 3:
                            window.console.error(arguments[0], arguments[0], arguments[0]);
                            break;
                        case 4:
                            window.console.error(arguments[0], arguments[0], arguments[0], arguments[0]);
                            break;
                        default:
                            self.error("Unsupported number of arguments in JS.console.error().");
                            break;

                    }
                }
            };

            this.log = function () {

                if (typeof (window.console) != "undefined" && typeof (window.console.log) != "undefined" ) {

                    switch (arguments.length) {
                        case 1:
                            window.console.log(arguments[0]);
                            break;
                        case 2:
                            window.console.log(arguments[0], arguments[0]);
                            break;
                        case 3:
                            window.console.log(arguments[0], arguments[0], arguments[0]);
                            break;
                        case 4:
                            window.console.log(arguments[0], arguments[0], arguments[0], arguments[0]);
                            break;
                        default:
                            self.error("Unsupported number of arguments in JS.console.log().");
                            break;

                    }
                }
            };
        }

        JS.registerModule("console", Console);
    }
    else {
        throw "JS Core Module Missing.  Unable to register the Console Module";
    }

})(window, JS, $);