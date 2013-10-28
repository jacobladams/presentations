function drawBox(title, height, width) {
    var box = document.createElement('div');

    box.style.width = width + 'px';
    box.style.height = height + 'px';
    box.innerText = title + ' (Area: ' + height * width + 'px)';
    box.style.background = 'lightblue';
    box.style.display = 'inline-block';
    document.body.appendChild(box);
}
;

drawBox('Hello World', '200px', '300px');
//# sourceMappingURL=script.js.map
