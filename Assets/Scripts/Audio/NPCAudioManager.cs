using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAudioManager : MonoBehaviour
{
    public AudioSource oneShotClips;
    public AudioClip[] barkSounds;
    public Dictionary<string, AudioClip[]> randomClipList = new Dictionary<string, AudioClip[]>();
    // Start is called before the first frame update
    void Start()
    {
        randomClipList.Add("bark", barkSounds);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayRandomClipFromList(string clipListName)
    {
        for (int i = 0; i < randomClipList[clipListName].Length; i++)
        {
            oneShotClips.PlayOneShot(randomClipList[clipListName][Random.Range(0, randomClipList[clipListName].Length)]);
        }
    }
}
