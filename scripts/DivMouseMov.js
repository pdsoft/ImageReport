
_dragObj = new Object();

function dragBegin(e) {

    _dragObj = document.getElementById('divNote');

    var x, y;

    if (navigator.userAgent.indexOf("MSIE") >= 0) {
        x = window.event.clientX + document.documentElement.scrollLeft + document.body.scrollLeft;
        y = window.event.clientY + document.documentElement.scrollTop + document.body.scrollTop;
    } else {
        x = e.clientX + window.scrollX;
        y = e.clientY + window.scrollY;
    }

    _dragObj.cursorStartX = x;
    _dragObj.cursorStartY = y;

    if (navigator.userAgent.indexOf("MSIE") >= 0) {
        document.attachEvent("onmousemove", dragContinue);
        document.attachEvent("onmouseup", dragEnd);
        window.event.cancelBubble = true;
        window.event.returnValue = false;
    } else {
        document.addEventListener("mousemove", dragContinue, true);
        document.addEventListener("mouseup", dragEnd, true);
        e.preventDefault();
    }
}

function dragContinue(e) {
    var x, y;

    var isIE = _browser.isIE;
    var isWebKitBased = _browser.isWebKitBased;

    if (navigator.userAgent.indexOf("MSIE") >= 0) {
        x = window.event.clientX + document.documentElement.scrollLeft + document.body.scrollLeft;
        y = window.event.clientY + document.documentElement.scrollTop + document.body.scrollTop;
    } else {
        x = e.clientX + window.scrollX;
        y = e.clientY + window.scrollY;
    }

    var distance = x - _dragObj.cursorStartX;

    distance = Math.abs(distance);

    // or top, bottom, right
    _dragObj.elNode.style.left = (_dragObj.elStartLeft + x - _dragObj.cursorStartX) + "px";

    if (navigator.userAgent.indexOf("MSIE") >= 0) {
        window.event.cancelBubble = true;
        window.event.returnValue = false;
    } else {
        e.preventDefault();
    }
}

function dragEnd() {
    if (navigator.userAgent.indexOf("MSIE") >= 0) {
        document.detachEvent("onmousemove", dragContinue);
        document.detachEvent("onmouseup", dragEnd);
    } else {
        document.removeEventListener("mousemove", dragContinue, true);
        document.removeEventListener("mouseup", dragEnd, true);
    }
}