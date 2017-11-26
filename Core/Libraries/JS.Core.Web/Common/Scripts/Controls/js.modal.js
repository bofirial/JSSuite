/*******************************************
 * Jumping Salamander Modal Module
 *******************************************/

(function (window, JS, $) {
    if (JS) {
        function Modal() {

            var self = this;

            this.initializeRegion = function ($region) {

                $('[data-modal-link]', $region).each(function (i, element) {
                    var $modalLink = $(element);
                             
                    if (!$modalLink.isInitialized('ModalLink')) {
                        $modalLink.click(function (e) {
                            self.open($modalLink.data('modalLink'));
                        });

                        $modalLink.markInitialized('ModalLink');
                    }
                });
            };

            this.open = function (url) {
                var $modal = $('#modal'),
                    $modalContent = $('.modal-content');

                if (!$modal.hasClass('in')) {
                    $modal.modal('show');

                    $modalContent.empty();

                    $modalContent.startLoadingIndicator();

                    $.ajax({
                        url: url,
                        success: function (data) {
                            $modalContent.append(data);

                            $modalContent.stopLoadingIndicator();
                        }
                    });
                }
            }
        }

        JS.registerModule("controls.modal", Modal);
    }
    else {
        throw "JS Core Module Missing.  Unable to register the Modal Module";
    }

})(window, JS, $);