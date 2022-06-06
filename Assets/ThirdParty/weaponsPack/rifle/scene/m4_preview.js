#pragma strict
var target : GameObject;
private var drawed : boolean = true;
private var aiming : boolean = false;
private var playerview : boolean = true;

function Start(){

}

function OnGUI () {
		if (GUI.Button(Rect(10,10,50,25),"fire"))
			fire();

		if (GUI.Button(Rect(70,10,70,25),"burst fire"))
			burstfire();
		
		if (GUI.Button(Rect(150,10,115,25),"Aim in/out"))
			aim();	
			
		if (GUI.Button(Rect(275,10,70,25),"reload"))
			reload();	
			
		if (GUI.Button(Rect(355,10,85,25),"holster/draw"))
			draw();	
			
		if (GUI.Button(Rect(595,10,130,25),"player/world model"))
			switchview();
	}

function fire () {
target.animation.Play("shotSingle");
target.animation.CrossFade("idle",0.4);
}

function burstfire () {
target.animation.Play("shotBurst");
}

function aim () {
if (!aiming) {
	animation["aimIN"].speed = 2;
	target.animation.Play("aimIN");
	aiming=true;
	}
else {
	animation["aimOUT"].speed = 2;
	target.animation.Play("aimOUT");
	aiming=false;
	target.animation.CrossFade("idle",1);
	}
}


function reload () {
animation["reload"].speed = 1.5;
target.animation.Play("reload");
target.animation.CrossFade("idle",7);
}

function draw () {

if (drawed) {
	animation["holster"].speed = 1.5;
	target.animation.Play("holster");
	drawed=false;
	}
else {
	animation["draw"].speed = 1.5;
	target.animation.Play("draw");
	drawed=true;
	target.animation.CrossFade("idle",1.5);
	}

}






function switchview () {
	if(playerview){
		gameObject.Find("hands").renderer.enabled=false;
		gameObject.Find("rifle08_m4").renderer.enabled=false;
		gameObject.Find("rifle08_charger").renderer.enabled=false;
		gameObject.Find("m4WHD").renderer.enabled=true;
		playerview = false;
	}
	else{
		gameObject.Find("m4WHD").renderer.enabled=false;
		gameObject.Find("hands").renderer.enabled=true;
		gameObject.Find("rifle08_m4").renderer.enabled=true;
		gameObject.Find("rifle08_charger").renderer.enabled=true;
		playerview = true;
	}
}