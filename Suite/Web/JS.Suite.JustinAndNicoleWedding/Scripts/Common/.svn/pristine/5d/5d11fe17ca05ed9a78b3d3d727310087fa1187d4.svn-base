///*******************************************
// * Jumping Salamander Hidden If Module
// *******************************************/

//(function (window, JS, $) {
//    if (JS) {
//        function HiddenIf() {

//            var self = this;

//            function hideIfConditionIsMet($hiddenIf, jsConditions) {

//                if (JS.controls.jsCondition.anyConditionIsMet(jsConditions)) {
//                    $hiddenIf.addClass('hide');

//                    $hiddenIf.trigger("hide");
//                }
//                else {
//                    $hiddenIf.removeClass('hide');

//                    $hiddenIf.trigger("show");
//                }
//            }

//            this.initializeRegion = function ($region) {

//                $('[data-hidden-if-keys]', $region).each(function (i, element) {
//                    var $hiddenIf = $(element);

//                    if (!$hiddenIf.isInitialized('HiddenIf')) {
//                        var keys = $hiddenIf.data('hiddenIfKeys').toString().split(','),
//                            createConditionsResult = JS.controls.jsCondition.createConditionsFromKeys($hiddenIf, keys),
//                            jsConditions = createConditionsResult.jsConditions,
//                            $targets = createConditionsResult.$targets;

//                        var hideCurrentControlIfConditionIsMet = hideIfConditionIsMet.bind(null, $hiddenIf, jsConditions);

//                        hideCurrentControlIfConditionIsMet();

//                        $targets.change(hideCurrentControlIfConditionIsMet);

//                        $hiddenIf.markInitialized('HiddenIf');
//                    }
//                });
//            };
//        }

//        JS.registerModule("controls.hiddenIf", HiddenIf);
//    }
//    else {
//        throw "JS Core Module Missing.  Unable to register the HiddenIf Module";
//    }

//})(window, JS, $);