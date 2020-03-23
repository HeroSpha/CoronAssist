(function ($) {
    'use strict';
	window.errorMessage = function (setting) {
		Swal.fire({
			icon: setting.icon,
			title: setting.title,
			text: setting.text,
			footer: setting.footerMessage
		});
	};
});