
//var html ='  <div class="demo-section">'+
//     '   <div id="listView"></div>'+
//       ' <div id="pager" class="k-pager-wrap"></div>'+
//   ' </div>'+
//'<script type="text/x-kendo-template" id="template">'+
//       ' <div class="product">'+
//        '    <img src="../content/web/foods/#= ProductID #.jpg" alt="#: ProductName # image" />'+
//      '      <h3>#:ProductName#</h3>'+
//    '        <p>#:kendo.toString(UnitPrice, "c")#</p>'+
//    '    </div>'+
  //'  </script>'
var html = '<div id="dataimg" style="width:100%;height:200px;overflow:scroll;></div> \n';
      
  
CKEDITOR.dialog.add('uploadimageadvDialog', function (editor) {
    return {
        title: 'Upload Image Advanced',
        minWidth: 600,
        minHeight: 500,
        contents: [
            {
                id: 'tab-adv',
                label: 'Advanced Image',
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
            //CKEDITOR.scriptLoader.load(['/Scripts/jquery-2.1.3.js', '/Scripts/kendo/kendo.all.min.js','/Scripts/jquery-ui-1.11.4.js'], function (completed, failed) {

         
            
            //});
            //$(function () {
            //    var dataSource = new kendo.data.DataSource({
            //        transport: {
            //            read: {
            //                url: "http://demos.telerik.com/kendo-ui/service/Products",
            //                dataType: "jsonp"
            //            }
            //        },
            //        pageSize: 15
            //    });

            //    $("#pager").kendoPager({
            //        dataSource: dataSource
            //    });

            //    $("#listView").kendoListView({
            //        dataSource: dataSource,
            //        template: kendo.template($("#template").html())

            //    });
            //});
            var dialog = this;
            
            $.get(window.location.origin + '/Ckeditor/DataImage', function (data) {
                $("#dataimg").empty();
               for (var i = 0; i < data.length; i++) {
                   $("#dataimg").append('<div style="float:left;width:132px;height:120px; margin-right:5px"> <img style=" width:100%; height:120px; cursor: pointer;" src="' + data[i].URL + '" alt = "' + data[i].Alt + '" /> </div>')

                }
               $("img").click(function () { dialog.getContentElement('tab-adv', 'urladv').setValue(this.src.replace(window.location.origin,'')); });

            });
        },
        onOk: function () {
            var dialog = this;
            var urladv = '';
            var altadv = '';
            var height = '';
            var width = '';
            urladv = dialog.getValueOf('tab-adv', 'urladv');
            altadv = dialog.getValueOf('tab-adv', 'altadv');
            width = dialog.getValueOf('tab-adv', 'widthtadv');
            height = dialog.getValueOf('tab-adv', 'heightadv');
            var img = '<img'
            img += ' src= "' + urladv + '"';
            if (altadv != '') {
                img += ' alt= "' + altadv + '"';
            }
            if (width != '') {
                img += ' width= "' + width + 'px"';
            }
            if (height != '') {
                img += ' height= "' + height + 'px"';
            }
            img += '/>';

            editor.insertHtml(img);
        }
        
    };
});