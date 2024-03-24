using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameBase : MonoBehaviour
{
    public event Action actionPerformedEvent;
    public event Action closedEvent;

    //Starts the minigame
    public virtual void Open()
    {
    }

    //Override Update() to implement your minigame. Don't forget to call base.Update()
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }

    //Triggers execution of an action linked to this minigame. E.g. adding a new wood log in tree cutting minigame. Call this when player finishes the minigame
    protected void PerformAction()
    {
        actionPerformedEvent?.Invoke();
    }

    //Close the minigame
    protected void Close()
    {
        closedEvent?.Invoke();
        Destroy(gameObject);
    }

    //Helper coroutines. These could be replaced with animation calls

    //Rotates a transform
    protected IEnumerator RotationCoroutine(Transform rect, Vector3 target, float speed, Action onEnd)
    {
        Quaternion targetRotation = Quaternion.Euler(target);
        while (targetRotation != rect.rotation)
        {
            rect.rotation = Quaternion.RotateTowards(rect.rotation, targetRotation, speed * Time.deltaTime);
            yield return null;
        }
        onEnd();
    }

    protected enum FadeDirection
    {
        FadeOut,
        FadeIn
    }
    //Fades in/out a ui image
    protected IEnumerator ImageFadeCoroutine(Image image, float duration, FadeDirection direction, Action onEnd)
    {
        image.CrossFadeAlpha(direction == FadeDirection.FadeOut ? 0 : 1, duration, false);
        yield return new WaitForSeconds(duration);
        onEnd();
    }
}
