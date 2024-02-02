using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemCollector : MonoBehaviour
{
    private int fruits = 0;
    

    [SerializeField] private TextMeshProUGUI fruitsText;
    [SerializeField] private AudioSource collectionSoundEffect;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectables"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            fruits++;
            

            fruitsText.text = "Fruits: " + fruits;
            
            
        }
    }
}
