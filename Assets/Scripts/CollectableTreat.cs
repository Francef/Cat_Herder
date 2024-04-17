using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableTreat : MonoBehaviour
{
    private int value = 1;
    private Coroutine regenerateDelay;
    private float waitTime = 10.0f;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Material boneMaterial;
    [SerializeField] private Material redBoneMaterial;
    private bool isNotCollectable = false;      // boolean to track if treat is able to be collected again
    void Start()
    {

    }

    void Update()
    {
        // rotate treat
        Vector3 rotation = Vector3.up * 180 * Time.deltaTime;
        transform.Rotate(rotation, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isNotCollectable)
        {
            Messenger<int>.Broadcast(GameEvent.TREAT_COLLECTED, value);
            // prevent treat increment during cooldown time
            isNotCollectable = true;
            // apply a red material to treat while it's not collectable
            meshRenderer.material = redBoneMaterial;
            regenerateDelay = StartCoroutine(CooldownTime(waitTime));
            
        }
    }
    IEnumerator CooldownTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        // player can now collect treat again
        isNotCollectable = false;
        // return bone to original material
        meshRenderer.material = boneMaterial;
    }
}
