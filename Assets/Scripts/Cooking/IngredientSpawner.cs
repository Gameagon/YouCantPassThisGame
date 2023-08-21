using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : IngredientContainer
{
	public Ingredient inPrefab;

	private void Start()
	{
		if (!ingredient) Respawn();
	}

	public override Ingredient Release()
	{
        if (ingredient == null) Debug.LogError("This container is empty");

        Ingredient temp = ingredient;
        ingredient.container = null;
        ingredient.transform.parent = null;
        ingredient = null;
        Respawn();
        return temp;
    }

    public void Respawn()
    {
        Fill(Instantiate(inPrefab));
    }
}
