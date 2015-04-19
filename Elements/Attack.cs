using UnityEngine;
using System.Collections;

public class Attack : MonoBehaviour {
	Elements firstType;
	Elements secondType;
	Elements thirdType;

	public void Awake() {

	}

	public void Start() {
		firstType = WaterElem.getInstance();
		secondType = FireElem.getInstance();
		thirdType = IceElem.getInstance();


		bool response = firstType.isWeakAgainst(secondType);
		Debug.Log("FIRST IS WEAK AGAINST SECOND: " + response);
		
		response = secondType.isWeakAgainst(firstType);
		Debug.Log("SECOND IS WEAK AGAINST FIRST: " + response);

		response = firstType.isWeakAgainst(thirdType);
		Debug.Log("FIRST IS WEAK AGAINST THIRD: " + response);
		
		response = thirdType.isWeakAgainst(firstType);
		Debug.Log("SECOND IS WEAK AGAINST FIRST: " + response);

		response = thirdType.isWeakAgainst(secondType);
		Debug.Log("THIRD IS WEAK AGAINST SECOND: " + response);
		
		response = secondType.isWeakAgainst(thirdType);
		Debug.Log("SECOND IS WEAK AGAINST THIRD: " + response);


	}
}