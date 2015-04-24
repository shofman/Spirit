using System.Linq;
using UnityEngine;

public abstract class Elements {
	protected Color elementColor;
	protected Elements[] weaknesses;
	protected Elements[] strengths;

	public Elements[] getStrengths() {
		return strengths;
	}

	public Elements[] getWeaknesses() {
		return weaknesses;
	}

	public Color getColor() {
        return elementColor;
    }

	public abstract bool isWeakAgainst(Elements e);
	public abstract bool isStrongAgainst(Elements e);
}