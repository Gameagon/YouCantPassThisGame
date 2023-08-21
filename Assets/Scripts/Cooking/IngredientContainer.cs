using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InteractionTarget;

public class IngredientContainer : MonoBehaviour
{
	protected Ingredient ingredient;
	public Transform point;

	public virtual Ingredient Release()
	{
		if (ingredient == null)
		{
			Debug.LogError("This container is empty");
			return null;
		}

		Ingredient temp = ingredient;
		ingredient.container = null;
		ingredient.transform.parent = null;
		ingredient = null;
		return temp;
	}

	public virtual void Fill(Ingredient _ingredient)
	{
		if (ingredient != null)
		{
			Debug.LogError("This container is already full with " + ingredient);
			return;
		}

		if(_ingredient.container) _ingredient.container.Release();

		ingredient = _ingredient;
		ingredient.container = this;
		ingredient.transform.SetParent(point);
		ingredient.transform.localPosition = Vector3.zero;
		ingredient.gameObject.layer = point.gameObject.layer;

	}

	public virtual void Fill(Interactor interactor)
	{
		Fill(interactor.GetComponent<IngredientContainer>().ingredient);
	}
}
