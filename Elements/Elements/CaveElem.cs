using System.Linq;
using UnityEngine;

public class CaveElem : Elements {
	/**
	 * Singleton pattern - we want only one instance of a cave element per game
	 */
	protected static CaveElem _instance;

	private CaveElem() {

	}

	public static Elements getInstance() {
		if (_instance == null) {
			_instance = new CaveElem();
			_instance.assignStrengths();
			_instance.assignWeaknesses();
		}
		return _instance;
	}

	public void assignStrengths() {
		strengths = new Elements[] {AirElem.getInstance(), LightElem.getInstance()};
	}

	public void assignWeaknesses() {
		weaknesses = new Elements[] {IceElem.getInstance(), JungleElem.getInstance()};
	}

	public override bool isWeakAgainst(Elements e) {
		return _instance.getWeaknesses().Contains(e);
	}

	public override bool isStrongAgainst(Elements e) {
		return _instance.getStrengths().Contains(e);
	}
}