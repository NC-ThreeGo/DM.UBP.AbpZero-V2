webpackJsonp([10],{556:function(n,t){},563:function(n,t,i){"use strict";function e(){return p.a.get("font-names").map(function(n){return'<option value="'+n+'">'+n+"</option>"}).join("")}function o(){return["None","Italic","Bold","Bold Italic"].map(function(n){return'<option value="'+n+'">'+n+"</option>"}).join("")}function l(){return p.a.get("font-sizes").map(function(n){return'<option value="'+n+'pt">'+n+"</option>"}).join("")}function a(){return[{val:"Strikeout",label:v.a.tr("Toolbar Text Strikeout")},{val:"Underline",label:v.a.tr("Toolbar Text Underline")}].map(function(n){return'<option value="'+n.val+'">'+n.label+"</option>"}).join("")}function s(){var n=arguments.length>0&&void 0!==arguments[0]?arguments[0]:{};return Object(f.a)().then(function(t){var i=new u.a("Toolbar Text Name"),e=c()(b()),o=e.find(".js-font-presentation"),l=e.find(".js-font-list"),a=e.find(".js-font-outline"),s=e.find(".js-font-size"),r=e.find(".js-font-modification"),d=function(n){o.css({"font-family":n.name,"font-size":n.size}),n.isUnderline&&n.isStrikeout?o.css("text-decoration","underline line-through"):n.isUnderline?o.css("text-decoration","underline"):n.isStrikeout?o.css("text-decoration","line-through"):o.css("text-decoration",""),n.isBold?o.css("font-weight","600"):o.css("font-weight",""),n.isItalic?o.css("font-style","italic"):o.css("font-style","")},f=function(n){l.val(n.name),s.val(n.size);var t=[];n.isUnderline&&t.push("Underline"),n.isStrikeout&&t.push("Strikeout"),r.val(t),n.isBold&&n.isItalic?a.val("Bold Italic"):n.isBold?a.val("Bold"):n.isItalic?a.val("Italic"):a.val("None"),d(n)};return f=f.bind(null,n),f(),l.on("change",function(t){n.name=c()(t.target).val(),f()}),a.on("change",function(t){var i=c()(t.target).val();"Bold Italic"===i?(n.isBold=!0,n.isItalic=!0):"Bold"===i?(n.isBold=!0,n.isItalic=!1):"Italic"===i?(n.isBold=!1,n.isItalic=!0):(n.isBold=!1,n.isItalic=!1),f()}),s.on("change",function(t){n.size=c()(t.target).val(),f()}),r.on("change",function(t){var i=c()(t.target).val();Array.isArray(i)&&(i.includes("Underline")?n.isUnderline=!0:n.isUnderline=!1,i.includes("Strikeout")?n.isStrikeout=!0:n.isStrikeout=!1,f())}),e.on("click",".js-save-font",function(){return i.trigger("ok",n)}),i.find(".fr-modal-content").html(e),t.append(i),window.DSG.head.put(t),i})}var r,d,c,f,u,v,p,b;Object.defineProperty(t,"__esModule",{value:!0}),r=i(556),d=i(0),c=i.n(d),f=i(259),u=i(258),v=i(7),p=i(4),b=function(){return'\n        <div class="fr-edit-font-dialog">\n            <div class="fr-modal-body fr-edit-font-dialog__body">\n                <div class="fr-edit-font-dialog__body-row">\n                    <div class="fr-edit-font-dialog__body-item fr-edit-font-dialog__font">\n                        <label>\n                            <span>'+v.a.tr("Toolbar Text Name")+'</span>\n                            <select class="js-font-list">\n                                '+e()+'\n                            </select>\n                        </label>\n                    </div>\n                    <div class="fr-edit-font-dialog__body-item fr-edit-font-dialog__outline">\n                        <label>\n                            <span>'+v.a.tr("General TextStyle")+'</span>\n                            <select class="js-font-outline">\n                                '+o()+'\n                            </select>\n                        </label>\n                    </div>\n                    <div class="fr-edit-font-dialog__body-item fr-edit-font-dialog__size">\n                        <label>\n                            <span>'+v.a.tr("Toolbar Text Size")+'</span>\n                            <select class="js-font-size">\n                                '+l()+'\n                            </select>\n                        </label>\n                    </div>\n                </div>\n                <div class="fr-edit-font-dialog__body-row">\n                    <div class="fr-edit-font-dialog__body-item fr-edit-font-dialog__modification">\n                        <select class="js-font-modification" multiple>\n                            '+a()+'\n                        </select>\n                    </div>\n                    <div class="fr-edit-font-dialog__body-item fr-edit-font-dialog__sample js-font-presentation">\n                        '+v.a.tr("Misc Sample")+'\n                    </div>\n                </div>\n            </div>\n\n            <div class="fr-modal-footer content-right">\n                <button type="button" class="fr-btn fr-btn-primary fr-save js-save-font">\n                    '+v.a.tr("Buttons Ok")+"\n                </button>\n            </div>\n        </div>\n    "},t.create=s}});