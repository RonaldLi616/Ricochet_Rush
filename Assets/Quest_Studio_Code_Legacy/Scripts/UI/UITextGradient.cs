using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Quest_Studio
{
    [AddComponentMenu("UI/Effects/UITextGradient")]
    public class UITextGradient : BaseMeshEffect
    {
        [SerializeField]
        private bool isHorizontal = true;
        [SerializeField]
        private Color32 topColor = Color.white;
        public Color32 GetTopColor()
        {
            return topColor;
        }
        public void SetTopColor(Color32 color)
        {
            topColor = color;
        }
        [SerializeField]
        private Color32 bottomColor = Color.black;
        public Color32 GetBottomColor()
        {
            return bottomColor;
        }
        public void SetBottomColor(Color32 color)
        {
            bottomColor = color;
        }
        public override void ModifyMesh(VertexHelper vh)
        {
            if (!this.IsActive())
                return;
            List<UIVertex> vertexList = new List<UIVertex>();
            vh.GetUIVertexStream(vertexList);
            ModifyVertices(vertexList);
            vh.Clear();
            vh.AddUIVertexTriangleStream(vertexList);
        }
        public void ModifyVertices(List<UIVertex> vertexList)
        {
            int count = vertexList.Count;
            if (isHorizontal)
            {
                // Do Horizontal
                float bottomY = vertexList[0].position.y;
                float topY = vertexList[0].position.y;
                for (int i = 1; i < count; i++)
                {
                    float y = vertexList[i].position.y;
                    if (y > topY)
                    {
                        topY = y;
                    }
                    else if (y < bottomY)
                    {
                        bottomY = y;
                    }
                }
                float uiElementHeight = topY - bottomY;
                for (int i = 0; i < count; i++)
                {
                    UIVertex uiVertex = vertexList[i];
                    uiVertex.color = Color32.Lerp(bottomColor, topColor, (uiVertex.position.y - bottomY) / uiElementHeight);
                    vertexList[i] = uiVertex;
                }
            }
            else
            {
                // Do Vertical
                float rightX = vertexList[0].position.x;
                float leftX = vertexList[0].position.x;
                for (int i = 1; i < count; i++)
                {
                    float x = vertexList[i].position.x;
                    if (x > rightX)
                    {
                        rightX = x;
                    }
                    else if (x < leftX)
                    {
                        leftX = x;
                    }
                }
                float uiElementWidth = rightX - leftX;
                for (int i = 0; i < count; i++)
                {
                    UIVertex uiVertex = vertexList[i];
                    uiVertex.color = Color32.Lerp(bottomColor, topColor, (uiVertex.position.x - leftX) / uiElementWidth);
                    vertexList[i] = uiVertex;
                }
            }
        }
    }
}