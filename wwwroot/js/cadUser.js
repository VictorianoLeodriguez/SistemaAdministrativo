$(document).ready(function () {
    $('#CPFJ').on('input', function () {
        let val = $(this).val().replace(/\D/g, '');

        if (val.length <= 11) {
            val = val
                .replace(/(\d{3})(\d)/, '$1.$2')
                .replace(/(\d{3})(\d)/, '$1.$2')
                .replace(/(\d{3})(\d{1,2})$/, '$1-$2');
        } else {
            val = val
                .replace(/(\d{2})(\d)/, '$1.$2')
                .replace(/(\d{3})(\d)/, '$1.$2')
                .replace(/(\d{3})(\d)/, '$1/$2')
                .replace(/(\d{4})(\d{1,2})$/, '$1-$2');
        }

        $(this).val(val);
    });
});

$(document).ready(function () {
    $('#LogoFile').on('change', function () {
        const file = this.files[0];

        if (file) {

            const reader = new FileReader();
            reader.onload = function (e) {

                $('#LogoPreview')
                    .attr('src', e.target.result)
                    .removeClass('d-none');

                $('#LogoIcon')
                    .addClass('d-none');
            };

            reader.readAsDataURL(file);
        }

    });
});