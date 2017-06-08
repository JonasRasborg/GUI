
const ele = document.querySelector('square');

ele.addEventListener('mousedown',
    function() {
        const mousemoveCb = function(event) {
            ele.style.left = event.x - 50 + 'px';
            ele.style.top = event.y - 50 + 'px';
        }

        const mouseupCb = function() {
            ele.removeEventListener('mousemove', mousemoveCb);
            ele.removeEventListener('mouseup', mouseupCb);
        }

        ele.addEventListener('mousemove', mousemoveCb);
        ele.addEventListener('mouseup', mouseupCb);
    }
);