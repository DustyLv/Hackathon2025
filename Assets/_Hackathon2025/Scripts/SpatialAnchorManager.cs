using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialAnchorManager : MonoBehaviour
{
    public static SpatialAnchorManager instance;

    private AnchorLoader anchorLoader;
    public const string NumUuidsPlayerPref = "numUuids";

    public List<GameObject> SpatialAnchorPrefabs;
    public List<OVRSpatialAnchor> anchors = new();

    private OVRSpatialAnchor lastCreatedAnchor;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    void Start() {
        anchorLoader = GetComponent<AnchorLoader>();
    }

    public void CreateSpatialAnchor(int prefabIndex, Transform placementTransform) {
        // var position = new Vector3(OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch).x, 0, OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch).z);
        // var rotation = Quaternion.Euler(0, OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch).eulerAngles.y, 0);
        var rotation = Quaternion.Euler(0, placementTransform.rotation.eulerAngles.y, 0);
        
        GameObject workingAnchor = Instantiate(SpatialAnchorPrefabs[prefabIndex], placementTransform.position, rotation);        

        StartCoroutine(AnchorCreated(workingAnchor.GetComponent<OVRSpatialAnchor>()));
    }

    private IEnumerator AnchorCreated(OVRSpatialAnchor workingAnchor) {
        while (!workingAnchor.Created && !workingAnchor.Localized) {
            yield return new WaitForEndOfFrame();
        }
        
        Guid anchorGuid = workingAnchor.Uuid;
        anchors.Add(workingAnchor);

        var saveData = workingAnchor.GetComponent<SpatialAnchorSaveData>();
        saveData.AnchorUuid = anchorGuid;

        lastCreatedAnchor = workingAnchor;

        Debug.Log("Created anchor with UUID: " + anchorGuid);
    }

    public async void SaveLastCreatedAnchor() {
        if (lastCreatedAnchor == null) {
            Debug.Log("No anchor to save");
            return;
        }
        var result = await lastCreatedAnchor.SaveAnchorAsync();
    
        if (result.Success) {
            Debug.Log("Saved anchor with UUID: " + lastCreatedAnchor.Uuid);
            var saveData = lastCreatedAnchor.GetComponent<SpatialAnchorSaveData>();
            SaveUuidToPlayerPrefs(saveData);
        } else {
            Debug.Log("Failed to save anchor");
        }
    }

    void SaveUuidToPlayerPrefs(SpatialAnchorSaveData data) {
        if (!PlayerPrefs.HasKey(NumUuidsPlayerPref)) {
            PlayerPrefs.SetInt(NumUuidsPlayerPref, 0);
            Debug.Log("Save: NumUuidsPlayerPref not found, creating new one");
        }
        
        int playerNumUuids = PlayerPrefs.GetInt(NumUuidsPlayerPref);
        PlayerPrefs.SetString("uuid" + playerNumUuids, data.ToString());
        Debug.Log("Saved UUID to player prefs: " + data.ToString() + " with key: " + "uuid" + playerNumUuids);
        PlayerPrefs.SetInt(NumUuidsPlayerPref, ++playerNumUuids);
    }

    public async void UnSaveLastCreatedAnchor() {
        if (lastCreatedAnchor == null) {
            Debug.Log("No anchor to unsave");
            return;
        }

        var result = await lastCreatedAnchor.EraseAnchorAsync();

        if (result.Success) {
            Debug.Log("Unsaved anchor with UUID: " + lastCreatedAnchor.Uuid);
            anchors.Remove(lastCreatedAnchor);
            Destroy(lastCreatedAnchor.gameObject);
        } else {
            Debug.Log("Failed to unsave anchor");
        }
    }

    public void UnsaveAllAnchors() {
        foreach (var anchor in anchors) {
            if (anchor == null) continue;
            UnsaveAnchor(anchor);
            Destroy(anchor.gameObject);
        }
        
        anchors.Clear();
        ClearAllUuidsFromPlayerPrefs();
        Debug.Log("Unsaved all anchors method called");
    }

    private async void UnsaveAnchor(OVRSpatialAnchor anchor) {
        await anchor.EraseAnchorAsync();
    
        Debug.Log("Unsaved anchor with UUID: " + anchor.Uuid);
    }

    private void ClearAllUuidsFromPlayerPrefs() {
        if (PlayerPrefs.HasKey(NumUuidsPlayerPref)) {
            int playerNumUuids = PlayerPrefs.GetInt(NumUuidsPlayerPref);
            for (int i = 0; i < playerNumUuids; i++) {
                PlayerPrefs.DeleteKey("uuid" + i);
            }
            PlayerPrefs.DeleteKey(NumUuidsPlayerPref);
            PlayerPrefs.Save();
        }
    }

    public void LoadSavedAnchors() {
        anchorLoader.LoadAnchorsByUuid();   
    }
}