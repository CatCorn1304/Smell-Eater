using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SniffController : MonoBehaviour
{
    public Image itemIcon;
    public GameObject sniffCutscene;
    public GameObject collectibleManager;
    public List<GameObject> collectibles;
    public GameObject closestCollectible;
    public float closestDistance;
    public float collectDistance;
    public float maxDistance;
    float sniffTimer;
    public float sniffLength;
    public bool sniffing;
    // Start is called before the first frame update
    void Start()
    {
        print(collectibleManager.GetComponent<CollectiblesManager>().collectibles.Length);
        for (int i = 0; i < collectibleManager.GetComponent<CollectiblesManager>().collectibles.Length; i++)
        {
            collectibles.Add(collectibleManager.GetComponent<CollectiblesManager>().collectibles[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
        FindClosestCollectible();
    }
    void Timer()
    {
        if (sniffTimer > 0)
        {
            sniffing = true;
            sniffTimer -= Time.deltaTime;
        }
        else
        {
            sniffing = false;
        }
    }
    void FindClosestCollectible()
    {
        float closestDist = float.MaxValue;

        foreach (GameObject collectible in collectibles)
        {
            float distance = Vector3.Distance(collectible.transform.position, transform.position);

            if (distance < closestDist)
            {
                closestDist = distance;
                closestCollectible = collectible;
            }
        }

        if (closestCollectible != null)
        {
            closestDistance = Vector3.Distance(closestCollectible.transform.position, transform.position);
        }
    }
    public void Sniff()
    {
        sniffTimer = sniffLength;
        if (closestCollectible != null)
        {
            if (closestDistance < collectDistance && closestCollectible.GetComponent<ItemPickup>())
            {
                itemIcon.sprite = closestCollectible.GetComponent<ItemPickup>().Item.icon;
                sniffCutscene.SetActive(true);
                closestCollectible.GetComponent<ItemPickup>().Pickup();
                collectibles.Remove(closestCollectible);
                closestCollectible = null;
            }
        }    
    }
}
