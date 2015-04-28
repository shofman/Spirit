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
		if (response) {
			response = thirdType.isWeakAgainst(secondType);
		}
	}
}