using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    public Image characterImage;
    public TMP_Text orderText;
    public Button letsCookButton;

    [System.Serializable]
    public class Order
    {
        public string characterName;
        public Sprite characterSprite;
        public string[] foodItems;
    }

    public List<Order> orders;
    private int currentOrderIndex = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentOrderIndex = PlayerPrefs.GetInt("CurrentOrderIndex", 0);
        ShowOrder();
    }

    private void ShowOrder()
    {
        if (currentOrderIndex < orders.Count)
        {
            Order currentOrder = orders[currentOrderIndex];
            characterImage.sprite = currentOrder.characterSprite;
            StartCoroutine(TypeOrderText($"Maiyya, aaj{string.Join(" - ", currentOrder.foodItems)} banayo naa"));

            SaveCurrentOrder(currentOrder);
        }
        else
        {
            orderText.text = "All Orders Completed!";
            letsCookButton.gameObject.SetActive(false);
        }
    }

    private IEnumerator TypeOrderText(string text)
    {
        orderText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            orderText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        letsCookButton.gameObject.SetActive(true);
    }

    private void SaveCurrentOrder(Order currentOrder)
    {
        PlayerPrefs.SetString("CurrentCharacter", currentOrder.characterName);
        PlayerPrefs.SetInt("FoodItemCount", currentOrder.foodItems.Length);

        for (int i = 0; i < currentOrder.foodItems.Length; i++)
        {
            PlayerPrefs.SetString($"OrderItem{i}", currentOrder.foodItems[i]);
        }

        PlayerPrefs.SetInt("CurrentOrderIndex", currentOrderIndex);
        PlayerPrefs.Save();
    }

    public void GoToFoodKitchen()
    {
        SceneManager.LoadScene("FoodKitchen");
    }

    public void NextOrder()
    {
        if (currentOrderIndex < orders.Count - 1)
        {
            currentOrderIndex++;
            PlayerPrefs.SetInt("CurrentOrderIndex", currentOrderIndex);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Order"); 
        }
    }

    public List<string> GetCurrentOrder()
    {
        List<string> currentFoodItems = new List<string>();
        int count = PlayerPrefs.GetInt("FoodItemCount", 0);

        for (int i = 0; i < count; i++)
        {
            currentFoodItems.Add(PlayerPrefs.GetString($"OrderItem{i}", ""));
        }

        return currentFoodItems;
    }

}