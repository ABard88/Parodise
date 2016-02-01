using UnityEngine;
using System.Collections;

public class CardModel : MonoBehaviour
{
	SpriteRenderer spriteRenderer;
	public Sprite[] faces;
	public Sprite cardback;
	public int cardIndex; // Example faces[cardIndex];
	public void ToggleFace(bool showFace)
	{
		if (showFace)
		{
			spriteRenderer.sprite = faces[cardIndex];
		}
		else
		{
			spriteRenderer.sprite = cardback;
		}
	}
	void Awake()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
}