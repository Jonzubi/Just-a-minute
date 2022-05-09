using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleUpAndDown : MonoBehaviour
{
    public float scaleFactor = 0.1f;
    float time = 0;

    private void Update() {
        time += Time.deltaTime;

        float auxSign = (int)time % 2 == 0 ? 1 : -1;
        float scale = scaleFactor * auxSign;
        // Scale up and down the object
        transform.localScale = new Vector3(
            transform.localScale.x + scale,
            transform.localScale.y + scale,
            transform.localScale.z + scale
        );

    }
}
