/*!
* Start Bootstrap - Clean Blog v5.1.0 (https://startbootstrap.com/theme/clean-blog)
* Copyright 2013-2021 Start Bootstrap
* Licensed under MIT (https://github.com/StartBootstrap/startbootstrap-clean-blog/blob/master/LICENSE)
*/

//$(document).ready(function () {

//    $('#summernote').summernote({
//        callbacks: {
//            onImageUpload: function (files) {
//                sendFile(files[0]);
//            }
//        }
//    });

//    //Upload files to the server
//    function sendFile(file) {
//        var data = new FormData();
//        data.append("file", file);
//        $.ajax({
//            data: data,
//            type: "POST",
//            // Call operation: Controller: SummernoteUploadImage in HomeController
//            url: '@Url.Action("UploadImage", "Article")',
//            cache: false,
//            contentType: false,
//            processData: false,
//            success: function (response) {
//                // After uploading to the server, insert the image into the correct position in the editor.
//                $('#summernote').summernote('insertImage', response.Url, function ($image) {
//                    $image.css('width', $image.width());
//                });
//            }
//        });
//    }
//});



$('document').ready(function () {
    $('#inputGroupFileAddon04').click(function () {
        $('#formTopics').submit();
    }
}



//$('.read-more').click(function () {
//    var text = $(this).prev('.text');
//    text.toggleClass('summary');
//    if (text.hasClass('summary')) {
//        $(this).text('Daha Fazla Oku');
//    } else {
//        $(this).text('Daha Az Oku');
//    }
//});


//$('.service').click(function (e) {
//    e.preventDefault();
//});

//(function ($) {
//    $.fn.floatLabels = function (options) {

//        // Settings
//        var self = this;
//        var settings = $.extend({}, options);


//        // Event Handlers
//        function registerEventHandlers() {
//            self.on('input keyup change', 'input, textarea', function () {
//                actions.swapLabels(this);
//            });
//        }


//        // Actions
//        var actions = {
//            initialize: function () {
//                self.each(function () {
//                    var $this = $(this);
//                    var $label = $this.children('label');
//                    var $field = $this.find('input,textarea').first();

//                    if ($this.children().first().is('label')) {
//                        $this.children().first().remove();
//                        $this.append($label);
//                    }

//                    var placeholderText = ($field.attr('placeholder') && $field.attr('placeholder') != $label.text()) ? $field.attr('placeholder') : $label.text();

//                    $label.data('placeholder-text', placeholderText);
//                    $label.data('original-text', $label.text());

//                    if ($field.val() == '') {
//                        $field.addClass('empty')
//                    }
//                });
//            },
//            swapLabels: function (field) {
//                var $field = $(field);
//                var $label = $(field).siblings('label').first();
//                var isEmpty = Boolean($field.val());

//                if (isEmpty) {
//                    $field.removeClass('empty');
//                    $label.text($label.data('original-text'));
//                }
//                else {
//                    $field.addClass('empty');
//                    $label.text($label.data('placeholder-text'));
//                }
//            }
//        }


//        // Initialization
//        function init() {
//            registerEventHandlers();

//            actions.initialize();
//            self.each(function () {
//                actions.swapLabels($(this).find('input,textarea').first());
//            });
//        }
//        init();


//        return this;
//    };

//    $(function () {
//        $('.float-label-control').floatLabels();
//    });
//})(jQuery);



/*//$(document).ready(function () */
//    $('.summernote').summernote();
//});

//$('#summernote').summernote({
//    height: 300,                 // set editor height
//    minHeight: null,             // set minimum height of editor
//    maxHeight: null,             // set maximum height of editor
//    focus: true                  // set focus to editable area after initializing summernote
//});
//$('#summernote').summernote({
//    placeholder: 'Hello stand alone ui',
//    tabsize: 2,
//    height: 120,
//    toolbar: [
//        ['style', ['style']],
//        ['font', ['bold', 'underline', 'clear']],
//        ['color', ['color']],
//        ['para', ['ul', 'ol', 'paragraph']],
//        ['table', ['table']],
//        ['insert', ['link', 'picture', 'video']],
//        ['view', ['fullscreen', 'codeview', 'help']]
//    ]
//});


(function ($) {
    "use strict"; // Start of use strict

    // Floating label headings for the contact form
    $("body").on("input propertychange", ".floating-label-form-group", function (e) {
        $(this).toggleClass("floating-label-form-group-with-value", !!$(e.target).val());
    }).on("focus", ".floating-label-form-group", function () {
        $(this).addClass("floating-label-form-group-with-focus");
    }).on("blur", ".floating-label-form-group", function () {
        $(this).removeClass("floating-label-form-group-with-focus");
    });

    // Show the navbar when the page is scrolled up
    var MQL = 992;

    //primary navigation slide-in effect
    if ($(window).width() > MQL) {
        var headerHeight = $('#mainNav').height();
        $(window).on('scroll', {
            previousTop: 0
        },
            function () {
                var currentTop = $(window).scrollTop();
                //check if user is scrolling up
                if (currentTop < this.previousTop) {
                    //if scrolling up...
                    if (currentTop > 0 && $('#mainNav').hasClass('is-fixed')) {
                        $('#mainNav').addClass('is-visible');
                    } else {
                        $('#mainNav').removeClass('is-visible is-fixed');
                    }
                } else if (currentTop > this.previousTop) {
                    //if scrolling down...
                    $('#mainNav').removeClass('is-visible');
                    if (currentTop > headerHeight && !$('#mainNav').hasClass('is-fixed')) $('#mainNav').addClass('is-fixed');
                }
                this.previousTop = currentTop;
            });
    }

})(jQuery); // End of use strict