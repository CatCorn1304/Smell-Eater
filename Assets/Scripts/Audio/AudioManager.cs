using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource source => GetComponent<AudioSource>();
    public AudioClip[] sniffSounds, barkSounds;
    public Dictionary<string, AudioClip[]> randomClipList = new Dictionary<string, AudioClip[]>();
    // Start is called before the first frame update
    void Start()
    {
        randomClipList.Add("sniff",sniffSounds);
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
            source.PlayOneShot(randomClipList[clipListName][Random.Range(0, randomClipList[clipListName].Length)]);
        }
    }
}
