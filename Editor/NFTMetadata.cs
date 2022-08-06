using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFTMetadata
{
    [Serializable]
    public class Rootobject
    {
        public Token? token;
    }
    [Serializable]
    public class Token
    {
        public Image? image;
        public string? description;
        public Metadata? metadata;
        public string? owner;
    }
    [Serializable]
    public class Image
    {
        public Mediaencoding? mediaEncoding;
        public string? url;
        public string? mimeType;
    }
    [Serializable]
    public class Mediaencoding
    {
        public string? large;
        public string? poster;
        public string? thumbnail;
        public string? original;
    }
    [Serializable]
    public class Metadata
    {
        public string? name;
        public string? description;
        public string? image;
        public string? image_url;
        public Properties properties;
    }
    [Serializable]
    public class Properties
    {
        public string? artist;
        public string? series;
        public string? minted;
        public string? created;
    }



}
