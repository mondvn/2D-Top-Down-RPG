using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeProjectile : MonoBehaviour
{
    [SerializeField] private float duration = 1f;
    [SerializeField] private AnimationCurve animCurve;
    [SerializeField] private float heightY = 3f;
    [SerializeField] private GameObject grapeProjectileShadow;

    private void Start()
    {
        GameObject grapeProjectileShadow =
            Instantiate(this.grapeProjectileShadow, transform.position + new Vector3(0, -.3f, 0), Quaternion.identity);

        Vector3 playerPos = PlayerController.Instance.transform.position;
        Vector3 grapeShadowStartPos = grapeProjectileShadow.transform.position;
        StartCoroutine(ProjectileCurveRoutine(transform.position, playerPos));
        StartCoroutine(MoveGrapeShadowRoutine(grapeProjectileShadow, grapeShadowStartPos, playerPos));
    }

    private IEnumerator ProjectileCurveRoutine(Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;

        while (timePassed < this.duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / this.duration;
            float heightT = this.animCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, this.heightY, heightT);

            transform.position = Vector2.Lerp(startPosition, endPosition, linearT) + new Vector2(0f, height);
            yield return null;
        }
        Destroy(gameObject);
    }

    private IEnumerator MoveGrapeShadowRoutine(GameObject grapeProjectileShadow, Vector3 startPosition, Vector3 endPosition)
    {
        float timePassed = 0f;

        while (timePassed < this.duration)
        {
            timePassed += Time.deltaTime;
            float linearT = timePassed / this.duration;

            grapeProjectileShadow.transform.position = Vector2.Lerp(startPosition, endPosition, linearT);
            yield return null;
        }
        Destroy(grapeProjectileShadow);
    }

}
