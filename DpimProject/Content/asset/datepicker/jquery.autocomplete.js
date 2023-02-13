/**
 * @preserve jQuery Autocomplete plugin v1.2.6
 * @homepage http://xdsoft.net/jqplugins/autocomplete/
 * @license MIT - MIT-LICENSE.txt
 * (c) 2014, Chupurnov Valeriy <chupurnov@gmail.com>
 */
(function ($) {
	'use strict';
	var	ARROWLEFT = 37,
		ARROWRIGHT = 39,
		ARROWUP = 38,
		ARROWDOWN = 40,
		TAB = 9,
		CTRLKEY = 17,
		SHIFTKEY = 16,
		DEL = 46,
		ENTER = 13,
		ESC = 27,
		BACKSPACE = 8,
		AKEY = 65,
		CKEY = 67,
		VKEY = 86,
		ZKEY = 90,
		YKEY = 89,
		defaultSetting = {},
		//currentInput = false,
		ctrlDown = false,
		shiftDown = false,
		interval_for_visibility,
		publics = {},
		accent_map = {
			'แบ':'a','ร':'a','รก':'a','ร€':'a','ร ':'a','ฤ':'a','ฤ':'a','แบฎ':'a','แบฏ':'a','แบฐ':'a','แบฑ':'a','แบด':'a','แบต':'a','แบฒ':'a',
			'แบช':'a','แบซ':'a','แบจ':'a','แบฉ':'a','ว':'a','ว':'a','ร…':'a','รฅ':'a','วบ':'a','วป':'a','ร':'a','รค':'a','ว':'a','ว':'a',
			'ร':'a','รฃ':'a','ศฆ':'a','ศง':'a','ว ':'a','วก':'a','ฤ':'a','ฤ…':'a','ฤ€':'a','ฤ':'a','แบข':'a','แบฃ':'a','ศ€':'a','ศ':'a',
			'ศ':'a','ศ':'a','แบ ':'a','แบก':'a','แบถ':'a','แบท':'a','แบฌ':'a','แบญ':'a','แธ€':'a','แธ':'a','ศบ':'a','โฑฅ':'a','วผ':'a','วฝ':'a',
			'วข':'a','วฃ':'a','แธ':'b','แธ':'b','แธ':'b','แธ…':'b','แธ':'b','แธ':'b','ษ':'b','ฦ€':'b','แตฌ':'b','ฦ':'b','ษ“':'b','ฦ':'b',
			'ฦ':'b','ฤ':'c','ฤ':'c','ฤ':'c','ฤ':'c','ฤ':'c','ฤ':'c','ฤ':'c','ฤ':'c','ร':'c','รง':'c','แธ':'c','แธ':'c','ศป':'c',
			'ศผ':'c','ฦ':'c','ฦ':'c','ษ•':'c','ฤ':'d','ฤ':'d','แธ':'d','แธ':'d','แธ':'d','แธ‘':'d','แธ':'d','แธ':'d','แธ’':'d','แธ“':'d',
			'แธ':'d','แธ':'d','ฤ':'d','ฤ‘':'d','แตญ':'d','ฦ':'d','ษ–':'d','ฦ':'d','ษ—':'d','ฦ':'d','ฦ':'d','ศก':'d','รฐ':'d','ร':'e',
			'ฦ':'e','ฦ':'e','ว':'e','รฉ':'e','ร':'e','รจ':'e','ฤ”':'e','ฤ•':'e','ร':'e','รช':'e','แบพ':'e','แบฟ':'e','แป€':'e','แป':'e',
			'แป':'e','แป…':'e','แป':'e','แป':'e','ฤ':'e','ฤ':'e','ร':'e','รซ':'e','แบผ':'e','แบฝ':'e','ฤ–':'e','ฤ—':'e','ศจ':'e','ศฉ':'e',
			'แธ':'e','แธ':'e','ฤ':'e','ฤ':'e','ฤ’':'e','ฤ“':'e','แธ–':'e','แธ—':'e','แธ”':'e','แธ•':'e','แบบ':'e','แบป':'e','ศ':'e','ศ…':'e',
			'ศ':'e','ศ':'e','แบธ':'e','แบน':'e','แป':'e','แป':'e','แธ':'e','แธ':'e','แธ':'e','แธ':'e','ษ':'e','ษ':'e','ษ':'e','ษ':'e',
			'แธ':'f','แธ':'f','แตฎ':'f','ฦ‘':'f','ฦ’':'f','วด':'g','วต':'g','ฤ':'g','ฤ':'g','ฤ':'g','ฤ':'g','วฆ':'g','วง':'g','ฤ ':'g',
			'ฤก':'g','ฤข':'g','ฤฃ':'g','แธ ':'g','แธก':'g','วค':'g','วฅ':'g','ฦ“':'g','ษ ':'g','ฤค':'h','ฤฅ':'h','ศ':'h','ศ':'h','แธฆ':'h',
			'แธง':'h','แธข':'h','แธฃ':'h','แธจ':'h','แธฉ':'h','แธค':'h','แธฅ':'h','แธช':'h','แธซ':'h','H':'h','ฬฑ':'h','แบ–':'h','ฤฆ':'h','ฤง':'h',
			'โฑง':'h','โฑจ':'h','ร':'i','รญ':'i','ร':'i','รฌ':'i','ฤฌ':'i','ฤญ':'i','ร':'i','รฎ':'i','ว':'i','ว':'i','ร':'i','รฏ':'i',
			'แธฎ':'i','แธฏ':'i','ฤจ':'i','ฤฉ':'i','ฤฐ':'i','i':'i','ฤฎ':'i','ฤฏ':'i','ฤช':'i','ฤซ':'i','แป':'i','แป':'i','ศ':'i','ศ':'i',
			'ศ':'i','ศ':'i','แป':'i','แป':'i','แธฌ':'i','แธญ':'i','I':'i','ฤฑ':'i','ฦ—':'i','ษจ':'i','ฤด':'j','ฤต':'j','J':'j','ฬ':'j',
			'วฐ':'j','ศท':'j','ษ':'j','ษ':'j','ส':'j','ษ':'j','ส':'j','แธฐ':'k','แธฑ':'k','วจ':'k','วฉ':'k','ฤถ':'k','ฤท':'k','แธฒ':'k',
			'แธณ':'k','แธด':'k','แธต':'k','ฦ':'k','ฦ':'k','โฑฉ':'k','โฑช':'k','ฤน':'a','ฤบ':'l','ฤฝ':'l','ฤพ':'l','ฤป':'l','ฤผ':'l','แธถ':'l',
			'แธท':'l','แธธ':'l','แธน':'l','แธผ':'l','แธฝ':'l','แธบ':'l','แธป':'l','ล':'l','ล':'l','ฬฃ':'l','ฤฟ':'l',
			'ล€':'l','ศฝ':'l','ฦ':'l','โฑ ':'l','โฑก':'l','โฑข':'l','ษซ':'l','ษฌ':'l','ษญ':'l','ศด':'l','แธพ':'m','แธฟ':'m','แน€':'m','แน':'m',
			'แน':'m','แน':'m','ษฑ':'m','ล':'n','ล':'n','วธ':'n','วน':'n','ล':'n','ล':'n','ร‘':'n','รฑ':'n','แน':'n','แน…':'n','ล…':'n',
			'ล':'n','แน':'n','แน':'n','แน':'n','แน':'n','แน':'n','แน':'n','ฦ':'n','ษฒ':'n','ศ ':'n','ฦ':'n','ษณ':'n','ศต':'n','N':'n',
			'ฬ':'n','n':'n','ร“':'o','รณ':'o','ร’':'o','รฒ':'o','ล':'o','ล':'o','ร”':'o','รด':'o','แป':'o','แป‘':'o','แป’':'o',
			'แป“':'o','แป–':'o','แป—':'o','แป”':'o','แป•':'o','ว‘':'o','ว’':'o','ร–':'o','รถ':'o','ศช':'o','ศซ':'o','ล':'o','ล‘':'o','ร•':'o',
			'รต':'o','แน':'o','แน':'o','แน':'o','แน':'o','ศฌ':'o','ศญ':'o','ศฎ':'o','ศฏ':'o','ศฐ':'o','ศฑ':'o','ร':'o','รธ':'o','วพ':'o',
			'วฟ':'o','วช':'o','วซ':'o','วฌ':'o','วญ':'o','ล':'o','ล':'o','แน’':'o','แน“':'o','แน':'o','แน‘':'o','แป':'o','แป':'o','ศ':'o',
			'ศ':'o','ศ':'o','ศ':'o','ฦ ':'o','ฦก':'o','แป':'o','แป':'o','แป':'o','แป':'o','แป ':'o','แปก':'o','แป':'o','แป':'o','แปข':'o',
			'แปฃ':'o','แป':'o','แป':'o','แป':'o','แป':'o','ฦ':'o','ษต':'o','แน”':'p','แน•':'p','แน–':'p','แน—':'p','โฑฃ':'p','ฦค':'p','ฦฅ':'p',
			'P':'p','ฬ':'p','p':'p','ส ':'q','ษ':'q','ษ':'q','ล”':'r','ล•':'r','ล':'r','ล':'r','แน':'r','แน':'r','ล–':'r',
			'ล—':'r','ศ':'r','ศ‘':'r','ศ’':'r','ศ“':'r','แน':'r','แน':'r','แน':'r','แน':'r','แน':'r','แน':'r','ษ':'r','ษ':'r','แตฒ':'r',
			'ษผ':'r','โฑค':'r','ษฝ':'r','ษพ':'r','แตณ':'r','ร':'s','ล':'s','ล':'s','แนค':'s','แนฅ':'s','ล':'s','ล':'s','ล ':'s','ลก':'s',
			'แนฆ':'s','แนง':'s','แน ':'s','แนก':'s','แบ':'s','ล':'s','ล':'s','แนข':'s','แนฃ':'s','แนจ':'s','แนฉ':'s','ศ':'s','ศ':'s','ส':'s',
			'S':'s','ฬฉ':'s','s':'s','ร':'t','รพ':'t','ลค':'t','ลฅ':'t','T':'t','แบ—':'t','แนช':'t','แนซ':'t','ลข':'t','ลฃ':'t','แนฌ':'t',
			'แนญ':'t','ศ':'t','ศ':'t','แนฐ':'t','แนฑ':'t','แนฎ':'t','แนฏ':'t','ลฆ':'t','ลง':'t','ศพ':'t','โฑฆ':'t','แตต':'t',
			'ฦซ':'t','ฦฌ':'t','ฦญ':'t','ฦฎ':'t','ส':'t','ศถ':'t','ร':'u','รบ':'u','ร':'u','รน':'u','ลฌ':'u','ลญ':'u','ร':'u','รป':'u',
			'ว“':'u','ว”':'u','ลฎ':'u','ลฏ':'u','ร':'u','รผ':'u','ว—':'u','ว':'u','ว':'u','ว':'u','ว':'u','ว':'u','ว•':'u','ว–':'u',
			'ลฐ':'u','ลฑ':'u','ลจ':'u','ลฉ':'u','แนธ':'u','แนน':'u','ลฒ':'u','ลณ':'u','ลช':'u','ลซ':'u','แนบ':'u','แนป':'u','แปฆ':'u','แปง':'u',
			'ศ”':'u','ศ•':'u','ศ–':'u','ศ—':'u','ฦฏ':'u','ฦฐ':'u','แปจ':'u','แปฉ':'u','แปช':'u','แปซ':'u','แปฎ':'u','แปฏ':'u','แปฌ':'u','แปญ':'u',
			'แปฐ':'u','แปฑ':'u','แปค':'u','แปฅ':'u','แนฒ':'u','แนณ':'u','แนถ':'u','แนท':'u','แนด':'u','แนต':'u','ษ':'u','ส':'u','แนผ':'v','แนฝ':'v',
			'แนพ':'v','แนฟ':'v','ฦฒ':'v','ส':'v','แบ':'w','แบ':'w','แบ€':'w','แบ':'w','ลด':'w','ลต':'w','W':'w','ฬ':'w','แบ':'w','แบ':'w',
			'แบ…':'w','แบ':'w','แบ':'w','แบ':'w','แบ':'w','แบ':'x','แบ':'x','แบ':'x','แบ':'x','ร':'y','รฝ':'y','แปฒ':'y','แปณ':'y','ลถ':'y',
			'ลท':'y','Y':'y','แบ':'y','ลธ':'y','รฟ':'y','แปธ':'y','แปน':'y','แบ':'y','แบ':'y','ศฒ':'y','ศณ':'y','แปถ':'y','แปท':'y',
			'แปด':'y','แปต':'y','ส':'y','ษ':'y','ษ':'y','ฦณ':'y','ฦด':'y','ลน':'z','ลบ':'z','แบ':'z','แบ‘':'z','ลฝ':'z','ลพ':'z','ลป':'z',
			'ลผ':'z','แบ’':'z','แบ“':'z','แบ”':'z','แบ•':'z','ฦต':'z','ฦถ':'z','ศค':'z','ศฅ':'z','ส':'z','ส‘':'z','โฑซ':'z','โฑฌ':'z','วฎ':'z',
			'วฏ':'z','ฦบ':'z','๏ผ’':'2','๏ผ–':'6','๏ผข':'B','๏ผฆ':'F','๏ผช':'J','๏ผฎ':'N','๏ผฒ':'R','๏ผถ':'V','๏ผบ':'Z','๏ฝ':'b','๏ฝ':'f','๏ฝ':'j',
			'๏ฝ':'n','๏ฝ’':'r','๏ฝ–':'v','๏ฝ':'z','๏ผ‘':'1','๏ผ•':'5','๏ผ':'9','๏ผก':'A','๏ผฅ':'E','๏ผฉ':'I','๏ผญ':'M','๏ผฑ':'Q','๏ผต':'U','๏ผน':'Y',
			'๏ฝ':'a','๏ฝ…':'e','๏ฝ':'i','๏ฝ':'m','๏ฝ‘':'q','๏ฝ•':'u','๏ฝ':'y','๏ผ':'0','๏ผ”':'4','๏ผ':'8','๏ผค':'D','๏ผจ':'H','๏ผฌ':'L','๏ผฐ':'P',
			'๏ผด':'T','๏ผธ':'X','๏ฝ':'d','๏ฝ':'h','๏ฝ':'l','๏ฝ':'p','๏ฝ”':'t','๏ฝ':'x','๏ผ“':'3','๏ผ—':'7','๏ผฃ':'C','๏ผง':'G','๏ผซ':'K','๏ผฏ':'O',
			'๏ผณ':'S','๏ผท':'W','๏ฝ':'c','๏ฝ':'g','๏ฝ':'k','๏ฝ':'o','๏ฝ“':'s','๏ฝ—':'w','แบณ':'a','ร':'a','รข':'a','แบค':'a','แบฅ':'a','แบฆ':'a','แบง':'a'
		};

	if (window.getComputedStyle === undefined) {
		window.getComputedStyle = (function () {
			function getPixelSize(element, style, property, fontSize) {
				var	sizeWithSuffix = style[property],
					size = parseFloat(sizeWithSuffix),
					suffix = sizeWithSuffix.split(/\d/)[0],
					rootSize;

				fontSize = fontSize !== null ? fontSize : /%|em/.test(suffix) && element.parentElement ? getPixelSize(element.parentElement, element.parentElement.currentStyle, 'fontSize', null) : 16;
				rootSize = property === 'fontSize' ? fontSize : /width/i.test(property) ? element.clientWidth : element.clientHeight;

				return (suffix === 'em') ? size * fontSize : (suffix === 'in') ? size * 96 : (suffix === 'pt') ? size * 96 / 72 : (suffix === '%') ? size / 100 * rootSize : size;
			}

			function setShortStyleProperty(style, property) {
				var	borderSuffix = property === 'border' ? 'Width' : '',
					t = property + 'Top' + borderSuffix,
					r = property + 'Right' + borderSuffix,
					b = property + 'Bottom' + borderSuffix,
					l = property + 'Left' + borderSuffix;

				style[property] = (style[t] === style[r] === style[b] === style[l] ? [style[t]]
					: style[t] === style[b] && style[l] === style[r] ? [style[t], style[r]]
						: style[l] === style[r] ? [style[t], style[r], style[b]]
							: [style[t], style[r], style[b], style[l]]).join(' ');
			}

			function CSSStyleDeclaration(element) {
				var	currentStyle = element.currentStyle,
					style = this,
					property,
					fontSize = getPixelSize(element, currentStyle, 'fontSize', null);
				
				for (property in currentStyle) {
					if (Object.prototype.hasOwnProperty.call(currentStyle, property)) {
						if (/width|height|margin.|padding.|border.+W/.test(property) && style[property] !== 'auto') {
							style[property] = getPixelSize(element, currentStyle, property, fontSize) + 'px';
						} else if (property === 'styleFloat') {
							style.float = currentStyle[property];
						} else {
							style[property] = currentStyle[property];
						}
					}
				}

				setShortStyleProperty(style, 'margin');
				setShortStyleProperty(style, 'padding');
				setShortStyleProperty(style, 'border');

				style.fontSize = fontSize + 'px';

				return style;
			}

			CSSStyleDeclaration.prototype = {
				constructor: CSSStyleDeclaration,
				getPropertyPriority: function () {},
				getPropertyValue: function (prop) {
					return this[prop] || '';
				},
				item: function () {},
				removeProperty: function () {},
				setProperty: function () {},
				getPropertyCSSValue: function () {}
			};

			function getComputedStyle(element) {
				return new CSSStyleDeclaration(element);
			}

			return getComputedStyle;
		}(this));
	}


	$(document)
		.on('keydown.xdsoftctrl', function (e) {
			if (e.keyCode === CTRLKEY) {
				ctrlDown = true;
			}
			if (e.keyCode === SHIFTKEY) {
				ctrlDown = true;
			}
		})
		.on('keyup.xdsoftctrl', function (e) {
			if (e.keyCode === CTRLKEY) {
				ctrlDown = false;
			}
			if (e.keyCode === SHIFTKEY) {
				ctrlDown = false;
			}
		});
	
	function accentReplace (s) {
		if (!s) { return ''; }
		var ret = '',i;
		for (i=0; i < s.length; i+=1) {
			ret += accent_map[s.charAt(i)] || s.charAt(i);
		}
		return ret;
	}
	
	function escapeRegExp (str) {
		return str.replace(/[\-\[\]\/\{\}\(\)\*\+\?\.\\\^\$\|]/g, "\\$&");
	}
	
	function getCaretPosition(input) {
		if (!input) {
			return;
		}
		if (input.selectionStart) {
			return input.selectionStart;
		}
		if (document.selection) {
			input.focus();
			var sel = document.selection.createRange(),
				selLen = document.selection.createRange().text.length;
			sel.moveStart('character', -input.value.length);
			return sel.text.length - selLen;
		}
	}

	function setCaretPosition(input, pos) {
		if (input.setSelectionRange) {
			input.focus();
			input.setSelectionRange(pos, pos);
		} else if (input.createTextRange) {
			var range = input.createTextRange();
			range.collapse(true);
			range.moveEnd('character', pos);
			range.moveStart('character', pos);
			range.select();
		}
	}

	function isset(value) {
		return value !== undefined;
	}

	function safe_call(callback, args, callback2, defaultValue) {
		if (isset(callback) && !$.isArray(callback)) {
			return $.isFunction(callback) ? callback.apply(this,args):defaultValue;
		}
		if(isset(callback2)) {
			return safe_call.call(this,callback2,args);
		}
		return defaultValue;
	};

	function __safe( callbackName,source,args,defaultValue ){
		var undefinedVar;
		return safe_call.call( this, (isset(this.source[source])&&
			Object.prototype.hasOwnProperty.call(this.source[source], callbackName)) ? this.source[source][callbackName] : undefinedVar, args, function(){
			return safe_call.call(this,
					isset(this[callbackName][source])?
						this[callbackName][source]:(
							isset(this[callbackName][0])?
								this[callbackName][0]:(
									Object.prototype.hasOwnProperty.call(this, callbackName)?
										this[callbackName]:
										undefinedVar
								)
						),
					args,
					defaultSetting[callbackName][source]||defaultSetting[callbackName][0]||defaultSetting[callbackName],
					defaultValue
			);
		},defaultValue);
	};

	function __get( property,source ){
		if(!isset(source))
			source = 0;
		
		if( $.isArray(this.source) && isset(this.source[source]) && isset(this.source[source][property]))
			return this.source[source][property];
			
		if( isset(this[property]) ){
			if( $.isArray(this[property]) ){
				if( isset(this[property][source]) )
					return this[property][source];
				if( isset(this[property][0]) )
					return this[property][0];
				return null;
			}
			return this[property];
		}
		
		return null;
	};

	function loadRemote( url,sourceObject,done,debug ){
		$.ajax($.extend(true,{
			url : url,
			type  : 'GET' ,
			async:true,
			cache :false,
			dataType : 'json'
		 },sourceObject.ajax))
		 
		 .done(function( data ){
			done&&done.apply(this,$.makeArray(arguments));
		 })
		 
		 .fail(function( jqXHR, textStatus ){
			if( debug )
				console.log("Request failed: " + textStatus);
		 });
	}


	function findRight( data,query ){
		var right = false,source;
		
		for (source = 0;source < data.length;source += 1) {
			if( right = __safe.call(this,"findRight",source,[data[source],query,source]) ){
				return {right:right,source:source};
			}
		}
		
		return false;
	}

	function processData( data,query ){
		var source;
		preparseData
			.call( this,data,query );
		
		for (source = 0;source < data.length;source += 1) {
			data[source] = __safe.call(this,
				'filter',
				source,
				[data[source], query, source],
				data[source]
			);
		}
	};


	function collectData( query,datasource,callback ){
		var options = this,source;
		
		if( $.isFunction(options.source) ){
				options.source.apply(options,[query,function(items){
					datasource = [items];
					safe_call.call(options,callback,[query]);
				},datasource,0]);
		}else{
			for (source = 0;source < options.source.length;source += 1) {
				if ($.isArray(options.source[source])) {
					datasource[source] = options.source[source];
				} else if ($.isFunction(options.source[source])) {
					(function (source) {
						options.source[source].apply(options,[query, function(items){
							if (!datasource[source]) {
								datasource[source] = [];
							}
								
							if (items && $.isArray(items)) {
								switch (options.appendMethod) {
									case 'replace':
										datasource[source] = items;
									break;
									default:
										datasource[source] = datasource[source].concat(items);
								}
							}
								
							safe_call.call(options,callback,[query]);
						}, datasource,source]);
					}(source));
				} else {
					switch (options.source[source].type) {
						case 'remote':
							if (isset(options.source[source].url)) {
								if (!isset(options.source[source].minLength) || query.length >= options.source[source].minLength){
									var url = __safe.call(options,'replace',source,[options.source[source].url,query],'');
									if (!datasource[source]) {
										datasource[source] = [];
									}
									(function (source) {
										loadRemote(url,options.source[source], function(resp){
											datasource[source] = resp;
											safe_call.call(options,callback,[query]);
										},options.debug);
									}(source));
								}
							}
						break;
						default:
							if( isset(options.source[source]['data']) ){
								datasource[source] = options.source[source]['data'];
							}else{
								datasource[source] = options.source[source];
							}
					}
				}
			}
		}
		safe_call.call(options,callback,[query]);
	};

	function preparseData( data,query ){
		for( var source=0;source<data.length;source++ ){
			data[source] = __safe.call(this,
				'preparse',
				source,
				[data[source],query],
				data[source]
			);
		}
	};

	function renderData( data,query ){
		var  source, i, $div, $divs = [];
		
		for (source = 0;source < data.length;source += 1) {
			for (i = 0;i < data[source].length;i += 1) {
				if( $divs.length>=this.limit )
					break;
					
				$div = $(__safe.call(this,
					'render',source,
					[data[source][i],source,i,query],
					''
				));
				
				$div.data('source',source);
				$div.data('pid',i);
				$div.data('item',data[source][i]);
				
				$divs.push($div);
			}
		}
		
		return $divs;
	};

	function getItem( $div,dataset ){
		if( isset($div.data('source')) && 
			isset($div.data('pid')) && 
			isset(dataset[$div.data('source')]) && 
			isset(dataset[$div.data('source')][$div.data('pid')]) 
		){
			return dataset[$div.data('source')][$div.data('pid')]
		}
		return false;
	};

	function getValue( $div,dataset ){
		var item = getItem($div,dataset);
		
		if( item ){
			return __safe.call(this,
				'getValue',$div.data('source'),
				[item,$div.data('source')]
			);
		}else{
			if( isset($div.data('value')) ){
				return decodeURIComponent($div.data('value'));
			}else{
				return $div.html();
			}
		}
	};

	defaultSetting = {
		minLength: 0,
		valueKey: 'value',
		titleKey: 'title',
		highlight: true,

		showHint: true,

		dropdownWidth: '100%',
		dropdownStyle: {},
		itemStyle: {},
		hintStyle: false,
		style: false,

		debug: true,
		openOnFocus: false,
		closeOnBlur: true,

		autoselect: false,
		
		accents: true,
		replaceAccentsForRemote: true,
		
		limit: 20,
		visibleLimit: 20,
		visibleHeight: 0,
		defaultHeightItem: 30,

		timeoutUpdate: 10,

		get: function (property, source) {
			return __get.call(this,property,source);
		},
		
		replace: [
			function (url, query) {
				if (this.replaceAccentsForRemote) {
					query = accentReplace(query);
				}
				return url.replace('%QUERY%',encodeURIComponent(query));
			}
		],
		
		equal:function( value,query ){
			return query.toLowerCase()==value.substr(0,query.length).toLowerCase();
		},
		
		findRight:[
			function(items,query,source){
				var results = [],value = '',i;
				if (items) {
					for (i = 0;i < items.length;i += 1) {
						value = __safe.call(this,'getValue',source,[items[i],source]);
						if (__safe.call(this, 'equal', source, [value,query,source], false)) {
							return items[i];
						}
					}				
				}
				return false;
			}
		],
		
		valid:[
			function (value, query) {
				if (this.accents) {
					value = accentReplace(value);
					query = accentReplace(query);
				}
				return value.toLowerCase().indexOf(query.toLowerCase())!=-1;
				
			}
		],
		
		filter:[
			function (items, query, source) {
				var results = [], value = '',i;
				if (items) {					
					for (i = 0;i < items.length;i += 1) {
						value = isset(items[i][this.get('valueKey', source)]) ? items[i][this.get('valueKey', source)] : items[i].toString();
						if (__safe.call(this, 'valid', source, [value, query])) {
							results.push(items[i]); 
						}
					}
				}
				return results;
			}
		],
		
		preparse:function(items){
			return items;
		},
		
		getValue: [
			function (item, source) {
				return isset(item[this.get('valueKey',source)])?item[this.get('valueKey',source)]:item.toString();
			}
		],
		
		getTitle: [
			function (item, source) {
				return isset(item[this.get('titleKey',source)])?item[this.get('titleKey',source)]:item.toString();
			}
		],
		
		render: [
			function (item, source, pid, query) {
				var value = __safe.call(this, "getValue", source, [item, source], defaultSetting.getValue[0].call(this, item, source)),
					title = __safe.call(this, "getTitle", source, [item, source], defaultSetting.getTitle[0].call(this, item, source)),
					_value = '',
					_query = '',
					_title = '',
					hilite_hints = '',
					highlighted = '',
					c, h, i,
					spos = 0;
					
				if (this.highlight) {
					if (!this.accents) {
						title = title.replace(new RegExp('('+escapeRegExp(query)+')','i'),'<b>$1</b>');
					}else{
						_title = accentReplace(title).toLowerCase().replace(/[<>]+/g, ''),
						_query = accentReplace(query).toLowerCase().replace(/[<>]+/g, '');
						
						hilite_hints = _title.replace(new RegExp(escapeRegExp(_query), 'g'), '<'+_query+'>');
						for (i=0;i < hilite_hints.length;i += 1) {
							c = title.charAt(spos);
							h = hilite_hints.charAt(i);
							if (h === '<') {
								highlighted += '<b>';
							} else if (h === '>') {
								highlighted += '</b>';
							} else {
								spos += 1;
								highlighted += c;
							}
						}
						title = highlighted;
					}
				}
					
				return '<div '+(value==query?'class="active"':'')+' data-value="'+encodeURIComponent(value)+'">'
							+title+
						'</div>';
			}
		],
		appendMethod: 'concat', // supported merge and replace 
		source:[]
	};
	function init( that,options ){
		if( $(that).hasClass('xdsoft_input') )
				return;
		
		var $box = $('<div class="xdsoft_autocomplete"></div>'),
			$dropdown = $('<div class="xdsoft_autocomplete_dropdown"></div>'),
			$hint = $('<input readonly class="xdsoft_autocomplete_hint"/>'),
			$input = $(that),
			timer1 = 0,
			dataset = [],
			iOpen	= false,
			value = '',
			currentValue = '',
			currentSelect = '',
			active = null,
			pos = 0;
		
		//it can be used to access settings
		$input.data('autocomplete_options', options);
		
		$dropdown
			.on('mousedown', function(e) {
				e.preventDefault();
				e.stopPropagation();
			})
			.on('updatescroll.xdsoft', function() {
				var _act = $dropdown.find('.active');
				if (!_act.length) {
					return;
				}
				
				var top = _act.position().top,
					actHght = _act.outerHeight(true),
					scrlTop = $dropdown.scrollTop(),
					hght = $dropdown.height();
					
				if (top <0) {
					$dropdown.scrollTop(scrlTop-Math.abs(top));
				} else if (top+actHght>hght) {
					$dropdown.scrollTop(scrlTop+top+actHght-hght);
				}
			});
		
		$box
			.css({
				'display':$input.css('display'),
				'width':$input.css('width')
			});
		
		if( options.style )
			$box.css(options.style);
			
		$input
			.addClass('xdsoft_input')
			.attr('autocomplete','off');
		
		$dropdown
			.on('mousemove','div',function(){
				if( $(this).hasClass('active') )
					return true;
				$dropdown.find('div').removeClass('active');
				$(this).addClass('active');
			})
			.on('mousedown touchstart','div',function(){
				$dropdown.find('div').removeClass('active');
				$(this).addClass('active');
				$input.trigger('pick.xdsoft');
			})

		function manageData(){
			if ($input.val()!=currentValue){
				currentValue = $input.val();
			} else {
				return;
			}
			if (currentValue.length < options.minLength) {
				$input.trigger('close.xdsoft');
				return;
			}
			collectData.call(options,currentValue,dataset,function( query ){
				if (query != currentValue) {
					return;
				}
				var right;	
				processData.call(options, dataset,query);

				$input.trigger('updateContent.xdsoft');

				if (options.showHint && currentValue.length && currentValue.length<=$input.prop('size') && (right = findRight.call(options,dataset,currentValue))) {
					var title 	=  __safe.call(options,'getTitle',right.source,[right.right,right.source]);
					title = query + title.substr(query.length);
					$hint.val(title);
				} else {
					$hint.val('');
				}
			});

			return;
		}

		function manageKey (event) {
			var key = event.keyCode, right;
			
			switch( key ){
				case AKEY: case CKEY: case VKEY: case ZKEY: case YKEY:
					if (event.shiftKey || event.ctrlKey) {
						return true;
					}
				break;
				case SHIFTKEY:	
				case CTRLKEY:
					return true;
				break;
				case ARROWRIGHT:	
				case ARROWLEFT:
					if (ctrlDown || shiftDown || event.shiftKey || event.ctrlKey) {
						return true;
					}
					value = $input.val();
					pos = getCaretPosition($input[0]);
					if (key === ARROWRIGHT && pos === value.length) {
						if (right = findRight.call(options, dataset, value)){
							$input.trigger('pick.xdsoft', [
								__safe.call(options,
									'getValue', right.source,
									[right.right, right.source]
								)
							]);
						} else {
							$input.trigger('pick.xdsoft');
						}
						event.preventDefault();
						return false;
					}
					return true;
				case TAB:
				return true;
				case ENTER:
					if (iOpen) {
						$input.trigger('pick.xdsoft');
						event.preventDefault();
						return false;
					} else {
						return true;
					}
				break;
				case ESC:
					$input
						.val(currentValue)
						.trigger('close.xdsoft');
					event.preventDefault();
					return false;
				case ARROWDOWN:
				case ARROWUP:
					if (!iOpen) {
						$input.trigger('open.xdsoft');
						$input.trigger('updateContent.xdsoft');
						event.preventDefault();
						return false;
					}
					
					active = $dropdown.find('div.active');
					
					var next = key==ARROWDOWN?'next':'prev', timepick = true;
					
					if( active.length ){
						active.removeClass('active');
						if( active[next]().length ){
							active[next]().addClass('active');
						}else{
							$input.val(currentValue);
							timepick = false;
						}
					}else{
						$dropdown.children().eq(key==ARROWDOWN?0:-1).addClass('active');
					}
					
					if( timepick ){
						$input.trigger('timepick.xdsoft');
					}
					
					$dropdown
						.trigger('updatescroll.xdsoft');
					
					event.preventDefault();
					return false;	
			}
			return;
		}
		
		$input
			.data('xdsoft_autocomplete',dataset)
			.after($box)
			.on('pick.xdsoft', function( event,_value ){

				$input
					.trigger('timepick.xdsoft',_value)
				
				currentSelect = currentValue = $input.val();
				
				$input
					.trigger('close.xdsoft');
				
				//currentInput = false;
				
				active = $dropdown.find('div.active').eq(0);
							
				if( !active.length )
					active = $dropdown.children().first();
					
				$input.trigger('selected.xdsoft',[getItem(active,dataset)]);
			})
			.on('timepick.xdsoft', function( event,_value ){
				active = $dropdown.find('div.active');
							
				if( !active.length )
					active = $dropdown.children().first();
				
				if( active.length ){
					if( !isset(_value) ){
						$input.val(getValue.call(options,active,dataset));
					}else{
						$input.val(_value);
					}
					$input.trigger('autocompleted.xdsoft',[getItem(active,dataset)]);
					$hint.val('');
					setCaretPosition($input[0],$input.val().length);
				}
			})
			.on('keydown.xdsoft input.xdsoft cut.xdsoft paste.xdsoft', function( event ){
				var ret = manageKey(event);
				
				if (ret === false || ret === true) {
					return ret;
				}
				
				setTimeout(function(){
					manageData();
				},1);
				
				manageData();
			})
			.on('change.xdsoft', function( event ){
				currentValue = $input.val();
			});
		
		currentValue = $input.val();
		
		collectData.call(options, $input.val(),dataset,function( query ){
			processData.call(options,dataset,query);
		});
		
		if( options.openOnFocus ){
			$input.on('focusin.xdsoft',function(){
				$input.trigger('open.xdsoft');
				$input.trigger('updateContent.xdsoft');
			});
		}
		
		if( options.closeOnBlur )
			$input.on('focusout.xdsoft',function(){
				$input.trigger('close.xdsoft');
			});
			
		$box
			.append($input)
			.append($dropdown);


		var olderBackground = false,
			timerUpdate = 0;
		
		$input
			.on('updateHelperPosition.xdsoft',function(){
				clearTimeout(timerUpdate);
				timerUpdate = setTimeout(function(){
					$box.css({
						'display':$input.css('display'),
						'width':$input.css('width')
					});
					$dropdown.css($.extend(true,{
						left:$input.position().left,
						top:$input.position().top + parseInt($input.css('marginTop'))+parseInt($input[0].offsetHeight),
						marginLeft:$input.css('marginLeft'),
						marginRight:$input.css('marginRight'),
						width:options.dropdownWidth=='100%'?$input[0].offsetWidth:options.dropdownWidth
					},options.dropdownStyle));
					
					if (options.showHint) {
						var style = getComputedStyle($input[0], "");
						
						$hint[0].style.cssText = style.cssText;
						
						$hint.css({
							'box-sizing':style.boxSizing,
							borderStyle:'solid',
							borderCollapse:style.borderCollapse,
							borderLeftWidth:style.borderLeftWidth,
							borderRightWidth:style.borderRightWidth,
							borderTopWidth:style.borderTopWidth,
							borderBottomWidth:style.borderBottomWidth,
							paddingBottom:style.paddingBottom,
							marginBottom:style.marginBottom,
							paddingTop:style.paddingTop,
							marginTop:style.marginTop,
							paddingLeft:style.paddingLeft,
							marginLeft:style.marginLeft,
							paddingRight:style.paddingRight,
							marginRight:style.marginRight,
							maxHeight:style.maxHeight,
							minHeight:style.minHeight,
							maxWidth:style.maxWidth,
							minWidth:style.minWidth,
							width:style.width,
							letterSpacing:style.letterSpacing,
							lineHeight:style.lineHeight,
							outlineWidth:style.outlineWidth,
							fontFamily:style.fontFamily,
							fontVariant:style.fontVariant,
							fontStyle:$input.css('fontStyle'),
							fontSize:$input.css('fontSize'),
							fontWeight:$input.css('fontWeight'),
							flex:style.flex,
							justifyContent:style.justifyContent,
							borderRadius:style.borderRadius,
							'-webkit-box-shadow':'none',
							'box-shadow':'none'
						});
						
						$input.css('font-size',$input.css('fontSize'))// fix bug with em font size
						
						$hint.innerHeight($input.innerHeight());
						
						$hint.css($.extend(true,{
							position:'absolute',
							zIndex:'1',
							borderColor:'transparent',
							outlineColor:'transparent',
							left:$input.position().left,
							top:$input.position().top,
							background:$input.css('background')
						},options.hintStyle));
						
						
						if( olderBackground!==false ){
							$hint.css('background',olderBackground);
						}else{
							olderBackground = $input.css('background');
						}
						
						try{
							$input[0].style.setProperty('background', 'transparent', 'important');
						} catch(e) {
							$input.css('background','transparent')
						}

						$box
							.append($hint);
					}
				}, options.timeoutUpdate||1);
			});
		
		if ($input.is(':visible')) {
			$input
				.trigger('updateHelperPosition.xdsoft');
		} else {
			interval_for_visibility = setInterval(function () {
				if ($input.is(':visible')) {
					$input
						.trigger('updateHelperPosition.xdsoft');
					clearInterval(interval_for_visibility);
				}
			},100);
		}
		
		$(window).on('resize',function () {
			$box.css({
				'width':'auto'
			});
			$input
				.trigger('updateHelperPosition.xdsoft');
		})
		
		$input	
			.on('close.xdsoft',function(){
				if (!iOpen) {
					return;
				}

				$dropdown
					.hide();

				$hint
					.val('');	

				if (!options.autoselect) {
					$input.val(currentValue);
				}

				iOpen = false;

				//currentInput = false;
			})
			
			.on('updateContent.xdsoft',function(){
				var out = renderData.call(options,dataset,$input.val()),
					hght = 10;
				
				if (out.length) {
					$input.trigger('open.xdsoft');
				} else {
					$input.trigger('close.xdsoft');
					return;
				}

				$(out).each(function(){
					this.css($.extend(true,{
						paddingLeft:$input.css('paddingLeft'),
						paddingRight:$input.css('paddingRight')
					},options.itemStyle));
				});

				$dropdown
					.html(out);
					
				if (options.visibleHeight){
					hght = options.visibleHeight;
				} else {
					hght = options.visibleLimit * ((out[0] ? out[0].outerHeight(true) : 0) || options.defaultHeightItem) + 5;
				}
				
				$dropdown
					.css('maxHeight', hght+'px')
			})
			
			.on('open.xdsoft',function(){
				if( iOpen )
					return;
				
				$dropdown
					.show();

				iOpen = true;
					
				//currentInput = $input;
			})
			.on('destroy.xdsoft',function(){
				$input.removeClass('xdsoft');
				$box.after($input);
				$box.remove();
				clearTimeout(timer1);
				//currentInput = false;
				$input.data('xdsoft_autocomplete',null);
				$input
					.off('.xdsoft')
			});
	};
	
	publics = {
		destroy: function () {
			return this.trigger('destroy.xdsoft');
		},
		update: function () {
			return this.trigger('updateHelperPosition.xdsoft');	
		},
		options: function (_options) {
			if (this.data('autocomplete_options') && $.isPlainObject(_options)) {
				this.data('autocomplete_options', $.extend(true, this.data('autocomplete_options'), _options));
			}
			return this;
		},
		setSource: function (_newsource, id) {
			if(this.data('autocomplete_options') && ($.isPlainObject(_newsource) || $.isFunction(_newsource) || $.isArray(_newsource))) {
				var options = this.data('autocomplete_options'), 
					dataset = this.data('xdsoft_autocomplete'),
					source 	= options.source;
				if (id!==undefined && !isNaN(id)) {
					if ($.isPlainObject(_newsource) || $.isArray(_newsource)) {
						source[id] =  $.extend(true,$.isArray(_newsource) ? [] : {}, _newsource);
					} else {
						source[id] =  _newsource;
					}
				} else {
					if ($.isFunction(_newsource)) {
						this.data('autocomplete_options').source = _newsource;
					} else {
						$.extend(true, source, _newsource);
					}
				}
				
				collectData.call(options, this.val(), dataset,function( query ){
					processData.call(options,dataset,query);
				});
			}
			return this;
		},
		getSource: function (id) {
			if (this.data('autocomplete_options')) {
				var source = this.data('autocomplete_options').source;
				if (id!==undefined && !isNaN(id) &&source[id]) {
					return source[id];
				} else {
					return source;
				}
			}
			return null;
		} 
	};
	
	$.fn.autocomplete = function(_options, _second, _third){
		if ($.type(_options) === 'string' && publics[_options]) {
			return publics[_options].call(this, _second, _third);
		}
		return this.each(function () {
			var options = $.extend(true, {}, defaultSetting, _options);
			init(this, options);
		});
	};
}(jQuery));
