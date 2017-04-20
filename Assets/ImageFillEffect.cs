using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// UIのImageコンポーネントに追加してこれをつけると、Fillっぽくできます
public class ImageFillEffect : UIBehaviour, IMeshModifier
{

    [SerializeField, Range(0, 1)]
    private float _fillAmount = 1.0f;

    private List<UIVertex> vertexList = new List<UIVertex>();

    public float fillAmount
    {
        get { return _fillAmount; }
        set
        {
            _fillAmount = value;
            if (cacheGraphic != null)
            {
                cacheGraphic.SetVerticesDirty();
            }
        }
    }

    private RectTransform cacheRectTransform;
    private Graphic cacheGraphic;

    void Awake()
    {
        base.Awake();
        cacheRectTransform = gameObject.GetComponent<RectTransform>();
        cacheGraphic = gameObject.GetComponent<Graphic>();
    }

#if UNITY_EDITOR
    public new void OnValidate()
    {
        base.OnValidate();

        var graphics = base.GetComponent<Graphic>();
        if (graphics != null)
        {
            graphics.SetVerticesDirty();
        }
    }
#endif

    public void ModifyMesh(Mesh mesh) { }
    public void ModifyMesh(VertexHelper verts)
    {
        if (!this.IsActive())
        {
            return;
        }
        vertexList.Clear();
        verts.GetUIVertexStream(vertexList);

        ModifyVertices(vertexList);

        verts.Clear();
        verts.AddUIVertexTriangleStream(vertexList);
    }


    void ModifyVertices(List<UIVertex> vertexList)
    {
        if (cacheRectTransform == null)
        {
            cacheRectTransform = gameObject.GetComponent<RectTransform>();
        }
        float maxX = cacheRectTransform.rect.xMin + cacheRectTransform.rect.width * _fillAmount;
        for (int i = 0, vertexListCount = vertexList.Count; i < vertexListCount; ++i)
        {
            var element = vertexList[i];
            float x = Mathf.Min(element.position.x, maxX);
            element.position = new Vector3(x, vertexList[i].position.y, vertexList[i].position.z);
            vertexList[i] = element;
        }
    }

}