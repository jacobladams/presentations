module TypeScriptDemo {
	export interface IBoxDetails{
		title:string;
		height:number;
		width:number;
	}
	
	export class BoxDrawer {
		drawBox(boxDetails: TypeScriptDemo.IBoxDetails) {
		    var box = document.createElement('div');
		    
		    box.style.width = boxDetails.width + 'px';
		    box.style.height = boxDetails.height + 'px';
		    box.innerText = boxDetails.title + ' (Area: ' + boxDetails.height * boxDetails.width + 'px)';
		    box.style.background = 'lightblue';
			box.style.display = 'inline-box';
		    document.body.appendChild(box);
		}
	}
}

var boxDrawer = new TypeScriptDemo.BoxDrawer();
boxDrawer.drawBox({title: 'Hello World', height:200, width:300});