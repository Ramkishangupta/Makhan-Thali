using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class OrderChecker : MonoBehaviour
{
    public static OrderChecker Instance { get; private set; }

    private List<string> requiredFoods;
    private HashSet<string> placedFoods = new HashSet<string>();

    public GameObject reactionPanel; // Reference to the UI Panel

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (OrderManager.Instance == null)
        {
            Debug.LogError("❌ OrderManager instance not found!");
            return;
        }

        requiredFoods = OrderManager.Instance.GetCurrentOrder();

        // Ensure Reaction Panel is hidden at the start
        if (reactionPanel != null)
        {
            reactionPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("❌ Reaction Panel is not assigned in OrderChecker!");
        }
    }

    public void AddFoodToPlate(string foodName)
    {
        placedFoods.Add(foodName);
    }

    public void CheckOrderCompletion()
    {
        bool isCorrect = true;

        foreach (string food in requiredFoods)
        {
            if (!placedFoods.Contains(food))
            {
                isCorrect = false;
                break;
            }
        }

        if (isCorrect)
        {
            Debug.Log("🎉 Order is correct!");
            PlayerPrefs.SetInt("OrderCorrect", 1);
        }
        else
        {
            Debug.Log("⚠ Order is incorrect!");
            PlayerPrefs.SetInt("OrderCorrect", 0);
        }

        PlayerPrefs.Save();

        // Show the Reaction Panel
        if (reactionPanel != null)
        {
            reactionPanel.SetActive(true);
        }
    }
}
