using System.Linq;
using UnityEngine;

public class LightElem : Elements {
	/**
	 * Singleton pattern - we want only one instance of a cave element per game
	 */
	protected static LightElem _instance;

	private LightElem() {
		// Element color inherited from parent Elements
        this.elementColor = new Color32(255,255,0,255);
	}

	public static Elements getInstance() {
		if (_instance == null) {
			_instance = new LightElem();
			_instance.assignStrengths();
			_instance.assignWeaknesses();
		}
		return _instance;
	}

	public void assignStrengths() {
		strengths = new Elements[] {ShadowElem.getInstance(), AirElem.getInstance()};
	}

	public void assignWeaknesses() {
		weaknesses = new Elements[] {EtherealElem.getInstance(), CaveElem.getInstance()};
	}

	public override bool isWeakAgainst(Elements e) {
		return _instance.getWeaknesses().Contains(e);
	}

	public override bool isStrongAgainst(Elements e) {
		return _instance.getStrengths().Contains(e);
	}
}