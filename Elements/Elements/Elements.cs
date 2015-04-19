using System.Linq;

public abstract class Elements {


	protected Elements[] weaknesses;
	protected Elements[] strengths;

	public Elements[] getStrengths() {
		return strengths;
	}

	public Elements[] getWeaknesses() {
		return weaknesses;
	}

	public abstract bool isWeakAgainst(Elements e);
	public abstract bool isStrongAgainst(Elements e);
}