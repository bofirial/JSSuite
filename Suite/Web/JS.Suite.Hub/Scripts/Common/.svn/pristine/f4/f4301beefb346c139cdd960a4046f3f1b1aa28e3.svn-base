/*******************************************
 * Jumping Salamander Validation Module
 *******************************************/
  
(function (window, JS, $) {
    if (JS) {
        function Validation() {

            this.initializeRegion = function ($region) {
                var $forms = $region.parents('form').add($region.find('form'));

                $forms.removeData("validator");
                $forms.removeData("unobtrusiveValidation");
                $.validator.unobtrusive.parse($forms);
            };

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
