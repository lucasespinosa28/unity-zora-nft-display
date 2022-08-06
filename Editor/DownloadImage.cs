#nullable enable
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class DownloadImage
{
    public IEnumerator FromUrl(string image, GameObject gameObject)
    {
        Debug.Log(ipfsUrl.isValid(image));
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(ipfsUrl.isValid(image)))
        {
            yield return request.SendWebRequest();
            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError($"Error: {request.error}");
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError($"Error: {request.error}");
                    break;
                case UnityWebRequest.Result.Success:
                    var texture = DownloadHandlerTexture.GetContent(request);

                    if (gameObject.TryGetComponent<Image>(out var hasImage))
                    {
                        gameObject.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(texture.width/10, texture.height/10);
                    }
                    if (gameObject.TryGetComponent<SpriteRenderer>(out var hasSprite))
                    {
                        gameObject.GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width/10, texture.height/10), Vector2.zero);
                    }
                    if (gameObject.TryGetComponent<Renderer>(out var hasMaterial))
                    {
                        Renderer renderer = gameObject.GetComponent<Renderer>();
                        Material uniqueMaterial = renderer.material;
                        uniqueMaterial.mainTexture = texture;
                    }
                    break;
            }
        }
    }

}
