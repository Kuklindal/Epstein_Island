using UnityEngine;

public class FootSteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepSounds;
    public float stepDelay = 0.5f;

    private float timer;

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        bool isMoving = Mathf.Abs(moveX) > 0.1f || Mathf.Abs(moveZ) > 0.1f;

        // ❗ СНАЧАЛА проверяем остановку
        if (!isMoving)
        {
            timer = stepDelay; // ← ключевой момент
            return;            // ← ВАЖНО: выходим сразу
        }

        // движение
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            PlayStep();
            timer = stepDelay;
        }
    }

    void PlayStep()
    {
        if (footstepSounds.Length == 0) return;

        int index = Random.Range(0, footstepSounds.Length);
        audioSource.PlayOneShot(footstepSounds[index]);
    }
}