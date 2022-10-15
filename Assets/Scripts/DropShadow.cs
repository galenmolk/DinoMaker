using System.Collections.Generic;
 
namespace UnityEngine.UI
{
    /// <summary>
    /// Source: https://pastebin.com/YGeaZwG3
    /// </summary>
    [AddComponentMenu("UI/Effects/DropShadow", 14)]
    public class DropShadow : BaseMeshEffect
    {
        [SerializeField] private Color shadowColor = new(0f, 0f, 0f, 0.5f);
        [SerializeField] private Vector2 shadowDistance = new(1f, -1f);
        [SerializeField] private bool useGraphicAlpha = true;
        [SerializeField] private int iterations = 5;
        [SerializeField] private Vector2 shadowSpread = Vector2.one;
 
        protected DropShadow() {}
 
#if UNITY_EDITOR
        protected override void OnValidate()
        {
            EffectDistance = shadowDistance;
            base.OnValidate();
        }
#endif
 
        public Color EffectColor
        {
            get => shadowColor;
            set
            {
                shadowColor = value;
                if (graphic != null)
                {
                    graphic.SetVerticesDirty();
                }
            }
        }
 
        public Vector2 ShadowSpread
        {
            get => shadowSpread;
            set
            {
                shadowSpread = value;
                if (graphic != null)
                {
                    graphic.SetVerticesDirty();
                }
            }
        }
 
        public int Iterations
        {
            get => iterations;
            set
            {
                iterations = value;
                if (graphic != null)
                {
                    graphic.SetVerticesDirty();
                }
            }
        }
 
        public Vector2 EffectDistance
        {
            get => shadowDistance;
            set
            {
                shadowDistance = value;
 
                if (graphic != null)
                {
                    graphic.SetVerticesDirty();
                }
            }
        }
 
        public bool UseGraphicAlpha
        {
            get => useGraphicAlpha;
            set
            {
                useGraphicAlpha = value;
                if (graphic != null)
                {
                    graphic.SetVerticesDirty();
                }
            }
        }
 
        private void DropShadowEffect(ICollection<UIVertex> verts)
        {
            int count = verts.Count;
 
            List<UIVertex> vertsCopy = new List<UIVertex>(verts);
            verts.Clear();
 
            for (int i = 0; i < iterations; i++)
            {
                for (int v = 0; v < count; v++)
                {
                    UIVertex vt = vertsCopy[v];
                    Vector3 position = vt.position;
                    float fac = i/(float)iterations;
                    position.x *= 1 + shadowSpread.x * fac * 0.01f;
                    position.y *= 1 + shadowSpread.y * fac * 0.01f;
                    position.x += shadowDistance.x * fac;
                    position.y += shadowDistance.y * fac;
                    vt.position = position;
                    Color32 color = shadowColor;
                    color.a = (byte)(color.a /(float)iterations);
                    vt.color = color;
                    verts.Add(vt);
                }
            }
 
            for (int i = 0, copyCount = vertsCopy.Count; i < copyCount; i++)
            {
                verts.Add(vertsCopy[i]);
            }
        }
 
        public override void ModifyMesh(VertexHelper vh)
        {
            if (!IsActive())
            {
                return;
            }

            List<UIVertex> output = new List<UIVertex>();
            vh.GetUIVertexStream(output);
 
            DropShadowEffect(output);
 
            vh.Clear();
            vh.AddUIVertexTriangleStream(output);
        }
    }
}
