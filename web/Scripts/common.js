﻿$(document).ready(function() {
    setInterval(function() {
        var ticks = $('.twitter li:first-child').attr('class');
        $.ajax({ url: "/ajax/tweets/" + ticks, dataType: 'json', success: function(data) {
            var lastChild = $('.twitter li:last-child')
            $.each(data, function(i, item) {
                $(lastChild).slideUp('slow', function() {
                    $('.additional.twitter ul').prepend('<li class="'+item.Ticks+'>' + item.Description +'</li>');
                    $('.twitter li:first-child').hide().slideDown('slow');
                    $(this).remove();
                })
                lastChild = $(lastChild).prev();
            });

        }
        });
    }, 10000);
    if ($('.field-validation-error').length == 0) {
        $('.comment-form').hide();
    }
    $('.comment-form').parent().before('<a href="" class="comment">Sign Guestbook</a>');
    $('a.comment').click(function() {
        $('.comment-form').slideToggle();
        $('a.comment').toggle(function() {
            $(this).html("Sign Guestbook");
        }, function() {
            $(this).html("Hide");
        });
        return false;
    });
    $('.paging a').live('click', function() {
        var anchor = $(this);
        $.get(anchor.attr('href'), function(data) {
            $('ul.entries').fadeOut(function() {
                $(this).html(data).fadeIn();
            });

            anchor.parent().addClass('selected');
            anchor.parent().siblings('.selected').removeClass('selected');
        });
        return false;
    })

    $('#footer li a').fancybox({
		'transitionIn'	:	'elastic',
		'transitionOut'	:	'elastic',
		'speedIn'		:	600, 
		'speedOut'		:	200, 
		'overlayShow'	:	false
	});

});