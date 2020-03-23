window.errorMessage = function (setting) {
	Swal.fire({
		icon: setting.icon,
		title: setting.title,
		text: setting.text,
		footer: setting.footerMessage
	});
};
window.confirmDialog = async function (settings) {

	const { value: url } = await Swal.fire({
		title: settings.title,
		text: settings.text,
		icon: settings.icon,
		showCancelButton: settings.showCancelButton,
		confirmButtonColor: settings.confirmButtonColor,
		cancelButtonColor: settings.cancelButtonColor,
		confirmButtonText: settings.confirmButtonText
	});
	var outcome = url ? 'Yes' : 'No';
	return outcome;
};
window.successDialog = function (setting) {
	Swal.fire({
		position: "center",
		icon: setting.icon,
		title: setting.title,
		showConfirmButton: setting.showConfirmButton,
		timer: setting.timer
	});
};
window.inputDialog = async function (setting) {
	const { value: input } = await Swal.fire({
		input: setting.inputType,
		inputPlaceholder: setting.placeholderText,
		showCancelButton: setting.showCancelButton
	});

	if (input) {
		return input;
	} else {
		return '';
	}
};
window.riskWeight = async function () {
	const { value: inputValue } = await Swal.fire({
		title: 'Risk Percentage',
		icon: 'question',
		input: 'range',
		inputAttributes: {
			min: 0,
			max: 100,
			step: 1
		},
		inputValue: 0
	});
	if (inputValue) {
		return inputValue;
	}
	else {
		return 0;
	}

};
