using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateMouse : MonoBehaviour
{
    public float speed = 5f;

    bool checkDown = false;

    public Toggle toggle;

    [SerializeField] float offset = -90f;

    void Update()
    {
        if (checkDown)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

            float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);
        }
    }

    private void OnMouseDown()
    {
        if (toggle.isOn)
        {
            checkDown = true;
        }
    }

    private void OnMouseUp()
    {
        checkDown = false;
    }
}
