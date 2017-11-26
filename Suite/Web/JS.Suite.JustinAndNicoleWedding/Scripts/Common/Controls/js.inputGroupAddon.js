/*******************************************
 * Jumping Salamander Input Group Add On Module
 *******************************************/

(function (window, JS, $) {
    if (JS) {
        function InputGroupAddOn() {

            var self = this;

            function applyInputGroupAddOnFixClasses($inputGroup) {

                $inputGroup.children().removeClass('input-group-addon-first-fix').removeClass('input-group-addon-last-fix');

                var visibleChildren = $inputGroup.children(':not(:hidden)');

                if (!visibleChildren.first().is(':first-child')) {
                    visibleChildren.first().addClass('input-group-addon-first-fix');
                }

                if (!visibleChildren.last().is(':last-child')) {
                    visibleChildren.last().addClass('input-group-addon-last-fix');
                }
            }

            this.initializeRegion = function ($region) {

                $('.input-group', $region).each(function (i, element) {
                    var $inputGroup = $(element);

                    if (!$inputGroup.isInitialized('InputGroupAddOn')) {

                        $inputGroup.bind('hide show', applyInputGroupAddOnFixClasses.bind(null, $inputGroup));

                        applyInputGroupAddOnFixClasses($inputGroup);

                        $inputGroup.markInitialized('InputGroupAddOn');
                    }
                });
            };
        }

        JS.registerModule("controls.inputGroupAddOn", InputGroupAddOn);
    }
    else {
        throw "JS Core Module Missing.  Unable to register the InputGroupAddOn Module";
    }

})(window, JS, $);