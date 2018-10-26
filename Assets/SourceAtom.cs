using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceAtom : MonoBehaviour {

	public Transform sourceAtom;

	// Use this for initialization
	void Start () {
		RandomGen.initGauss(-5.0f, 5.0f, 5.0f);
		sourceAtom.GetComponent<Atom>().generate(100);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
