
var html = '<div id="dataiframe" style="width:100%;height:200px;overflow:scroll;></div> \n'
      
              

CKEDITOR.dialog.add('uploadvideoDialog', function (editor) {
    return {
        title: 'Upload Video',
        minWidth: 600,
        minHeight: 500,
        contents: [
            {
                id: 'tab-adv',
                label: 'Video',
                elements: [
                    {
                        type: 'html',  
                        html: html
                    },
                    {
                          type: 'text',
                          id: 'urladv',
                          label: 'URL',
                          validate: CKEDITOR.dialog.validate.notEmpty("URL field cannot be empty.")
                    },
                    {
                        type: 'text',
                        id: 'altadv',
                        label: 'Alt',
                    },
                    {
                        type: 'text',
                        id: 'widthtadv',
                        label: 'Width',
                        validate: CKEDITOR.dialog.validate.integer("Width field can be number")
                    },
                    {
                        type: 'text',
                        id: 'heightadv',
                        label: 'Height',
                        validate: CKEDITOR.dialog.validate.integer("Height field can be number")
                    }
                ]
                
            }
        ],
        onLoad: function () {
            var dialog = this;
            $.get(window.location.origin + '/Ckeditor/DataVideo', function (data) {
                $("#dataiframe").empty();
                for (var i = 0; i < data.length; i++) {
                    $("#dataiframe").append('<div style="float:left;width:132px;height:120px; margin-right:5px"> <video style="width:100%; height:120px; cursor: pointer;" src="' + data[i].URL + '"/></div>')
                    }
                $("video").click(function() { dialog.getContentElement('tab-adv', 'urladv').setValue(this.src.replace(window.location.origin, '')); });


            });
        },
        onOk: function () {
            var dialog = this;
            var urladv = '';
            urladv = dialog.getValueOf('tab-adv', 'urladv');
         
            var video = new CKEDITOR.dom.element('video', editor.document);
            video.setAttributes(
				{
				    controls: 'controls'
				});
            var source = new CKEDITOR.dom.element('source', editor.document);
            source.setAttributes(
                {
                    src: urladv,
                    type: 'video/mp4'
                } );
            editor.insertElement(video);
            video.append(source);
      
        }
        
    };
});