//window.addEventListener('DOMContentLoaded', function () {
//    var image = document.getElementById('image');
//    var cropBoxData;
//    var canvasData;
//    var cropper;

//    $('#modal').on('shown.bs.modal', function () {
//        cropper = new Cropper(image, {
//            autoCropArea: 0.5,
//            ready: function () {

//                // Strict mode: set crop box data first
//                cropper.setCropBoxData(cropBoxData).setCanvasData(canvasData);
//            }
//        });
//    }).on('hidden.bs.modal', function () {
//        cropBoxData = cropper.getCropBoxData();
//        canvasData = cropper.getCanvasData();
//        cropper.destroy();
//    });
//});

window.onload = function () {

    'use strict';

    var Cropper = window.Cropper;
    var URL = window.URL || window.webkitURL;
    var container = document.querySelector('.img-container');
    var avatarPreview = document.getElementById('avatarPreview').firstElementChild;
    var loadImageBtn = document.getElementById('loadImageBtn'); 
    var image = container.getElementsByTagName('img').item(0);
    var actions = document.getElementById('actions');
    var options = {
        aspectRatio: 1/1,
        preview: '.img-preview',
        ready: function (e) {
        },
        cropstart: function (e) {
        },
        cropmove: function (e) {
        },
        cropend: function (e) {
        },
        crop: function (e) {
            var data = e.detail;

            //console.log(e.type);
            //dataX.value = Math.round(data.x);
            //dataY.value = Math.round(data.y);
            //dataHeight.value = Math.round(data.height);
            //dataWidth.value = Math.round(data.width);
            //dataRotate.value = typeof data.rotate !== 'undefined' ? data.rotate : '';
            //dataScaleX.value = typeof data.scaleX !== 'undefined' ? data.scaleX : '';
            //dataScaleY.value = typeof data.scaleY !== 'undefined' ? data.scaleY : '';
        },
        zoom: function (e) {
            console.log(e.type, e.detail.ratio);
        }
    };
    var cropper = new Cropper(image, options);
    var originalImageURL = image.src;
    var uploadedImageType = 'image/jpeg';
    var uploadedImageURL;

    // Tooltip
    $('[data-toggle="tooltip"]').tooltip();

    // Buttons
    if (!document.createElement('canvas').getContext) {
        $('button[data-method="getCroppedCanvas"]').prop('disabled', true);
    }

    if (typeof document.createElement('cropper').style.transition === 'undefined') {
        $('button[data-method="rotate"]').prop('disabled', true);
        $('button[data-method="scale"]').prop('disabled', true);
    }

    // Download
    //if (typeof download.download === 'undefined') {
    //  download.className += ' disabled';
    //}

    // Options
    //actions.querySelector('.docs-toggles').onchange = function (event) {
    //  var e = event || window.event;
    //  var target = e.target || e.srcElement;
    //  var cropBoxData;
    //  var canvasData;
    //  var isCheckbox;
    //  var isRadio;

    //  if (!cropper) {
    //    return;
    //  }

    //  if (target.tagName.toLowerCase() === 'label') {
    //    target = target.querySelector('input');
    //  }

    //  isCheckbox = target.type === 'checkbox';
    //  isRadio = target.type === 'radio';

    //  if (isCheckbox || isRadio) {
    //    if (isCheckbox) {
    //      options[target.name] = target.checked;
    //      cropBoxData = cropper.getCropBoxData();
    //      canvasData = cropper.getCanvasData();

    //      options.ready = function () {
    //        console.log('ready');
    //        cropper.setCropBoxData(cropBoxData).setCanvasData(canvasData);
    //      };
    //    } else {
    //      options[target.name] = target.value;
    //      options.ready = function () {
    //        console.log('ready');
    //      };
    //    }

    //    // Restart
    //    cropper.destroy();
    //    cropper = new Cropper(image, options);
    //  }
    //};

    // Methods
    actions.querySelector('.docs-buttons').onclick = function (event) {
        var e = event || window.event;
        var target = e.target || e.srcElement;
        var cropped;
        var result;
        var input;
        var data;

        if (!cropper) {
            return;
        }

        while (target !== this) {
            if (target.getAttribute('data-method')) {
                break;
            }

            target = target.parentNode;
        }

        if (target === this || target.disabled || target.className.indexOf('disabled') > -1) {
            return;
        }

        data = {
            method: target.getAttribute('data-method'),
            target: target.getAttribute('data-target'),
            option: target.getAttribute('data-option') || undefined,
            secondOption: target.getAttribute('data-second-option') || undefined
        };

        cropped = cropper.cropped;

        if (data.method) {
            if (typeof data.target !== 'undefined') {
                input = document.querySelector(data.target);

                if (!target.hasAttribute('data-option') && data.target && input) {
                    try {
                        data.option = JSON.parse(input.value);
                    } catch (e) {
                        console.log(e.message);
                    }
                }
            }

            switch (data.method) {
                case 'rotate':
                    if (cropped && options.viewMode > 0) {
                        cropper.clear();
                    }

                    break;

                case 'getCroppedCanvas':
                    try {
                        data.option = JSON.parse(data.option);
                    } catch (e) {
                        console.log(e.message);
                    }

                    if (uploadedImageType === 'image/jpeg') {
                        if (!data.option) {
                            data.option = {};
                        }

                        data.option.fillColor = '#fff';
                    }

                    break;
            }

            result = cropper[data.method](data.option, data.secondOption);

            switch (data.method) {
                case 'rotate':
                    if (cropped && options.viewMode > 0) {
                        cropper.crop();
                    }

                    break;
                case 'getCroppedCanvas':
                    if (result) {
                        var image = result.toDataURL(uploadedImageType);
                        $(avatarPreview).attr('src', image);
                        var hiddenInput = document.getElementById('hiddenArea');
                        hiddenInput.value = result.toDataURL(uploadedImageType);
                        $('#modal').modal('hide');
                    }

                    break;
            }

            if (typeof result === 'object' && result !== cropper && input) {
                try {
                    input.value = JSON.stringify(result);
                } catch (e) {
                    console.log(e.message);
                }
            }
        }
    };
    // Import image
    var inputImage = document.getElementById('inputImage');

    if (URL) {
        inputImage.onchange = function () {
            var files = this.files;
            var file;

            if (cropper && files && files.length) {
                file = files[0];

                if (/^image\/\w+/.test(file.type)) {
                    uploadedImageType = file.type;

                    if (uploadedImageURL) {
                        URL.revokeObjectURL(uploadedImageURL);
                    }

                    image.src = uploadedImageURL = URL.createObjectURL(file);
                    cropper.destroy();
                    cropper = new Cropper(image, options);
                    inputImage.value = null;
                } else {
                    window.alert('Please choose an image file.');
                }
            }
        };
    } else {
        inputImage.disabled = true;
        inputImage.parentNode.className += ' disabled';
    }

    avatarPreview.onclick = function () {
        $('#modal').modal('show');
    };

    loadImageBtn.onclick = function () {
        $('#modal').modal('show');
    };
    
    $('#modal').on('shown.bs.modal', function () {
        image.src = "";
        //document.getElementById("uploadForm").reset();
        cropper.destroy();
    }).on('hidden.bs.modal', function () {
        image.src = "";
        //document.getElementById("uploadForm").reset();
        cropper.destroy();
    });
};
