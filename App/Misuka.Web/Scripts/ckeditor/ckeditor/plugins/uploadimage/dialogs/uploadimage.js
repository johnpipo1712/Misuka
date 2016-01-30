
CKEDITOR.dialog.add('uploadimageDialog', function (editor) {
    return {
        title: 'Upload Image Basic',
        minWidth: 600,
        minHeight: 500,
        contents: [
            {
                id: 'tab-basic',
                label: 'Basic Image',
                elements: [
                    {
                        type: 'text',
                        id: 'urlbasic',
                        label: 'URL',
                        validate: CKEDITOR.dialog.validate.notEmpty("URL field cannot be empty.")
                    },
                    {
                        type: 'text',
                        id: 'altbasic',
                        label: 'Alt',
                    },
                    {
                        type: 'text',
                        id: 'widthtbasic',
                        label: 'Width',
                        validate: CKEDITOR.dialog.validate.integer("Width field can be number")
                    },
                    {
                        type: 'text',
                        id: 'heightbasic',
                        label: 'Height',
                        validate: CKEDITOR.dialog.validate.integer("Height field can be number")
                    }
                ]
            }
            
        ],
        onOk: function () {
            var dialog = this;
            var urlbasic = '';
            var altbasic = '';
            var height = '';
            var width = '';
            urlbasic = dialog.getValueOf('tab-basic', 'urlbasic');
            altbasic = dialog.getValueOf('tab-basic', 'altbasic');
            width = dialog.getValueOf('tab-basic', 'widthtbasic');
            height = dialog.getValueOf('tab-basic', 'heightbasic');
             var img = '<img'
            img += ' src= "' + urlbasic + '"'
            if (altbasic != '') {
                img += ' alt= "' + altbasic + '"'
            }
            if (width != '') {
                img += ' width= "' + width + 'px"'
            }
            if (height != '') {
                img += ' height= "' + height + 'px"'
            }
            img += '/>'
          
            editor.insertHtml(img);
        }
    };
});