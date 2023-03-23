using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace MagicLeap.Soundfield
{
// verify MSA is selected in audio settings
#if UNITY_EDITOR
using UnityEditor.PackageManager;
public static class InstalledPackages
{
    [InitializeOnLoadMethod]
    private static void InitializeOnLoad()
    {
        var listRequest = Client.List(true);
        while (!listRequest.IsCompleted)
            Thread.Sleep(100);

        if (listRequest.Error != null)
        {
            Debug.Log("Error: " + listRequest.Error.message);
            return;
        }

        var packages = listRequest.Result;
        foreach (var package in packages)
        {
            if(package.name.Contains("soundfield"))
            {
                var version = package.version;
                MagicLeap.Soundfield.MSAGlobalScriptableObject asset = GetAsset();
                asset.MSAPackageVersionFromSO = version;
                asset.IsMLSDKPresent = false;  // by default we don't know if SDK is present
                EditorUtility.SetDirty(asset);
                var spatializerName = AudioSettings.GetSpatializerPluginName();
                Debug.Log("Spatializer in use: " + spatializerName);
                if(!spatializerName.Contains("MSA Spatializer"))
                {
                    // AudioSettings will not save unless the user changes it, Unity's thing, so do not set it and confuse the user.
                    EditorUtility.DisplayDialog("Spatializer Selection", "The spatializer selected is not Magic Leap MSA.\nMSA Spatializer can be selected under Project Settings/Audio/Spatialzer Plugin", "OK");
                }
            }
            else if(package.name.Contains("com.magicleap.unitysdk"))
            {
                var version = package.version;
                MagicLeap.Soundfield.MSAGlobalScriptableObject asset = GetAsset();
                if(string.Compare(package.version,"0.53.0")>0 )
                    asset.IsMLSDKPresent = true;
                else
                    asset.IsMLSDKPresent = false;
                Debug.Log("SDK package dependency is met: "+asset.IsMLSDKPresent.ToString());
                EditorUtility.SetDirty(asset);
            }
        }

        {
            var audioManagerAsset = AssetDatabase.LoadMainAssetAtPath("ProjectSettings/AudioManager.asset");

            var serializedObject = new SerializedObject(audioManagerAsset);
            serializedObject.Update();
            var ambisonicDecoderProperty = serializedObject.FindProperty("m_AmbisonicDecoderPlugin");
            var ambiDecoder = ambisonicDecoderProperty.stringValue;
            Debug.Log("Ambisonic decoder in use: " + ambiDecoder);
            if(!ambiDecoder.Contains("MSA Ambisonic"))
            {
                EditorUtility.DisplayDialog("Ambisonic Decoder Selection", "The ambisonic decoder selected is not Magic Leap MSA.\nYou can select MSA Ambisonic under Project Settings/Audio/Ambisonic Decoder Plugin", "OK");
            }

        }
    }
    private static MagicLeap.Soundfield.MSAGlobalScriptableObject GetAsset()
    {
        var guid = AssetDatabase.FindAssets($"t:{nameof(MSAGlobalScriptableObject)}").FirstOrDefault();
        MagicLeap.Soundfield.MSAGlobalScriptableObject asset;
        if(!string.IsNullOrWhiteSpace(guid))
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            asset = AssetDatabase.LoadAssetAtPath<MSAGlobalScriptableObject>(path);
        }
        else
        {

            // None found -> create a new one
            asset = ScriptableObject.CreateInstance<MSAGlobalScriptableObject>();
            asset.name = nameof(MSAGlobalScriptableObject);

            // Store the asset as actually asset
            if( !Directory.Exists($"{FileUtil.GetProjectRelativePath(Application.dataPath)}/Resources"))
            {
                Directory.CreateDirectory($"{FileUtil.GetProjectRelativePath(Application.dataPath)}/Resources");
            }
            AssetDatabase.CreateAsset(asset, $"{FileUtil.GetProjectRelativePath(Application.dataPath)}/Resources/{nameof(MSAGlobalScriptableObject)}.asset");
        }
        return asset;
    }
}
#endif
}