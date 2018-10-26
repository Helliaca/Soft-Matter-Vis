using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommandLineController : MonoBehaviour {

	public InputField input;
	public Text output;

	private int lastUsedLength = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Tab)) input.ActivateInputField();
		if(Input.GetKeyDown(KeyCode.Return) && input.text!="") {
			addLine(input.text);
			string[] cmd = input.text.Split(new char[] {' ','\t'}, System.StringSplitOptions.RemoveEmptyEntries);
			input.text = "";

			if(cmd[0] == "genchain" && cmd.Length == 2) {
				int l = int.Parse(cmd[1]);
				Globals.output("Generating single chain, l=" + l + "...");
				Globals.generateSingleChain(l);
				Globals.output("Done");
				lastUsedLength = l;
			}
			else if(cmd[0] == "genchain" && cmd.Length == 3) {
				int a = int.Parse(cmd[1]);
				int l = int.Parse(cmd[2]);
				Globals.output("Generating " + a + " chains, l=" + l + "...");
				for (int i=0; i<a; i++) Globals.generateSingleChain(l);
				Globals.output("Done");
				lastUsedLength = l;
			}
			else if(cmd[0] == "clear") {
				Globals.output("Clearing all chains...");
				Globals.clearAllChains();
				Globals.output("Done");
			}
			else if(cmd[0] == "calc" && cmd.Length == 2) {
				if(cmd[1]=="gyr") Globals.calcGyrationRadius(lastUsedLength);
				if(cmd[1]=="R2") Globals.calcR2Radius(lastUsedLength);
			}
			else if(cmd[0] == "toggle" && cmd.Length == 2) {
				if(cmd[1]=="gyr") Globals.toggleGyr();
				if(cmd[1]=="R2") Globals.toggleR2();
			}
			else {
				Globals.output("Command not recognized: " + cmd[0]);
			}

		}
	}

	public void addLine(string txt) {
		output.text += "\n" + txt;
	}
}
