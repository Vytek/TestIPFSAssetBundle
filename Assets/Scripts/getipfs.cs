using System.Collections;
using System.Collections.Generic;
using LitJson;
using System.IO;
using UnityEngine;

public class getipfs : MonoBehaviour {

    private JsonData itemData;

    //Help:http://answers.unity3d.com/questions/11021/how-can-i-send-and-receive-data-to-and-from-a-url.html
    //Verify IPFS in Daemon state working
    //Usando il gateway: https://gateway.ipfs.io/ipfs/QmcdH1f3mMZDVKGwhXW5nyBrSjK74o6r9G5xLcqetnhGeM

    // Use this for initialization
    void Start()
    {
        //QmeAuM7P7P7oUB7KC6eqv64k5WUyBh5reWW1aZ1QqQzFRA //AssetBundle Unity
        //string url = "http://localhost:5001/api/v0/cat?arg=QmeAuM7P7P7oUB7KC6eqv64k5WUyBh5reWW1aZ1QqQzFRA"; //Fbx file
        string url = "http://localhost:5001/api/v0/cat?arg=QmQTXdMMxFAxBFcLTTGLogzDAyNzf6ntsCbmzyQ63XiKt9"; //Fbx Candle file
        //string url = "http://localhost:5001/api/v0/get?arg=QmcdH1f3mMZDVKGwhXW5nyBrSjK74o6r9G5xLcqetnhGeM"; //Cat txt file
        //string url = "http://localhost:5001/api/v0/id"; //ID Ipfs
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        // if (www.error == null)
        if (string.IsNullOrEmpty(www.error))
        {
            Debug.Log("WWW Ok!: " + www.text);
            /* OK FUNZIONA! ESP 10/01/2017
            Debug.Log("WWW Ok!: " + www.text);
            //https://www.youtube.com/watch?v=OyQQ-7-22Hw&t=825s //LitJson
            itemData = JsonMapper.ToObject(www.text);
            Debug.Log(itemData["ID"]);
            */

            /*
            //Response Header
            Debug.Log(www.responseHeaders["Content-Type"]);
            */

            AssetBundle bundle = www.assetBundle;
            Debug.Log(bundle.name); //duckipfs
            //Show alle Assets Name
            for (int i = 0; i < bundle.GetAllAssetNames().Length; i++)
            {
                Debug.Log(bundle.GetAllAssetNames()[i]); //AssetsName
            }

            Vector3 pos = new Vector3(0, 3, 0); // as an example
            Debug.Log(bundle.LoadAsset(bundle.GetAllAssetNames()[0].ToString()));
            Debug.Log((bundle.GetAllAssetNames()[0].ToString()));
            //GameObject obj = Instantiate(bundle.LoadAsset("assets/meshes/duck.fbx"), pos, Quaternion.identity) as GameObject; //OK
            GameObject obj = Instantiate(bundle.LoadAsset(bundle.GetAllAssetNames()[0]), pos, Quaternion.identity) as GameObject; //OK
            
            // Unload the AssetBundles compressed contents to conserve memory
            bundle.Unload(false);
            // Frees the memory from the web stream
            www.Dispose();
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}
