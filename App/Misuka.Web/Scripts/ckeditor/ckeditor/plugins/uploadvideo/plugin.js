

CKEDITOR.plugins.add('uploadvideo', {
    icons: 'uploadvideo',
    init: function (editor) {
        editor.addCommand('uploadvideo', new CKEDITOR.dialogCommand('uploadvideoDialog'));
        editor.ui.addButton('uploadvideo', {
            label: 'Upload Video',
            command: 'uploadvideo',
            toolbar: 'insert'
        });

        CKEDITOR.dialog.add('uploadvideoDialog', this.path + 'dialogs/uploadvideo.js');
    }
});