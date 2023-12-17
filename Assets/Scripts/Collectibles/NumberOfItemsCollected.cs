using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberOfItemsCollected : MonoBehaviour
{
    public GameManager gm;
    public Animator open;
    public Animator endopen;
    public AudioSource source1;
    public AudioClip first;
    public bool open1 = false;
    public bool open2 = false;
    public GameObject gatethought;

    private void Update()
    {
        if (gm.items >= 1 && open1 == false)
        {
            open.SetTrigger("Smell");
            open1 = true;
            source1.PlayOneShot(first);
            StartCoroutine(think());
        }

        if (gm.items >= 6 && open2 == false)
        {
            endopen.SetTrigger("end");
            open2 = true;
            source1.PlayOneShot(first);
            StartCoroutine(think());
        }
    }

    private IEnumerator think()
    {
        gatethought.SetActive(true);
        yield return new WaitForSeconds(5f);
        gatethought.SetActive(false);
    }
}
