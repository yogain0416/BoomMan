using System.Collections.Generic;
using System.IO;
using App.Data;
using BoomManData;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
public class OreDataImporter : MonoBehaviour
{
    [MenuItem("Tools/Import OreData from BoomManData")]
    public static void ImportOreData()
    {
        // OreStatus 데이터를 로드합니다.
        List<OreStatus> oreStatusList = OreStatus.GetList();

        if (oreStatusList == null || oreStatusList.Count == 0)
        {
            Debug.LogError("No OreStatus data loaded from BoomManData.");
            return;
        }

        // ScriptableObject가 저장될 폴더 경로 설정
        string folderPath = "Assets/ScriptableObjects/Ore";
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        // OreStatus 데이터를 기반으로 OreData ScriptableObject 생성
        foreach (OreStatus oreStatus in oreStatusList)
        {
            OreData oreData = ScriptableObject.CreateInstance<OreData>();
            oreData.id = oreStatus.id;
            oreData.name = oreStatus.name;
            oreData.oreResourceRate = oreStatus.oreResourceRate;
            oreData.oreResourceCost = oreStatus.oreResourceCost;
            oreData.oreStageThresholds = oreStatus.oreStageThresholds;

            // ScriptableObject를 에셋 파일로 저장
            string assetPath = Path.Combine(folderPath, $"{oreStatus.id}_OreData.asset");
            AssetDatabase.CreateAsset(oreData, assetPath);
        }

        // 변경 사항 적용
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        Debug.Log("OreData imported successfully from BoomManData!");
    }
}
#endif
