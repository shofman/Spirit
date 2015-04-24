using System.Linq;
using UnityEngine;

public class EtherealElem : Elements {
	/**
	 * Singleton pattern - we want only one instance of a cave element per game
	 */
	protected static EtherealElem _instance;

	private EtherealElem() {
		// Element color inherited from parent Elements
        this.elementColor = new Color32(255,0,255,255);
	}

	public static Elements getInstance() {
		if (_instance == null) {
			_instance = new EtherealElem();
			_instance.assignStrengths();
			_instance.assignWeaknesses();
		}
		return _instance;
	}

	public void assignStrengths() {
		strengths = new Elements[] {LightElem.getInstance(), FireElem.getInstance()};
	}

	public void assignWeaknesses() {
		weaknesses = new Elements[] {ShadowElem.getInstance(), JungleElem.getInstance()};
	}

	public override bool isWeakAgainst(Elements e) {
		return _instance.getWeaknesses().Contains(e);
	}

	public override bool isStrongAgainst(Elements e) {
		return _instance.getStrengths().Contains(e);
	}
}