using UnityEngine;

public class BasketClick : MonoBehaviour
{
    public BasketManager basket;

    void OnMouseDown()
    {
        basket.OpenBasket();
    }
}