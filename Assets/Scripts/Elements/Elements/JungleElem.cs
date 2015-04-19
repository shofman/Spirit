using System.Linq;
using UnityEngine;

public class JungleElem : Elements {
	/**
	 * Singleton pattern - we want only one instance of a cave element per game
	 */
	protected static JungleElem _instance;

	private JungleElem() {

	}

	public static Elements getInstance() {
		if (_instance == null) {
			_instance = new JungleElem();
			_instance.assignStrengths();
			_instance.assignWeaknesses();
		}
		return _instance;
	}

	public void assignStrengths() {
		strengths = new Elements[] {CaveElem.getInstance(), EtherealElem.getInstance()};
	}

	public void assignWeaknesses() {
		weaknesses = new Elements[] {AirElem.getInstance(), FireElem.getInstance()};
	}

	public override bool isWeakAgainst(Elements e) {
		return _instance.getWeaknesses().Contains(e);
	}

	public override bool isStrongAgainst(Elements e) {
		return _instance.getStrengths().Contains(e);
	}
}