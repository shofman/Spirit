using System.Linq;
using UnityEngine;

public class FireElem : Elements {
	/**
	 * Singleton pattern - we want only one instance of a cave element per game
	 */
	protected static FireElem _instance;

	private FireElem() {
		// Element color inherited from parent Elements
        this.elementColor = new Color32(255,0,0,255);
	}

	public static Elements getInstance() {
		if (_instance == null) {
			_instance = new FireElem();
			_instance.assignStrengths();
			_instance.assignWeaknesses();
		}
		return _instance;
	}

	public void assignStrengths() {
		strengths = new Elements[] {IceElem.getInstance(), JungleElem.getInstance()};
	}

	public void assignWeaknesses() {
		weaknesses = new Elements[] {WaterElem.getInstance(), EtherealElem.getInstance()};
	}

	public override bool isWeakAgainst(Elements e) {
		return _instance.getWeaknesses().Contains(e);
	}

	public override bool isStrongAgainst(Elements e) {
		return _instance.getStrengths().Contains(e);
	}
}