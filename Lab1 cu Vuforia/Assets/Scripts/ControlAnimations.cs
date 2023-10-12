using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnimations : MonoBehaviour
{
    public GameObject target1;
    public GameObject target2;

    public AudioClip attackSound;
    private AudioSource target1AudioSource;
    private AudioSource target2AudioSource;

    public float minimumDistance = 0.15f;
    public float soundDelay = 0.8f;

    private float lastTimePlayedTarget1 = 0f;
    private float lastTimePlayedTarget2 = 0f;

    void Start()
    {
        target1AudioSource = target1.GetComponent<AudioSource>();
        target2AudioSource = target2.GetComponent<AudioSource>();
    }

    void Update()
    {
        float distance = Vector3.Distance(target1.transform.position, target2.transform.position);

        if (distance <= minimumDistance)
        {
            PlayAttackSound(target1, ref lastTimePlayedTarget1);
            PlayAttackSound(target2, ref lastTimePlayedTarget2);

            target1.GetComponent<Animator>().SetBool("isWithinAttackRange", true);
            target2.GetComponent<Animator>().SetBool("isWithinAttackRange", true);
            Debug.Log("Within attack range");
        }
        else
        {
            target1.GetComponent<Animator>().SetBool("isWithinAttackRange", false);
            target2.GetComponent<Animator>().SetBool("isWithinAttackRange", false);
            Debug.Log("Not within attack range");
        }
    }

    private void PlayAttackSound(GameObject target, ref float lastTimePlayed)
    {
        if (Time.time - lastTimePlayed >= soundDelay)
        {
            AudioSource audioSource = target.GetComponent<AudioSource>();
            if (audioSource && !audioSource.isPlaying)
            {
                audioSource.clip = attackSound;
                audioSource.Play();
                lastTimePlayed = Time.time;
            }
        }
    }
}
