using UnityEngine;
using UnityEngine.UIElements;

public class ObjectCollect : MonoBehaviour
{
    public Material MaterialOutline;

    private void Start()
    {
        ChangeOutlineObject(MaterialOutline, 0);
    }

    public void ChangeOutlineObject(Material mat, float scale)
    {
        mat.SetFloat("_Scale", scale);
    }
}
