using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FadeMaterial : MonoBehaviour
{
    public List<GameObject> blockingObjects, objectsToFadeBack;
    public LayerMask fadeLayer;
    public GameObject player;
    public float fadeSpeed;
    bool blocking;
    public float targetAlpha;
    public float raycastDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetBlockingMaterial();
        Fade();
    }
    void GetBlockingMaterial()
    {
        //raycast from player to camera, anything blocking is considered a blocking object and added to the list
        Transform cameraPos = Camera.main.gameObject.transform;
        RaycastHit hit;
        Debug.DrawRay(cameraPos.position, (player.transform.position - cameraPos.position) * 1000, Color.green);
        //objects are only considered blocking if they're on the "Fade" layer
        if (Physics.Raycast(cameraPos.position, player.transform.position - cameraPos.position, out hit, 1000, fadeLayer))
        {
            int endOfList = blockingObjects.Count - 1;
            //if list is empty, or currently blocking object is not at the end of the list, add it to the list
            if (blockingObjects.Count == 0 || blockingObjects.Count > 0 && blockingObjects[endOfList] != hit.collider.gameObject)
            {
                blockingObjects.Add(hit.collider.gameObject);
            }
        }
        else
        {
            //if nothing is blocking the player
            //but there are still blocking objects
            if (blockingObjects.Count > 0)
            {
                for (int i = 0; i < blockingObjects.Count; i++)
                {
                    //add them to objects to fade back
                    objectsToFadeBack.Add(blockingObjects[i]);
                    blockingObjects.Remove(blockingObjects[i]);
                }
            }
        }
    }
    void Fade()
    {
        //fade blocking objects to half opacity
        if (blockingObjects.Count > 0)
        {
            for (int i = 0; i < blockingObjects.Count; i++)
            {
                blockingObjects[i].GetComponent<MeshRenderer>().material.color = LerpAlpha(blockingObjects[i], 0.5f);
            }
        }
        //fade objects to fade back back to full opacity
        if (objectsToFadeBack.Count > 0)
        {
            for (int i = 0; i < objectsToFadeBack.Count; i++)
            {
                objectsToFadeBack[i].GetComponent<MeshRenderer>().material.color = LerpAlpha(objectsToFadeBack[i], 1);
                //print(objectsToFadeBack[i].GetComponent<MeshRenderer>().material.color.a);
                //if an object to fade back is at full opacity
                if (objectsToFadeBack[i].GetComponent<MeshRenderer>().material.color.a >= 0.99f)
                {
                    //remove it from list
                    objectsToFadeBack.Remove(objectsToFadeBack[i]);
                }
            }
        }
    }
    Color LerpAlpha(GameObject gameObject, float alpha)
    {
        //interpolate between current material color and current material color with changed opacity, at fadespeed
        Color materialColor = gameObject.GetComponent<MeshRenderer>().material.color;
        return Color.Lerp(materialColor, new Color(materialColor.r, materialColor.g, materialColor.b, alpha), Time.deltaTime * fadeSpeed);
    }
}
