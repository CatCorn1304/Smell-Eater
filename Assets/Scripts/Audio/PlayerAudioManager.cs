using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public PlayerController pc;
    public AudioSource sfx;
    public AudioClip[] sniffSounds, barkSounds, grassSounds, woodSounds;
    public Dictionary<string, AudioClip[]> randomClipList = new Dictionary<string, AudioClip[]>();
    public float maxDistTillPlaySound;
    public string groundType = "grass";
    public Vector3 lastPos;
    // Start is called before the first frame update
    void Start()
    {
        randomClipList.Add("sniff", sniffSounds);
        randomClipList.Add("bark", barkSounds);
        randomClipList.Add("grass", grassSounds);
        randomClipList.Add("wood", woodSounds);
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        groundType = pc.groundType;
        if (Vector3.Distance(transform.position,lastPos) > maxDistTillPlaySound)
        {
            PlayRandomClipFromList(groundType);
            lastPos = transform.position;
        }
    }

    public void PlayRandomClipFromList(string clipListName)
    {
        for (int i = 0; i < randomClipList[clipListName].Length; i++)
        {
            sfx.PlayOneShot(randomClipList[clipListName][Random.Range(0, randomClipList[clipListName].Length)]);
        }
    }
}
