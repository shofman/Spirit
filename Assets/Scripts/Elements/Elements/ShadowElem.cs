using System.Linq;
using UnityEngine;

public class ShadowElem : Elements {
	/**
	 * Singleton pattern - we want only one instance of a cave element per game
	 */
	protected static ShadowElem _instance;

	private ShadowElem() {
		// Element color inherited from parent Elements
        this.elementColor = new Color32(26,26,26,255);
	}

	public static Elements getInstance() {
		if (_instance == null) {
			_instance = new ShadowElem();
			_instance.assignStrengths();
			_instance.assignWeaknesses();
		}
		return _instance;
	}

	public void assignStrengths() {
		strengths = new Elements[] {EtherealElem.getInstance(), AirElem.getInstance()};
	}

	public void assignWeaknesses() {
		weaknesses = new Elements[] {LightElem.getInstance(), WaterElem.getInstance()};
	}

	public override bool isWeakAgainst(Elements e) {
		return _instance.getWeaknesses().Contains(e);
	}

	public override bool isStrongAgainst(Elements e) {
		return _instance.getStrengths().Contains(e);
	}
}