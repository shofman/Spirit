using System.Linq;
using UnityEngine;

public class AirElem : Elements {
	/**
	 * Singleton pattern - we want only one instance of a mouse state per game
	 */
	protected static AirElem _instance;

	private AirElem() {
		
	}

	public void assignStrengths() {
		strengths = new Elements[] {JungleElem.getInstance(), WaterElem.getInstance()};
	}

	public void assignWeaknesses() {
		weaknesses = new Elements[] {CaveElem.getInstance(), ShadowElem.getInstance()};
	}

	public static Elements getInstance() {
		if (_instance == null) {
			_instance = new AirElem();
			_instance.assignStrengths();
			_instance.assignWeaknesses();
		}
		return _instance;
	}

	public override bool isWeakAgainst(Elements e) {
		return _instance.getWeaknesses().Contains(e);
	}

	public override bool isStrongAgainst(Elements e) {
		return _instance.getStrengths().Contains(e);
	}
}