using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Signal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Scenery") || collision.gameObject.CompareTag("Spawner"))
        {
            CreateObject createObject  = collision.GetComponent<CreateObject>();
            if (createObject != null)
            {
                createObject.Create();
            }
        }
    }
}
