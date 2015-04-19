using System.Linq;
using UnityEngine;

public class WaterElem : Elements {
	/**
	 * Singleton pattern - we want only one instance of a cave element per game
	 */
	protected static WaterElem _instance;

	private WaterElem() {

	}

	public static Elements getInstance() {
		if (_instance == null) {
			_instance = new WaterElem();
			_instance.assignStrengths();
			_instance.assignWeaknesses();
		}
		return _instance;
	}

	public void assignStrengths() {
		strengths = new Elements[] {FireElem.getInstance(), ShadowElem.getInstance()};
	}

	public void assignWeaknesses() {
		weaknesses = new Elements[] {IceElem.getInstance(), AirElem.getInstance()};
	}

	public override bool isWeakAgainst(Elements e) {
		return _instance.getWeaknesses().Contains(e);
	}

	public override bool isStrongAgainst(Elements e) {
		return _instance.getStrengths().Contains(e);
	}
}