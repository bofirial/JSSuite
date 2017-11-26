/*******************************************
 * Jumping Salamander Submit To Action Module
 *******************************************/

(function (window, JS, $) {
    if (JS) {
        function SubmitToAction() {

            var self = this;

            this.initializeRegion = function ($region) {

                $('[data-submit-to-action]', $region).each(function (i, element) {
                    var $submitToAction = $(element);

                    if (!$submitToAction.isInitialized('SubmitToAction')) {
                        $submitToAction.on("click", function (e) {
                            e.preventDefault();

                            $submitToAction.closest("form").attr('action', $submitToAction.data('submitToAction')).submit();
                        });

                        $submitToAction.markInitialized('SubmitToAction');
                    }
                });
            };
        }

        JS.registerModule("controls.submitToAction", SubmitToAction);
    }
    else {
        throw "JS Core Module Missing.  Unable to register the SubmitToAction Module";
    }

})(window, JS, $);