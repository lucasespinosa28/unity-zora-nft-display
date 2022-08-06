using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ZoraNft : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public NFTMetadata.Token metadata;
    [SerializeField] public string address;
    [SerializeField] public string id;
    void Start()
    {
        StartCoroutine(GetRequest(address, id));
    }
    public void PreviewNft()
    {
        StartCoroutine(GetRequest(address, id));
    }
    private IEnumerator GetRequest(string contract, string id)
    {
        string details = "{token(token: {address: \"$_address\", tokenId: \"$_id\"}) {\r\n    token {\r\n      image {\r\n        mediaEncoding {\r\n          ... on ImageEncodingTypes {\r\n            large\r\n            poster\r\n            thumbnail\r\n            original\r\n          }\r\n          ... on VideoEncodingTypes {\r\n            large\r\n            poster\r\n            thumbnail\r\n            preview\r\n          }\r\n          ... on AudioEncodingTypes {\r\n            large\r\n            original\r\n          }\r\n        }\r\n        url\r\n        mimeType\r\n      }\r\n      description\r\n      metadata\r\n      owner\r\n    }\r\n  }}";
        string jsonData = JsonConvert.SerializeObject(new { query = details.Replace("$_address", contract).Replace("$_id", id) });
        Debug.Log(jsonData);
        byte[] postData = Encoding.ASCII.GetBytes(jsonData);
        Debug.Log(postData.ToString());
        using (UnityWebRequest request = UnityWebRequest.Post("https://api.zora.co/graphql", UnityWebRequest.kHttpVerbPOST))
        {
            request.uploadHandler = new UploadHandlerRaw(postData);
            request.SetRequestHeader("Content-Type", "application/json");
            yield return request.SendWebRequest();

            switch (request.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError($"Error: {request.error}");
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError($"HTTP Error: {request.error}");
                    break;
                case UnityWebRequest.Result.Success:
                    var text = request.downloadHandler.text;
                    var textResult = text.Replace("{\"data\":{\"token\":", "").Replace("}}}}", "}}");
                    var jsonToObject = JsonUtility.FromJson<NFTMetadata.Rootobject>(textResult);
                    metadata = jsonToObject.token;
                    var getImage = new DownloadImage();
                    StartCoroutine(getImage.FromUrl(jsonToObject.token.image.url, gameObject));
                    break;
            }
        }
    }
}
