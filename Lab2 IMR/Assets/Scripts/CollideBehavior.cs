using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CollideBehavior : MonoBehaviour
{
    [Header("Throw Effects")]
    public ParticleSystem throwParticlesPrefab;
    public AudioClip throwSound;
    public AudioClip throwSound2;

    [Header("Collision Effects")]
    public ParticleSystem collisionParticlesPrefab;
    public AudioClip collisionSound;

    [Header("BestScore Effects")]
    public AudioClip newBestScoreSound;

    private AudioSource audioSource;


    private Vector3 startingPosition;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectExited.AddListener(HandleRelease);
        grabInteractable.selectEntered.AddListener(HandleGrab);
    }

    private void OnDisable()
    {
        var grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectExited.RemoveListener(HandleRelease);
    }

    private void HandleRelease(SelectExitEventArgs args)
    {
        if (!args.isCanceled)
        {
            startingPosition = transform.position;

            if (throwParticlesPrefab)
            {
                ParticleSystem throwEffect = Instantiate(throwParticlesPrefab, transform.position, Quaternion.identity, transform);
                Destroy(throwEffect.gameObject, throwEffect.main.duration);
            }
            if (throwSound && audioSource)
            {
                if (Random.Range(0, 2) == 0)
                    audioSource.PlayOneShot(throwSound);
                else
                    audioSource.PlayOneShot(throwSound2);
            }
        }
    }

    private void HandleGrab(SelectEnterEventArgs args)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            float distanceTravelled = Vector3.Distance(startingPosition, transform.position);
            int currentHitScore = Mathf.CeilToInt(distanceTravelled * 100);
            // update the score
            KeepScore.Score += currentHitScore;

            // update the best score
            if (KeepScore.bestScore < currentHitScore)
            {
                KeepScore.bestScore = currentHitScore;
                audioSource.PlayOneShot(newBestScoreSound);
            }
            
            // play the collision particles
            if (collisionParticlesPrefab)
            {
                ParticleSystem collisionEffect = Instantiate(collisionParticlesPrefab, collision.contacts[0].point, Quaternion.identity);
                Destroy(collisionEffect.gameObject, collisionEffect.main.duration);
            }
            
            // play the collision sound
            if (collisionSound && audioSource)
            {
                audioSource.PlayOneShot(collisionSound);
            }
            
            // freeze the object
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
