webpackJsonp([0],{558:function(n,t){},567:function(n,t,e){"use strict";function r(){var n=arguments.length>0&&void 0!==arguments[0]?arguments[0]:{},t=n.name,e=n.message;return Object(s.a)().then(function(n){var r=new l.a(t,{danger:!0}),a=i()(c());return r.find(".fr-modal-content").html(a),a.find(".alert-text").text(e),r.on("click",".js-alert-ok-btn",function(){return r.close()}),n.append(r),window.DSG.head.put(n),r})}var a,d,i,l,o,c,s;Object.defineProperty(t,"__esModule",{value:!0}),a=e(558),d=e(0),i=e.n(d),l=e(258),o=e(7),c=function(){return'\n        <div>\n            <div class="fr-modal-body fr-alert-dialog">\n                <table>\n                    <tr>\n                        <td><div class="alert-mark">!</div></td>\n                        <td><div class="alert-text"></div></td>\n                    </tr>\n                </table>\n            </div>\n\n            <div class="fr-modal-footer">\n                <div class="pull-right">\n                    <button type="button" class="fr-btn fr-btn-danger js-alert-ok-btn">\n                        '+o.a.tr("Ok")+"\n                    </button>\n                </div>\n            </div>\n        </div>\n    "},s=e(259),t.create=r}});