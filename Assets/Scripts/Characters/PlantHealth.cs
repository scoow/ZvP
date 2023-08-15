using System.Collections;
using UnityEngine;

public class PlantHealth : Health
{
    private void Start()
    {
        dyingEvent.AddListener(onDie);
    }

    private void OnDisable()
    {
        dyingEvent?.RemoveListener(onDie);
    }

    private void onDie()
    {
        dyingEvent.RemoveListener(onDie);
        StartCoroutine(WaitForDeadStateAnimation());
    }

    private IEnumerator WaitForDeadStateAnimation()
    {
        yield return new WaitForSeconds(2.5f);
        Destroy(gameObject);
    }    
}
