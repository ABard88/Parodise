using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[RequireComponent(typeof(DCardStack))]
public class DCardStackView : MonoBehaviour //Deckview needs to have deck
{
	DCardStack deck, remaining;
	Dictionary<int, CardView> fetchedCards;
	int lastCount;
	public Vector3 start;
	public float cardOffset;
	public bool faceUp=false;
	public GameObject cardPrefab;
	public void DToggle(int card, bool isFaceUp)
	{
		fetchedCards[card].IsFaceUp = isFaceUp;
	}
	void Awake()
	{
		fetchedCards = new Dictionary<int, CardView>();
		deck = GetComponent<DCardStack>();

		DShowCards();
		lastCount = deck.DCardCount;

		deck.CardRemoved += deck_CardRemoved; // for removing cards from gameobject
		deck.CardAdded += deck_CardAdded;
	}
	
	void deck_CardAdded(object sender, CardEventArgs e)
	{

		float co = cardOffset * deck.DCardCount;
		Vector3 temp = start + new Vector3(co + 2, 0f); // Gives it a temp position 
		DAddCard(temp, e.CardIndex, deck.DCardCount);
	}
	public void deck_CardRemoved(object sender, CardEventArgs e) // for removing cards from gameobject
	{
		if(fetchedCards.ContainsKey(e.CardIndex))
		{
			Destroy(fetchedCards[e.CardIndex].Card);
			fetchedCards.Remove(e.CardIndex);
		}
	}

	public void DShowCards()
	{
		int cardCount = 0;
		if (deck.HasCards)
		{
			foreach (int i in deck.DGetCards()) // player's personal deck
			{
				float co = cardOffset * cardCount;                
				Vector3 temp = start + new Vector3(co + 2, 0f); // Gives it a temp position 
				DAddCard(temp, i, cardCount);
				cardCount++;
			}
		}
	}

	public void DAddCard(Vector3 position, int cardIndex, int positionalIndex)
	{   
		
		if(fetchedCards.ContainsKey(cardIndex))
		{
			if(!faceUp)
			{
				CardModel model = fetchedCards[cardIndex].Card.GetComponent<CardModel>();
				model.ToggleFace(fetchedCards[cardIndex].IsFaceUp);
			}
			return;
		}
		GameObject cardCopy = (GameObject)Instantiate(cardPrefab); // Copies card
		cardCopy.transform.position = position;
		CardModel cardModel = cardCopy.GetComponent<CardModel>();
		cardModel.cardIndex = cardIndex;
		cardModel.ToggleFace(faceUp);//in editor in script CardStackView check FaceUp to see cards if unchecked face down
		SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();
		spriteRenderer.sortingOrder = positionalIndex;
		fetchedCards.Add(cardIndex, new CardView (cardCopy));
		cardCopy.transform.localScale=new Vector3(0.5f,0.5f,0);
	}
}
