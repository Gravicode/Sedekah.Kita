function fw_open(url) {
    window.location = url;
}

function fw_initInputs(){
    $('.form-group>input[type="password"]+button.toggle').on('click',function(){
        var inp = $(this).prev('input');
        if (inp.attr('type')=='password') inp.attr('type','text');
        else inp.attr('type','password')
    });
}

function fw_dialogClose(id){
    if (!id) $('body>.fwdialog').removeClass('show');
    else $('body>#'+id+'.fwdialog').removeClass('show');
    if ($('body>.fwdialog.show').length==0) $('body').removeClass('withDialog');
}

function fw_dialog(id,content,buttons,styles){
    // content : html content
    // buttons : [{class:'primary',label:'OK',action:'fw_dialogClose(\'id\')'}]
    // styles  : inline css
    if (!styles) styles = '';
    var dlg = $('body>#'+id+'.fwdialog');
    if (dlg.length==0) {
        dlg = $('<div id="'+id+'" class="fwdialog" ><div class="fwdialog-outer"><div class="fwdialog-inner"><div class="fwdialog-content"></div><div class="fwdialog-buttons"></div></div></div></div>').appendTo('body');
    }
    if (typeof content == 'object') content.appendTo($('.fwdialog-content',dlg));
    else $('.fwdialog-content',dlg).html(content);
    $('.fwdialog-buttons',dlg).empty();
    if (buttons!=null) {
        if (buttons.length>0) {
            for(var x in buttons) {
                var btn = $('<div class="form-button"><button class="'+(buttons[x].class || '')+'" onclick="'+buttons[x].action+'">'+(buttons[x].label || '')+'</button></div>');
                /*
                $('button',btn).on('click',function(){
                    let Fn = new Function (buttons[x].action);
                    return Fn();
                });
                */
                $('.fwdialog-buttons',dlg).append(btn);
                
            }
        } else {
            var btn = $('<div class="form-button"><button class="primary">OK</button></div>');
            btn.on('click',function(){fw_dialogClose();});
            $('.fwdialog-buttons',dlg).append(btn);
        }
    }
    $('.fwdialog-inner',dlg).attr('style',styles);
    dlg.addClass('show');
    if ($('body>.fwdialog.show').length>0) $('body').addClass('withDialog');
}

$(function(){
    fw_initInputs();
});