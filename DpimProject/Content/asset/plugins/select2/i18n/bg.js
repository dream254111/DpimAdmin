﻿/*! Select2 4.0.0 | https://github.com/select2/select2/blob/master/LICENSE.md */

(function(){if(jQuery&&jQuery.fn&&jQuery.fn.select2&&jQuery.fn.select2.amd)var e=jQuery.fn.select2.amd;return e.define("select2/i18n/bg",[],function(){return{inputTooLong:function(e){var t=e.input.length-e.maximum,n="ะะพะปั ะฒัะฒะตะดะตัะต ั "+t+" ะฟะพ-ะผะฐะปะบะพ ัะธะผะฒะพะป";return t>1&&(n+="a"),n},inputTooShort:function(e){var t=e.minimum-e.input.length,n="ะะพะปั ะฒัะฒะตะดะตัะต ะพัะต "+t+" ัะธะผะฒะพะป";return t>1&&(n+="a"),n},loadingMore:function(){return"ะ—ะฐั€ะตะถะดะฐั ัะต ะพัะตโ€ฆ"},maximumSelected:function(e){var t="ะะพะถะตัะต ะดะฐ ะฝะฐะฟั€ะฐะฒะธัะต ะดะพ "+e.maximum+" ";return e.maximum>1?t+="ะธะทะฑะพั€ะฐ":t+="ะธะทะฑะพั€",t},noResults:function(){return"ะัะผะฐ ะฝะฐะผะตั€ะตะฝะธ ััะฒะฟะฐะดะตะฝะธั"},searching:function(){return"ะขัั€ัะตะฝะตโ€ฆ"}}}),{define:e.define,require:e.require}})();