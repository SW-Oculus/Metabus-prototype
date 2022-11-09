using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour
{
    //asd
    [Tooltip("������Ʈ �̸�")]
    public string objName;
    bool objFocused;
    bool objPressed;

    void Awake()
    {
        objFocused = false;
        objPressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
        objFocused = false;
        objPressed = false;
    }

    public void ChangeColor()
    {
        if (objFocused) // raycast�� ���� ������Ʈ�� ��Ŀ�� �� ��: red
            GetComponent<MeshRenderer>().material.color = Color.red;
        else if (objPressed) // raycast�� ���� ������Ʈ�� Ŭ���� ��: blue
            GetComponent<MeshRenderer>().material.color = Color.blue;
        else // ������Ʈ�� �⺻��: white
            GetComponent<MeshRenderer>().material.color = Color.white;
    }

    public bool ObjFocused
    {
        get { return objFocused; }
        set { objFocused = value; }
    }

    public bool ObjPressed
    {
        get { return objPressed; }
        set { objPressed = value; }
    }
}
