#pragma strict
var pumplist : AnimationClip[];
var target : GameObject;
private var pumpnumber : int = 0;
private var drawed : boolean = true;
private var playerview : boolean = true;

var silencerDisplay : GameObject;

function Start(){
silencerDisplay.renderer.enabled=false;
target.animation.CrossFade("idleAct",0.4);
yield WaitForSeconds(4);
target.animation.CrossFade("idle",0.4);

}

function OnGUI () {
		if (GUI.Button(Rect(10,10,120,25),"fire"))
			fire();

		if (GUI.Button(Rect(140,10,70,25),"reload"))
			reload();
		
		if (GUI.Button(Rect(220,10,115,25),"silencer on/off"))
			silencer();	
			
		if (GUI.Button(Rect(345,10,30,25),"run"))
			run();	
			
		if (GUI.Button(Rect(595,10,130,25),"player/world model"))
			switchview();
	}

function fire () {
camera.fieldOfView = 50;
target.animation.Play("fire2");
target.animation.CrossFade("idle",0.4);
}

function reload () {
camera.fieldOfView = 50;
target.animation.Play("reload");
yield WaitForSeconds(2.2);
target.animation.CrossFade("idle");
}

function run () {
camera.fieldOfView = 50;
target.animation.Play("runStart");
target.animation.CrossFade("run",0.5);
}


function silencer () {
camera.fieldOfView = 50;
if (drawed==true) {
	target.animation["silencerOn"].speed = 1.3;
	target.animation.Play("silencerOn");
	silencerDisplay.renderer.enabled=true;
	yield WaitForSeconds(4);
	target.animation.Play("idle");
	drawed=false;
	}
else {
	target.animation.Play("silencerOff");
	drawed=true;
	yield WaitForSeconds(3.5);
	target.animation.CrossFade("idle");
	silencerDisplay.renderer.enabled=false;
	}
}

function switchview () {
	if(playerview){
		gameObject.Find("gunHD").renderer.enabled=true;
		gameObject.Find("hands").renderer.enabled=false;
		gameObject.Find("gun").renderer.enabled=false;
		gameObject.Find("topshell").renderer.enabled=false;
		gameObject.Find("silencer").renderer.enabled=false;
		gameObject.Find("charger").renderer.enabled=false;
		playerview = false;
	}
	else{
		gameObject.Find("gunHD").renderer.enabled=false;
		gameObject.Find("hands").renderer.enabled=true;
		gameObject.Find("gun").renderer.enabled=true;
		gameObject.Find("topshell").renderer.enabled=true;
		gameObject.Find("silencer").renderer.enabled=true;
		gameObject.Find("charger").renderer.enabled=true;
		playerview = true;
	}
}