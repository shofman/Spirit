using System.Linq;
using UnityEngine;

public class IceElem : Elements {
	/**
	 * Singleton pattern - we want only one instance of a cave element per game
	 */
	protected static IceElem _instance;

	private IceElem() {
		// Element color inherited from parent Elements
        this.elementColor = new Color32(128,128,255,255);
	}

	public static Elements getInstance() {
		if (_instance == null) {
			_instance = new IceElem();
			_instance.assignStrengths();
			_instance.assignWeaknesses();
		}
		return _instance;
	}

	public void assignStrengths() {
		strengths = new Elements[] {WaterElem.getInstance(), CaveElem.getInstance()};
	}

	public void assignWeaknesses() {
		weaknesses = new Elements[] {FireElem.getInstance(), LightElem.getInstance()};
	}

	public override bool isWeakAgainst(Elements e) {
		return _instance.getWeaknesses().Contains(e);
	}

	public override bool isStrongAgainst(Elements e) {
		return _instance.getStrengths().Contains(e);
	}
}