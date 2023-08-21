using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public IngredientContainer container;

    public void GetPickedUp(Interactor interactor)
    {
        interactor.GetComponent<IngredientContainer>().Fill(this);
    }
}
