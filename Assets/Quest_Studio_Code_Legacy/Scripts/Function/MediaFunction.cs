using UnityEngine;

namespace Quest_Studio
{
    public class MediaFunction
    {
        // Resize Media Object
        #region
        public static void ResizeMediaObject(float containerWidth, float containerHeight, RectTransform mediaObjectRT, float mediaObjectWidth, float mediaObjectHeight)
        {
            float widthRatio = containerWidth / mediaObjectWidth;
            float heightRatio = containerHeight / mediaObjectHeight;

            //Square image (All == width)
            if (mediaObjectWidth == mediaObjectHeight)
            {
                if (containerWidth <= containerHeight)
                {
                    mediaObjectRT.sizeDelta = new Vector2(containerWidth, containerWidth);
                }
                else if (containerWidth > containerHeight)
                {
                    mediaObjectRT.sizeDelta = new Vector2(containerHeight, containerHeight);
                }
            }

            //Landscape image
            else if (mediaObjectWidth > mediaObjectHeight)
            {
                float multipliedHeight = mediaObjectHeight * widthRatio;
                if (multipliedHeight > containerHeight)
                {
                    mediaObjectRT.sizeDelta = new Vector2(mediaObjectWidth * heightRatio, containerHeight);
                }
                else
                {
                    mediaObjectRT.sizeDelta = new Vector2(containerWidth, mediaObjectHeight * widthRatio);
                }
            }

            //Portrait image
            else if (mediaObjectWidth < mediaObjectHeight)
            {
                float multipliedWidth = mediaObjectWidth * heightRatio;
                if (multipliedWidth > containerWidth)
                {
                    mediaObjectRT.sizeDelta = new Vector2(containerWidth, mediaObjectHeight * widthRatio);
                }
                else
                {
                    mediaObjectRT.sizeDelta = new Vector2(mediaObjectWidth * heightRatio, containerHeight);
                }
            }
        }
        #endregion

        // Resize Media Object To Max
        #region
        public static void ResizeMediaObjectToMax(float containerWidth, float containerHeight, RectTransform mediaObjectRT, float mediaObjectWidth, float mediaObjectHeight)
        {
            float widthRatio = containerWidth / mediaObjectWidth;
            float heightRatio = containerHeight / mediaObjectHeight;

            //Square image (All == width)
            if (mediaObjectWidth == mediaObjectHeight)
            {
                if (containerWidth <= containerHeight)
                {
                    mediaObjectRT.sizeDelta = new Vector2(containerWidth, containerWidth);
                }
                else if (containerWidth > containerHeight)
                {
                    mediaObjectRT.sizeDelta = new Vector2(containerHeight, containerHeight);
                }
            }

            //Landscape image
            else if (mediaObjectWidth > mediaObjectHeight)
            {
                mediaObjectRT.sizeDelta = new Vector2(mediaObjectWidth * heightRatio, containerHeight);
            }

            //Portrait image
            else if (mediaObjectWidth < mediaObjectHeight)
            {
                mediaObjectRT.sizeDelta = new Vector2(containerWidth, mediaObjectHeight * widthRatio);
            }
        }
        #endregion

        // Create Render Texture
        #region
        public static RenderTexture CreateRenderTexture(float containerWidth, float containerHeight, float mediaObjectWidth, float mediaObjectHeight)
        {
            RenderTexture renderTexture = new RenderTexture(0, 0, 0, RenderTextureFormat.ARGB32);
            float widthRatio = containerWidth / mediaObjectWidth;
            float heightRatio = containerHeight / mediaObjectHeight;
            float multipliedWidth = mediaObjectWidth * heightRatio;
            float multipliedHeight = mediaObjectHeight * widthRatio;

            //Square image (All == width)
            if (mediaObjectWidth == mediaObjectHeight)
            {
                if (containerWidth <= containerHeight)
                {
                    renderTexture = new RenderTexture((int)containerWidth, (int)containerWidth, 0, RenderTextureFormat.ARGB32);
                }
                else if (containerWidth > containerHeight)
                {
                    renderTexture = new RenderTexture((int)containerHeight, (int)containerHeight, 0, RenderTextureFormat.ARGB32);
                }
            }

            //Landscape image
            else if (mediaObjectWidth > mediaObjectHeight)
            {
                if (multipliedHeight > containerHeight)
                {
                    renderTexture = new RenderTexture((int)multipliedWidth, (int)containerHeight, 0, RenderTextureFormat.ARGB32);
                }
                else
                {
                    renderTexture = new RenderTexture((int)containerWidth, (int)multipliedHeight, 0, RenderTextureFormat.ARGB32);
                }
            }

            //Portrait image
            else if (mediaObjectWidth < mediaObjectHeight)
            {
                if (multipliedWidth > containerWidth)
                {
                    renderTexture = new RenderTexture((int)containerWidth, (int)multipliedHeight, 0, RenderTextureFormat.ARGB32);
                }
                else
                {
                    renderTexture = new RenderTexture((int)multipliedWidth, (int)containerHeight, 0, RenderTextureFormat.ARGB32);
                }
            }
            return renderTexture;
        }
        #endregion
    }
}