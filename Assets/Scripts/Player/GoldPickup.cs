using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPickup : MonoBehaviour
{

	public int value;
	public MoneyManager MM;

	// Start is called before the first frame update
	void Start()
	{
		MM = FindObjectOfType<MoneyManager>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	// when player walks into coin, it picks up
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.name == "Player")
		{
			MM.AddMoney(value);
			Destroy(gameObject);
		}
	}
}
