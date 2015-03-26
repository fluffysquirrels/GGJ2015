public class SafeSquareHandler
{
	private static int currentSafeSquare = 0;

	public static int CurrentSafeSquare
	{
		get
		{
			return currentSafeSquare;
		}
		set
		{
			currentSafeSquare = value;
		}
	}
}
