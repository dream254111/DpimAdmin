const axioscustom = axios.create();
axioscustom.defaults.baseURL = $('base').first().attr('href');
axioscustom.defaults.headers.post['X-Post-Back-Token'] = $('base').first().attr('postback') || '';
const $xtools = {
    isEmpty: function (x) {
        return x === undefined || x === null || x === '';
    },
    int(x) {
        return isNaN(parseInt(x)) ? 0 : parseInt(x);
    },
    dec(x, n) {
        if (isNaN(parseFloat(x))) return 0;
        n = isNaN(parseInt(n)) ? 6 : parseInt(n);
        let dc = new Decimal(x).toDP(n).toNumber();
        return dc;
    },
    async getLocal(url) {
        let error = null;
        let resp = await axioscustom.get(url);
        //console.log(resp)
        return resp.data;
    },
    async postLocalJson(url, data) {
        let error = null;
        let resp = null;
        resp = await axioscustom.post(url, data);
        //console.log(resp)
        return resp.data;
    },
    async postLocalForm(url, formdata) {
        let error = null;
        let resp = null;
        resp = await axioscustom.post(url, formdata, { headers: { 'Content-Type': 'multipart/form-data' } });
        //console.log(resp)
        return resp.data;
    },
    showResult(d, success_text, minimize, callback) {
        if (d.success) {
            if (!$xtools.isEmpty(success_text)) {
                if (minimize) {
                    $notify.success(success_text);
                }
                else {
                    $alert('', success_text, 'success', callback);
                }
            }
        } else {
            throw d.error;
        }
    },
    showError(err, ui, callback) {
        let msg = err.toString();
        if ($xt.strStartWith(msg, '[ui_code]')) {
            msg = ui[msg.split(':')[1]] || msg;
        }

        $alert('', msg, 'danger', callback);
    },
    strStartWith(str, strCompare) {
        str = str || '';
        str = str.toLowerCase();

        strCompare = strCompare || '';
        strCompare = strCompare.toLowerCase();

        return str.indexOf(strCompare) === 0;
    },
    strContains(str, strCompare) {
        str = str || '';
        str = str.toLowerCase();

        strCompare = strCompare || '';
        strCompare = strCompare.toLowerCase();

        return str.indexOf(strCompare) > -1;
    },
    checkEmpty(obj, props) {
        return $linq(props).any(x => $xtools.isEmpty(obj[x]));
    }
};

const $xt = $xtools;