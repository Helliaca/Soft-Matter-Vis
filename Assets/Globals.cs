using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour {

	public static Globals GL;

	public static Transform SourceAtom;
	public static Transform RadiusSphere_purple;
	public static Transform RadiusSphere_green;
	public static CommandLineController cmd;

	public static float bondlength;

	static Transform gyrRadiusSphere;
	static Transform R2RadiusSphere;

	static List<Transform> sourceAtoms;

	// Use this for initialization
	void Awake() {
		if(GL != null) GameObject.Destroy(GL);
		else GL = this;
		DontDestroyOnLoad(this);
	}

	void Start() {
		sourceAtoms = new List<Transform>();
		Globals.bondlength = 5.0f; //Changing bondlength requires restarting
		Invoke("lateInitGauss", 0.5f); //Initializign Gauss slightly later, in order to guarantee GlobalsSetter is done
	}

	void lateInitGauss() {
		RandomGen.initGauss(-1.0f*bondlength, bondlength, bondlength);
	}

	// Update is called once per frame
	void Update () {
		
	}

	public static void generateSingleChain(int length) {
		Transform sa = Instantiate(Globals.SourceAtom, Vector3.zero, Quaternion.identity);
		sa.GetComponent<Atom>().generate(length);
		sourceAtoms.Add(sa);
	}

	public static void clearAllChains() {
		foreach(Transform t in sourceAtoms) {
			t.GetComponent<Atom>().delete();
		}
		if(gyrRadiusSphere != null) Destroy(gyrRadiusSphere.gameObject);
		if(R2RadiusSphere != null) Destroy(R2RadiusSphere.gameObject);
	}

	public static void output(string s) {
		Globals.cmd.addLine(s);
	}


	public static void calcGyrationRadius(int atomAmount) {
		if(gyrRadiusSphere != null) Destroy(gyrRadiusSphere.gameObject);
		float sum = 0;
		for(int m=1; m<=atomAmount; m++) {
			for(int n=1; n<=atomAmount; n++) {
				sum += Mathf.Abs(n-m)*Mathf.Pow(bondlength, 2.0f);
			}
		}
		sum *= 1.0f/(2.0f*Mathf.Pow(atomAmount, 2.0f));
		sum = Mathf.Sqrt(sum);

		Globals.output("Radius of Gyration: " + sum);

		//sum = atomAmount/(6.0f*Mathf.Pow(bondlength, 2.0f));
		//Globals.output("Radius of Gyration: " + sum);

		gyrRadiusSphere = Instantiate(RadiusSphere_purple, Vector3.zero, Quaternion.identity);
		gyrRadiusSphere.localScale = new Vector3(1.0f, 1.0f, 1.0f)*sum*2.0f; //*2.0f because our sphere has radius of 0.5f
	}

	public static void calcR2Radius(int atomAmount) {
		if(R2RadiusSphere != null) Destroy(R2RadiusSphere.gameObject);
		float sum = 0;

		sum = (float)atomAmount * Mathf.Pow(bondlength, 2.0f);
		sum = Mathf.Sqrt(sum);

		Globals.output("Polymer size sqrt(N*b^2) : " + sum);

		R2RadiusSphere = Instantiate(RadiusSphere_green, Vector3.zero, Quaternion.identity);
		R2RadiusSphere.localScale = new Vector3(1.0f, 1.0f, 1.0f)*sum*2.0f; //*2.0f because our sphere has radius of 0.5f
	}

	public static void toggleGyr() {
		if(gyrRadiusSphere==null) return;
		gyrRadiusSphere.gameObject.SetActive(!gyrRadiusSphere.gameObject.activeSelf);
	}

	public static void toggleR2() {
		if(R2RadiusSphere==null) return;
		R2RadiusSphere.gameObject.SetActive(!R2RadiusSphere.gameObject.activeSelf);
	}

}
