using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    [SerializeField] private string _androidGameID;
    [SerializeField] private string _iOSGameID;
    [SerializeField] private string _rewardedVideoAndroid;
    [SerializeField] private string _rewardedVideoIOS;
    [SerializeField] private bool _testMode = true;

    private string _adUnitId;
    private string _rewardAd;

    private void Awake()
    {
        // Get the Ad Unit ID for the current platform:

        if (Application.platform == RuntimePlatform.Android)
        {
            _adUnitId = _androidGameID;
            _rewardAd = _rewardedVideoAndroid;
        }
        else
        {
            _adUnitId = _iOSGameID;
            _rewardAd = _rewardedVideoIOS;
        }
        
        Advertisement.Initialize(_adUnitId, _testMode);
    }

    public void ShowRewardedVideo()
    {
        if (Advertisement.IsReady(_rewardAd))
        {
            Advertisement.Show(_rewardAd);
        }
        else
        {
            Debug.Log("Rewarded Video is not ready, please try again later!");
        }
    }
 
    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Rewarded Ad is ready");
    }
 
    public void OnUnityAdsDidError(string message)
    {
        Debug.LogWarning("The ad did not finish due to an error.");
    }
 
    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Rewarded Ad started!");
    }
 
    public void OnUnityAdsDidFinish(string placementId, ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                // PlayerStats.Instance.AddDiamonds(100);
                // SavingSystem.Instance.SaveCurrency();
                Debug.Log("Watched ad");
                break;
            case ShowResult.Failed:
                Debug.LogWarning("The ad did not finish due to an error.");
                break;
            case ShowResult.Skipped:
                Debug.LogWarning("The ad was skipped, you will not be rewarded");
                break;
        }
    }
    
    private void OnEnable()
    {
        Advertisement.AddListener(this);
        
    }
 
    private void OnDisable()
    {
        Advertisement.RemoveListener(this);
    }
}
