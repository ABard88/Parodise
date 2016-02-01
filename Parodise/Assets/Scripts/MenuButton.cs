using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour 
{
	public void Sabbac(string Sabbac)
	{
		Application.LoadLevel (Sabbac);
	}
	
	public void SelectDeck (string SelectDeck)
	{
		Application.LoadLevel (SelectDeck);
	}
	public void ViewDeck (string ViewDeck)
	{
		Application.LoadLevel (ViewDeck);
	}
	public void DeckSelectRandom (string DeckSelectRandom)
	{
		Application.LoadLevel (DeckSelectRandom);
	}
	public void StartScreen (string StartScreen)
	{
		Application.LoadLevel (StartScreen);
	}
	public void CleanGwent (string CleanGwent)
	{
		Application.LoadLevel (CleanGwent);
	}
	public void CustomizeDeck (string CustomizeDeck)
	{
		Application.LoadLevel (CustomizeDeck);
	}

	public void Declare (string Declare)
	{
		Application.LoadLevel (Declare);
	}
	public void PolCamp (string PolCamp)
	{
		Application.LoadLevel (PolCamp);
	}
	public void FanCamp (string FanCamp)
	{
		Application.LoadLevel (FanCamp);
	}
	public void HistCamp (string HistCamp)
	{
		Application.LoadLevel (HistCamp);
	}
	public void CartCamp (string CartCamp)
	{
		Application.LoadLevel (CartCamp);
	}
	//pol campaign
	public void Gwent3RoundL4 (string Gwent3RoundL4)
	{
		Application.LoadLevel (Gwent3RoundL4);
	}
	
	//fantasy
	public void GwentFant(string GwentFant)
	{
		Application.LoadLevel (GwentFant);
	}
	public void GwentHist(string GwentHist)
	{
		Application.LoadLevel (GwentHist);
	}
	public void GwentCart(string GwentCart)
	{
		Application.LoadLevel (GwentCart);
	}

	public void Exit (string Exit)
	{
		Application.Quit ();
	}


}
