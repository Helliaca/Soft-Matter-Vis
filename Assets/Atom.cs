using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atom : MonoBehaviour {

	public Transform atomPrefab;

	Vector3 bond;
	Atom next;
	LineRenderer line;

	// Use this for initialization
	void Start () {
		line = transform.GetComponent<LineRenderer>();
		line.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(next!=null) {
			line.enabled = true;
			line.SetPosition(0, transform.position);
			line.SetPosition(1, next.transform.position);
		}
		else line.enabled = false;
	}

	public void generate(int atoms) {
		atoms--;
		if(atoms>0) {
			bond = Random.insideUnitSphere;
			bond *= RandomGen.getRandomGauss()+Globals.bondlength;

			next = Instantiate(atomPrefab, transform.position+bond, Quaternion.identity).GetComponent<Atom>();
			next.generate(atoms);
		}
	}

	public void delete() {
		if(next!=null) next.delete();
		Destroy(this.gameObject);
	}
}
