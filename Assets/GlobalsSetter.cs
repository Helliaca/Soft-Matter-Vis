using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalsSetter : MonoBehaviour {

	public Transform SourceAtom;
	public Transform RadiusSphere_purple;
	public Transform RadiusSphere_green;
	public CommandLineController cmd;

	// Use this for initialization
	void Start () {
		Globals.SourceAtom = SourceAtom;
		Globals.cmd = cmd;
		Globals.RadiusSphere_purple = RadiusSphere_purple;
		Globals.RadiusSphere_green = RadiusSphere_green;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
