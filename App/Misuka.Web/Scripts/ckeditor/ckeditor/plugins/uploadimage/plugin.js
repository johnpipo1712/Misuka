CKEDITOR.plugins.add('uploadimage', {
    icons: 'uploadimage',
    init: function (editor) {
        editor.addCommand('uploadimage', new CKEDITOR.dialogCommand('uploadimageDialog'));
        editor.ui.addButton('uploadimage', {
            label: 'Upload Image Basic',
            command: 'uploadimage',
            toolbar: 'insert'
        });

        CKEDITOR.dialog.add('uploadimageDialog', this.path + 'dialogs/uploadimage.js');
    }
});