﻿/*! Select2 4.0.0 | https://github.com/select2/select2/blob/master/LICENSE.md */

(function(){if(jQuery&&jQuery.fn&&jQuery.fn.select2&&jQuery.fn.select2.amd)var e=jQuery.fn.select2.amd;return e.define("select2/i18n/is",[],function(){return{inputTooLong:function(e){var t=e.input.length-e.maximum,n="Vinsamlegast styttiรฐ texta um "+t+" staf";return t<=1?n:n+"i"},inputTooShort:function(e){var t=e.minimum-e.input.length,n="Vinsamlegast skrifiรฐ "+t+" staf";return t>1&&(n+="i"),n+=" รญ viรฐbรณt",n},loadingMore:function(){return"Sรฆki fleiri niรฐurstรถรฐurโ€ฆ"},maximumSelected:function(e){return"รรบ getur aรฐeins valiรฐ "+e.maximum+" atriรฐi"},noResults:function(){return"Ekkert fannst"},searching:function(){return"Leitaโ€ฆ"}}}),{define:e.define,require:e.require}})();