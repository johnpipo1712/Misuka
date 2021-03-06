

CKEDITOR.plugins.add('uploadimageadv', {
    icons: 'uploadimageadv',
    init: function (editor) {
        var pluginDirectory = this.path;
        //editor.addContentsCss(pluginDirectory + '/Content/kendo/2014.1.318/kendo.common.min.css');
        //editor.addContentsCss(pluginDirectory + '/Content/kendo/2014.1.318/kendo.common-bootstrap.min.css');
        //editor.addContentsCss(pluginDirectory +'/Content/kendo/2014.1.318/kendo.bootstrap.min.css');
        editor.addCommand('uploadimageadv', new CKEDITOR.dialogCommand('uploadimageadvDialog'));
        editor.ui.addButton('uploadimageadv', {
            label: 'Upload Image Advanced',
            command: 'uploadimageadv',
            toolbar: 'insert'
        });

        CKEDITOR.dialog.add('uploadimageadvDialog', this.path + 'dialogs/uploadimageadv.js');
    }
});