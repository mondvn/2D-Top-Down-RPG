using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;
    private float waitToLoadTime = 1f;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            UIFade.Instance.FadeToBlack();
            SceneManagement.Instance.SetTransitionName(this.sceneTransitionName);
            StartCoroutine(LoadScreenRoutine());
        }
    }

    private IEnumerator LoadScreenRoutine()
    {
        yield return new WaitForSeconds(this.waitToLoadTime);
        SceneManager.LoadScene(this.sceneToLoad);
    }
}
