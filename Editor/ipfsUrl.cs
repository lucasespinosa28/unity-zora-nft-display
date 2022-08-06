using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ipfsUrl 
{
    public static string isValid(string url)
    {
        Debug.Log(url);
        if (url.Contains("ipfs://")) return url.Replace("ipfs://", "https://ipfs.io/ipfs/");
        return url;
    }
}
