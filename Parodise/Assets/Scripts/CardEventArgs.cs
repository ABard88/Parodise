using System; //used to remove cards from the Comp object

public class CardEventArgs: EventArgs
{
	public int CardIndex { get; private set; }
	public CardEventArgs(int cardIndex)
	{
		CardIndex = cardIndex;
	}
}