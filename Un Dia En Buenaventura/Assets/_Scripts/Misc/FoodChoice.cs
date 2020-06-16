using UnityEngine;

public class FoodChoice : MonoBehaviour
{    
    public void SetFoodName(string foodName)
    {
        PlayerPrefs.SetString("food_name", foodName);
    }
}