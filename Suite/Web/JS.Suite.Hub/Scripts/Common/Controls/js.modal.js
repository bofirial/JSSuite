﻿/*******************************************
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
                            var $gridForm = $modalLink.closest('.gridForm');

                            self.open($modalLink.data('modalLink'));

                            if ($gridForm.length > 0) {
                                $('#modal').one('hidden.bs.modal', function () { $gridForm.submit(); });
                            }
                        });

                        $modalLink.markInitialized('ModalLink');
                    }
                });

                $('[data-modal-save-form]', $region).each(function (i, element) {
                    var $modalSaveButton = $(element),
                        $modalSaveForm = $('#' + $modalSaveButton.data('modalSaveForm')),
                        $modalContent = $('.modal-content');

                    if (!$modalSaveButton.isInitialized('ModalSaveButton')) {
                        $modalSaveButton.click(function (e) {

                            if ($modalSaveForm.valid()) {
                                $modalContent.startLoadingIndicator();

                                $.ajax({
                                    url: $modalSaveForm.attr('action'),
                                    type: 'POST',
                                    data: $modalSaveForm.serialize(),
                                    success: function (data) {

                                        $modalContent.empty();

                                        $modalContent.append(data);

                                        JS.initializeRegion($modalContent);

                                        $modalContent.stopLoadingIndicator();
                                    }
                                });
                            }

                            
                        });

                        $modalSaveButton.markInitialized('ModalSaveButton');
                    }
                });

                $('[data-modal-close]', $region).each(function (i, element) {
                    var $modalCloseButton = $(element),
                        $modalContent = $('.modal-content');

                    if (!$modalCloseButton.isInitialized('ModalCloseButton')) {
                        $modalCloseButton.click(function (e) {

                                $modalContent.startLoadingIndicator();

                                $.ajax({
                                    url: $modalCloseButton.data('modalClose'),
                                    type: 'POST',
                                    success: function (data) {

                                        if (data.Success) {
                                            $('#modal').modal('hide');
                                        }
                                        else {
                                            $modalContent.empty();

                                            $modalContent.append(data);

                                            JS.initializeRegion($modalContent);

                                            $modalContent.stopLoadingIndicator();
                                        }
                                    }
                                });


                        });

                        $modalCloseButton.markInitialized('ModalCloseButton');
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

                            JS.initializeRegion($modalContent);

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