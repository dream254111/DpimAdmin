﻿/*! Select2 4.0.0 | https://github.com/select2/select2/blob/master/LICENSE.md */

(function(){if(jQuery&&jQuery.fn&&jQuery.fn.select2&&jQuery.fn.select2.amd)var e=jQuery.fn.select2.amd;return e.define("select2/i18n/cs",[],function(){function e(e,t){switch(e){case 2:return t?"dva":"dvฤ";case 3:return"tลi";case 4:return"ฤtyลi"}return""}return{errorLoading:function(){return"Vรฝsledky nemohly bรฝt naฤteny."},inputTooLong:function(t){var n=t.input.length-t.maximum;return n==1?"Prosรญm zadejte o jeden znak mรฉnฤ":n<=4?"Prosรญm zadejte o "+e(n,!0)+" znaky mรฉnฤ":"Prosรญm zadejte o "+n+" znakลฏ mรฉnฤ"},inputTooShort:function(t){var n=t.minimum-t.input.length;return n==1?"Prosรญm zadejte jeลกtฤ jeden znak":n<=4?"Prosรญm zadejte jeลกtฤ dalลกรญ "+e(n,!0)+" znaky":"Prosรญm zadejte jeลกtฤ dalลกรญch "+n+" znakลฏ"},loadingMore:function(){return"Naฤรญtajรญ se dalลกรญ vรฝsledkyโ€ฆ"},maximumSelected:function(t){var n=t.maximum;return n==1?"Mลฏลพete zvolit jen jednu poloลพku":n<=4?"Mลฏลพete zvolit maximรกlnฤ "+e(n,!1)+" poloลพky":"Mลฏลพete zvolit maximรกlnฤ "+n+" poloลพek"},noResults:function(){return"Nenalezeny ลพรกdnรฉ poloลพky"},searching:function(){return"Vyhledรกvรกnรญโ€ฆ"}}}),{define:e.define,require:e.require}})();
