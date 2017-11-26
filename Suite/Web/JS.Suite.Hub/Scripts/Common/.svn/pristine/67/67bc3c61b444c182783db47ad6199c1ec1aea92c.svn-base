/*******************************************
 * Jumping Salamander Validation Module
 *******************************************/
  
(function (window, JS, $) {
    if (JS) {
        function Validation() {

            // override jquery validate plugin defaults
            $.validator.setDefaults({
                highlight: function (element) {
                    $(element).closest('.form-group').addClass('has-error');
                },
                unhighlight: function (element) {
                    $(element).closest('.form-group').removeClass('has-error');
                }
            });
        }

        JS.registerModule("validation", Validation);
    }
    else {
        throw "JS Core Module Missing.  Unable to register the Validation Module";
    }

})(window, JS, $);
