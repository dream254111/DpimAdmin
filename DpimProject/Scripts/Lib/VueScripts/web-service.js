const base = $(`base`).first();
const baseUrl = base.attr(`href`);
const xhash = base.attr(`xhash`);
const cook = "";
const $dtl = {
    queryString: (a => {
        if (a === '') return {};
        let b = {};
        for (let i = 0; i < a.length; ++i) {
            let p = a[i].split('=', 2);
            b[p[0]] = (p.length === 1) ? '' : decodeURIComponent(p[1].replace(/\+/g, ' '));
        }
        return b;
    })(window.location.search.substr(1).split('&')),
    cookies: {
        set: function (cname, cvalue, exdays) {
            exdays = exdays || 30;
            let d = new Date();
            d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
            let expires = "expires=" + d.toUTCString();
            document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
        },
        get: function (cname) {
            let name = cname + "=";
            let decodedCookie = decodeURIComponent(document.cookie);
            let ca = decodedCookie.split(';');
            for (let i = 0; i < ca.length; i++) {
                let c = ca[i];
                while (c.charAt(0) === ' ') {
                    c = c.substring(1);
                }
                if (c.indexOf(name) === 0) {
                    return c.substring(name.length, c.length);
                }
            }
            return "";
        }
    },
    isNullOrEmpty(o) {
        return (o === undefined || o === null || o === '');
    },
    isSelectedData(data, index) {
        let c = Object.assign([], data);
        let idx = 0;
        $linq(c).foreach(x => {
            x.checked = index === idx;
            idx++;
        });
    },
    float(o, n) {
        let fls = (o || 0).toString();
        let fl = 0;

        fls = fls.replace(/,/g, '');

        //if (!isNaN(parseFloat(fls))) {
        //    fl = parseFloat(parseFloat(fls).toFixed(n || 8));
        //}

        n = !isNaN(parseInt(n)) ? parseInt(n) : 6;

        let dec = new Decimal(fls);

        return dec.isNaN() ? 0 : dec.toDP(n).toNumber();
    },
    int(o) {
        let ins = (o || 0).toString();
        let int = 0;
        ins = ins.replace(/,/g, '');
        if (!isNaN(parseFloat(ins))) {
            int = parseInt(ins);
        }
        return int;
    },
    dec(x) {
        return new Decimal(x);
    },
    numberFormat(n) {
        if ($dtl.isNullOrEmpty(n) || isNaN(parseFloat(n)))
            return null;
        else {
            let o = '';
            n = n.toString();
            let sp = n.split('.');
            o = parseInt(sp[0]).toLocaleString('en-US');
            if (sp.length > 1) {
                o += '.' + sp[1];
            }
            return o;
        }
    },
    httpPost(form, action, done, failed, always, progress,header) {
        $.ajax({
            url: action,
            type: 'POST',
            headers: {
                'X-Post-Back-Token': ``,
                'X-MG-User-Hash': xhash,
                'Authorization':token
            },
            data: form,
            async: true,
            cache: false,
            contentType: false,
            processData: false,
            success: (data) => {
                done(data);
            },
            error: (jqXHR, textStatus, errorThrown) => {
                failed("error " + jqXHR.status + " " + errorThrown);
            },
            complete: () => {
                always();
            },
            xhr: () => {
                let xhr = new window.XMLHttpRequest();
          
                xhr.upload.addEventListener("progress", (evt) => {
                    if (evt.lengthComputable) {
                        let percentComplete = parseInt(evt.loaded / evt.total);
                        if (percentComplete <= 25) {
                            percentComplete = 25
                        } else if (percentComplete > 25 && percentComplete <= 50) {
                            percentComplete = 50
                        } else if (percentComplete < 100) {
                            percentComplete = 75
                        } else {
                            percentComplete = 100
                        }
                        if (progress) {
                            progress(percentComplete)
                        }
                    }
                }, false);
                xhr.addEventListener("progress", (evt) => {
                    if (evt.lengthComputable) {
                        let percentComplete = parseInt(evt.loaded / evt.total);
                        if (percentComplete <= 25) {
                            percentComplete = 25
                        } else if (percentComplete > 25 && percentComplete <= 50) {
                            percentComplete = 50
                        } else if (percentComplete < 100) {
                            percentComplete = 75
                        } else {
                            percentComplete = 100
                        }
                        if (progress) {
                            progress(percentComplete)
                        }
                    }
                }, false);
                xhr.upload.addEventListener('loadend', function (e) {
                    if (progress) {
                        progress(100)
                    }
                });
                xhr.addEventListener('loadend', function (e) {
                    if (progress) {
                        progress(100)
                    }
                });
                return xhr;
            },
        });
    },
    httpGet(action, done, failed, always, progress,header) {
        $.ajax({
            url: action,
            type: 'GET',
            headers: {
                'X-MG-User-Hash': xhash,
                'Authorization': token

            },
            data: null,
            async: true,
            cache: false,
            contentType: false,
            processData: false,
            success: (data) => {
                done(data);
            },
            error: (jqXHR, textStatus, errorThrown) => {
                failed("error " + jqXHR.status + " " + errorThrown);
            },
            complete: () => {
                always();
            },
            xhr: () => {
                let xhr = new window.XMLHttpRequest();
                xhr.upload.addEventListener("progress", (evt) => {
                    if (evt.lengthComputable) {
                        let percentComplete = parseInt(evt.loaded / evt.total);
                        if (percentComplete <= 25) {
                            percentComplete = 25
                        } else if (percentComplete > 25 && percentComplete <= 50) {
                            percentComplete = 50
                        } else if (percentComplete < 100) {
                            percentComplete = 75
                        } else {
                            percentComplete = 100
                        }
                        if (progress) {
                            progress(percentComplete)
                        }
                    }
                }, false);

                xhr.addEventListener("progress", (evt) => {
                    if (evt.lengthComputable) {
                        let percentComplete = parseInt(evt.loaded / evt.total);
                        if (percentComplete <= 25) {
                            percentComplete = 25
                        } else if (percentComplete > 25 && percentComplete <= 50) {
                            percentComplete = 50
                        } else if (percentComplete < 100) {
                            percentComplete = 75
                        } else {
                            percentComplete = 100
                        }
                        if (progress) {
                            progress(percentComplete)
                        }
                    }
                }, false);
                xhr.upload.addEventListener('loadend', function (e) {
                    if (progress) {
                        progress(100)
                    }
                });
                xhr.addEventListener('loadend', function (e) {
                    if (progress) {
                        progress(100)
                    }
                });
                return xhr;
            },
        });
    },
    get(url, fn, done, header) {
        $dtl.httpGet(url, (d) => {
            if (fn) {
                fn(d);
            }
        },
            err => {
                $alert(``, err, `danger`);
            },
            () => {
                if (done) {
                    done();
                };
            },
            (per) => {
            }
            , header);
    },
    post(obj, url, fn, always,header) {
        if (!(obj && obj.constructor && obj.constructor.name && obj.constructor.name === 'FormData')) { obj = JSON.stringify(obj); }
        $dtl.httpPost(obj, url, d => {
            console.log(xhash);

            if (fn) {
                fn(d);
            }
        }, err => {
            $alert(``, err, `danger`);
        }, () => {
            if (always) {
                always();
            };
        },null,header);
    },
    thaiDate(dt) {
        let date = moment(dt)
        if (!date.isValid()) {
            date = moment(dt, 'DD/MM/YYYY');
        }
        if (!date.isValid()) {
            return null
        } else {
            return date.format('DD/MM/YYYY');
        }
    },
    thaiDateTime(dt) {
        let date = moment(dt)
        if (!date.isValid()) {
            date = moment(dt, 'DD/MM/YYYY HH:mm:ss');
        }
        if (!date.isValid()) {
            return null
        } else {
            return date.format('DD/MM/YYYY HH:mm:ss');
        }
    }
};

