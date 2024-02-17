using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    //private int numberofFruits = 0;
    

    [SerializeField] private TextMeshProUGUI fruitsText;
    [SerializeField] private AudioSource collectionSoundEffect;

    private void Start()
    {
        fruitsText.text = PlayerManager.numberOfFruits.ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectables"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            PlayerManager.numberOfFruits++;
            PlayerPrefs.SetInt("NumberOfFruits", PlayerManager.numberOfFruits);
            //fruits++;
            //fruitsText.text = "Fruits: " + fruits;
            UpdateFruitsText();
            PlayerPrefs.DeleteAll(); //reset all the prefab
            

        }
    }
    private void UpdateFruitsText()
    {
        fruitsText.text = PlayerManager.numberOfFruits.ToString();
    }
    
}
