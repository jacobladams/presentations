var TypeScriptDemo;
(function (TypeScriptDemo) {
    var BoxDrawer = (function () {
        function BoxDrawer() {
        }
        BoxDrawer.prototype.drawBox = function (boxDetails) {
            var box = document.createElement('div');
            box.style.width = boxDetails.width + 'px';
            box.style.height = boxDetails.height + 'px';
            box.innerText = boxDetails.title + ' (Area: ' + boxDetails.height * boxDetails.width + 'px)';
            box.style.background = 'lightblue';
            box.style.display = 'inline-box';
            document.body.appendChild(box);
        };
        return BoxDrawer;
    })();
    TypeScriptDemo.BoxDrawer = BoxDrawer;
})(TypeScriptDemo || (TypeScriptDemo = {}));
var boxDrawer = new TypeScriptDemo.BoxDrawer();
boxDrawer.drawBox({ title: 'Hello World', height: 200, width: 300 });