class Pagination {
    constructor() {
        this.total_item = 0;
        this.skip = 0;
        this.take = 10;
        this.current_page = 1;
    }

    setItemsPerPage(x) {
        this.take = x;
    }

    getItemsPerPage() {
        return this.take || 0;
    }

    setTotalItems(x) {
        this.total_item = x;
    }

    getTotalItems() {
        return this.total_item || 0;
    }

    setCurrentPage(x) {
        this.current_page = x;
    }

    getCurrentPage() {
        return this.current_page;
    }

    getTotalPages() {
        let p = (this.total_item % this.take != 0) ? Math.floor(this.total_item / this.take) + 1 : Math.floor(this.total_item / this.take);
        p = p < 1 ? 1 : p;
        return p;
    }

    skipItems() {
        return ((this.current_page > 0 ? this.current_page : 1) - 1) * this.take;
    }

    getItemNo(index) {
        return (index + (this.getItemsPerPage() * (this.getCurrentPage() - 1)) + 1);
    }

    createPagesArray() {
        let total_pages = this.getTotalPages();
        let outp_arr = [];
        if (total_pages <= 7) {
            for (let i = 1; i <= total_pages; i++) {
                outp_arr.push({ page_no: i, active: i == this.current_page });
            }
        }
        else if (total_pages > 7 && this.current_page <= 4) {
            for (let i = 1; i <= 7; i++) {
                if (i == 7) {
                    //outp_arr.push({ page_no: 0, active: false, icon: null });
                    outp_arr.push({ page_no: total_pages, active: total_pages == this.current_page, icon: null });
                } else {
                    outp_arr.push({ page_no: i, active: i == this.current_page, icon: null });
                }
            }
        }
        else if (total_pages > 7 && this.current_page >= total_pages - 3) {
            for (let i = 1; i <= 7; i++) {
                if (i == 1) {
                    outp_arr.push({ page_no: 1, active: 1 == this.current_page, icon: null });
                    //outp_arr.push({ page_no: 0, active: false, icon: null });
                }
                else {
                    outp_arr.push({ page_no: total_pages - 7 + i, active: (total_pages - 7 + i) == this.current_page, icon: null });
                }
            }
        } else {
            let start = this.current_page - 7 + 4;
            let end = 0;
            for (let i = 1; i <= 7; i++) {
                if (i == 1) {
                    outp_arr.push({ page_no: 1, active: 1 == this.current_page, icon: null });
                    //outp_arr.push({ page_no: 0, active: false, icon: null });
                }
                else if (i == 7) {
                    //outp_arr.push({ page_no: 0, active: false, icon: null });
                    outp_arr.push({ page_no: total_pages, active: total_pages == this.current_page, icon: null });
                } else {
                    outp_arr.push({ page_no: start - 1 + i, active: (start - 1 + i) == this.current_page, icon: null });
                }
            }
        }
        return outp_arr;
    }
}