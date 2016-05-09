function showvideo(sid,cno){
	var no, cno;
	no=parseInt(cno,10);
	no=no-1;
	cno=no.toString();
	url="../showvideo.asp?sid="+sid+"&cno="+cno;
	window.open(url, 'showvideo', 'height=488, width=742, top=0, left=0, toolbar=no, menubar=no, scrollbars=no, resizable=no,location=no, status=no');
}

function redirect(url){
	window.location.href=url;
}
    
function imgzoom(o){
	var zoom=parseInt(o.style.zoom, 10)||100;zoom+=event.wheelDelta/12;if (zoom>0) o.style.zoom=zoom+'%';return false;
}

N = (document.all) ? 0 : 1;
var ob;
function MD(e) {
    if (N) {
        ob = document.layers[e.target.name];
        X = e.x;
        Y = e.y;
        return false;
    }
    else {
        ob = event.srcElement.parentElement.style;
        X = event.offsetX;
        Y = event.offsetY;
    }
}

function MM(e) {
    if (ob) {
        if (N) {
            ob.moveTo((e.pageX - X), (e.pageY - Y));
        }
        else {
            ob.pixelLeft = event.clientX - X + document.body.scrollLeft;
            ob.pixelTop = event.clientY - Y + document.body.scrollTop;
            return false;
        }
    }
}

function MU() {
    ob = null;
}

if (N) {
    document.captureEvents(Event.MOUSEDOWN | Event.MOUSEMOVE | Event.MOUSEUP);
}
document.onmousedown = MD;
document.onmousemove = MM;
document.onmouseup = MU;