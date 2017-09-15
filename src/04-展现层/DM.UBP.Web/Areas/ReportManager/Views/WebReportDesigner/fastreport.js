function frxOpenPrint(url) {
  window.open(url, '_blank','resizable,scrollbars,width="400",height="300"'); 
};

function frRequestObject() {
  if (typeof XMLHttpRequest === 'undefined') {
    XMLHttpRequest = function() {
      try { return new ActiveXObject("Msxml2.XMLHTTP.6.0"); }
        catch(e) {}
      try { return new ActiveXObject("Msxml2.XMLHTTP.3.0"); }
        catch(e) {}
      try { return new ActiveXObject("Msxml2.XMLHTTP"); }
        catch(e) {}
      try { return new ActiveXObject("Microsoft.XMLHTTP"); }
        catch(e) {}
      throw new Error("This browser does not support XMLHttpRequest.");
    };
  }
  return new XMLHttpRequest();
}

function frRequestServer(url) {
    //abp.ajax({
    //    url: encodeURI(url),
    //    type: "post"
    //}).done(function (data, textStatus, request) {
    //    onFrAjaxSuccess(data, textStatus, request);
    //});
    $.ajax({
        url: encodeURI(url),
        type: "post",
        success: onFrAjaxSuccess
    });
    //$.get(
    //    encodeURI(url),        
    //    {
    //        "_": $.now()
    //    },
    //    onFrAjaxSuccess
    //    );
}

function onFrAjaxSuccess(data, textStatus, request) {
    console.info("FastReport data length " + data.length);
    obj = request.getResponseHeader("FastReport-container");
    div = document.getElementById(obj);
    div = frReplaceInnerHTML(div, data);
    var scripts = div.getElementsByTagName('script');
    for (var i = 0; i < scripts.length; i++)
        eval(scripts[i].text);
}

function frReplaceInnerHTML(repobj, html) {
    var obj = repobj;
    var newObj = document.createElement(obj.nodeName);
    newObj.id = obj.id;
    newObj.className = obj.className;
    newObj.innerHTML = html;
    if (obj.parentNode)
        obj.parentNode.replaceChild(newObj, obj);
    else
        obj.innerHTML = html;
    return newObj;
}

function frProcessReqChange() {
  try 
  { 
    if (req.readyState == 4) 
    {
        if (req.status == 200) 
        {
            obj = req.getResponseHeader("FastReport-container");            
            div = document.getElementById(obj);
            div = frReplaceInnerHTML(div, req.responseText);	        
            var scripts = div.getElementsByTagName('script');
            for (var i = 0; i < scripts.length; i++) 
                eval(scripts[i].text);
        } 
        else 
        {
            throw new Error("Error: " + req.statusText);
        }
    }
  }
  catch( e ) {
      throw new Error("Error: " + e);      
  }
}
