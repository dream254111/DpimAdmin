﻿/*! Select2 4.0.0 | https://github.com/select2/select2/blob/master/LICENSE.md */

(function(){if(jQuery&&jQuery.fn&&jQuery.fn.select2&&jQuery.fn.select2.amd)var e=jQuery.fn.select2.amd;return e.define("select2/i18n/mk",[],function(){return{inputTooLong:function(e){var t=e.input.length-e.maximum,n="ะ’ะต ะผะพะปะธะผะต ะฒะฝะตัะตัะต "+e.maximum+" ะฟะพะผะฐะปะบั ะบะฐั€ะฐะบัะตั€";return e.maximum!==1&&(n+="ะธ"),n},inputTooShort:function(e){var t=e.minimum-e.input.length,n="ะ’ะต ะผะพะปะธะผะต ะฒะฝะตัะตัะต ัััะต "+e.maximum+" ะบะฐั€ะฐะบัะตั€";return e.maximum!==1&&(n+="ะธ"),n},loadingMore:function(){return"ะ’ัะธััะฒะฐัะต ั€ะตะทัะปัะฐัะธโ€ฆ"},maximumSelected:function(e){var t="ะะพะถะตัะต ะดะฐ ะธะทะฑะตั€ะตัะต ัะฐะผะพ "+e.maximum+" ััะฐะฒะบ";return e.maximum===1?t+="ะฐ":t+="ะธ",t},noResults:function(){return"ะะตะผะฐ ะฟั€ะพะฝะฐัะดะตะฝะพ ัะพะฒะฟะฐั“ะฐัะฐ"},searching:function(){return"ะั€ะตะฑะฐั€ัะฒะฐัะตโ€ฆ"}}}),{define:e.define,require:e.require}})();
