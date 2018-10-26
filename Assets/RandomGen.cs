using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGen {

	static System.Random rand = new System.Random();

	static float probMul = 10000; //Multiply this with probabilty to get amount of elements
	static float resolution = 0.1f; //Element resolution

	static List<float> probList = new List<float>();

	public static void initGauss(float rangeMin, float rangeMax, float b) {
		for(float i = rangeMin; i<rangeMax; i+=resolution) {
			addMultipleToList(i, (int)(probMul*Gauss_Distr(i, b)));
		}
		Globals.output("Initialized.");
		Globals.output("Random sample size: " + probList.Count);
	}

	public static float getRandomGauss() {
		return probList[rand.Next(probList.Count)];
	}

	//using box-mueller transform to get gauss-distr. See: https://stackoverflow.com/questions/218060/random-gaussian-variables
	//public static float getRandomGauss() {
	//	double u1 = 1.0-rand.NextDouble(); //uniform(0,1] random doubles
	//	double u2 = 1.0-rand.NextDouble();
	//	double randStdNormal = System.Math.Sqrt(-2.0 * System.Math.Log(u1)) *
	//		System.Math.Sin(2.0 * System.Math.PI * u2); //random normal(0,1)
	//	Debug.Log(randStdNormal);
	//	return (float)randStdNormal;
	//}

	public static float Gauss_Distr(float x, float b) {
		float t1 = Mathf.Pow( 3.0f / (2.0f*Mathf.PI * Mathf.Pow(b, 2)), (3.0f/2.0f));
		float t2 = Mathf.Exp(-1.0f * ( (3.0f*Mathf.Pow(Mathf.Abs(x), 2)) / (2.0f*Mathf.Pow(b, 2)) ) );
		return t1*t2;
	}

	private static void addMultipleToList(float item, int amount) {
		for(int i=0; i<amount; i++) {
			probList.Add(item);
		}
		//Debug.Log("probList Added: " + item + " " + amount  + " times");
	}
}
