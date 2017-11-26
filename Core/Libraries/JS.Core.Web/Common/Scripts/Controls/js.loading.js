/*******************************************
 * Jumping Salamander Loading Module
 *******************************************/

(function (window, JS, $) {
    if (JS) {
        function Loading() {

            var self = this
                minDisplayTime = 500;

            $.fn.extend({
                startLoadingIndicator: function () {
                    return this.each(function () {
                        var $this = $(this),
                            currentTime = new Date();

                        $this.addClass('loadingParent');

                        $this.prepend("<div class='loading'></div>");

                        $this.data('earliestEndTime', currentTime.setMilliseconds(currentTime.getMilliseconds() + minDisplayTime));
                    });
                },
                stopLoadingIndicator: function () {
                    return this.each(function () {
                        var $this = $(this),
                            earliestEndTime = new Date($this.data('earliestEndTime')),
                            currentTime = new Date();

                        if (earliestEndTime < currentTime) {
                            $this.find(".loading").remove();

                            $this.removeClass('loadingParent');
                        }
                        else {
                            window.setTimeout(function () {
                                $this.stopLoadingIndicator();
                            }, earliestEndTime.getTime() - currentTime.getTime());
                        }
                    });
                },
            });
        }

        JS.registerModule("controls.loading", Loading);
    }
    else {
        throw "JS Core Module Missing.  Unable to register the Loading Module";
    }

})(window, JS, $);