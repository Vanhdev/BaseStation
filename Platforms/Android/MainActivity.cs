using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Telephony;
using Android.Util;
using AndroidX.Core.App;
using BaseStation.Models;
using BaseStation.Shared;
using Java.Interop;

namespace BaseStation;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{
    private static LocationManager _locationManager;
    private static MyLocationListener _locationListener = new MyLocationListener();
    private static TelephonyManager _telephonyManager;
    private CellInformationService _cellInformationService = new CellInformationService();
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        if (ActivityCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) != Permission.Granted &&
            ActivityCompat.CheckSelfPermission(this, Manifest.Permission.AccessCoarseLocation) != Permission.Granted)
        {
            ActivityCompat.RequestPermissions(this, new string[] {
                Manifest.Permission.AccessFineLocation, 
                Manifest.Permission.AccessCoarseLocation,
            }, 1);
        }
    }
    protected override void OnStart()
    {
        base.OnStart();
        if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
        {
            Android.Webkit.WebView.SetWebContentsDebuggingEnabled(true);
        }
        var context = AndroidX.Core.Content.ContextCompat.CreateDeviceProtectedStorageContext(this);
        _cellInformationService.OnStart(context);
        //_locationManager = ((LocationManager) GetSystemService(LocationService));
        //string provider = LocationManager.GpsProvider;

        //_locationManager.RequestLocationUpdates(provider, 1000, 1, _locationListener);
        //_telephonyManager = ((TelephonyManager) GetSystemService(TelephonyService));
    }

    protected override void OnStop()
    {
        base.OnStop();
        _cellInformationService.OnStop();
        //_locationManager.RemoveUpdates(_locationListener);
    }

    public async Task<GPS> GetGPS()
    {
        if (_locationManager != null)
        {
            string provider = LocationManager.GpsProvider;

            var lastKnownLocation = _locationManager.GetLastKnownLocation(provider);
            if (lastKnownLocation != null)
            {
                var gps = new GPS();
                gps.Latitude = lastKnownLocation.Latitude;
                gps.Longitude = lastKnownLocation.Longitude;
                gps.Altitude = lastKnownLocation.Altitude;
                gps.Accuracy = lastKnownLocation.Accuracy;
                gps.Speed = lastKnownLocation.Speed;
                return gps;
            }
            else
            {
                return null;
            }
        }
        return null;
    }

    [Obsolete]
    public async Task<List<CellInfor>> GetCellInfos()
    {
        List<CellInfor> CellList = new List<CellInfor>();
        var check = this.PackageManager.GetSystemAvailableFeatures();
        var x = _telephonyManager.CellLocation;
        var lst = _telephonyManager.AllCellInfo;
        var CellInfoList = lst.ToList();
        if (CellInfoList == null || CellInfoList.Count() == 0)
        {
            return null;
        }

        int cellNumber = CellInfoList.Count();
        foreach (CellInfo cellInfo in CellInfoList)
        {
            CellInfor bs = bindData(cellInfo);
            CellList.Add(bs);
        }
        return CellList;
    }

    [Obsolete]
    private CellInfor bindData(CellInfo cellInfo)
    {
        CellInfor baseStation = null;
        //基站有不同信号类型：2G，3G，4G
        if (cellInfo is CellInfoWcdma)
        {
            //联通3G
            CellInfoWcdma cellInfoWcdma = (CellInfoWcdma)cellInfo;
            CellIdentityWcdma cellIdentityWcdma = cellInfoWcdma.CellIdentity;
            baseStation = new CellInfor();
            baseStation.SetCellType("WCDMA");
            baseStation.SetCid(cellIdentityWcdma.Cid);
            baseStation.SetLac(cellIdentityWcdma.Lac);
            baseStation.SetMcc(cellIdentityWcdma.Mcc);
            baseStation.SetMnc(cellIdentityWcdma.Mnc);
            baseStation.SetBsic_psc_pci(cellIdentityWcdma.Psc);
            if (cellInfoWcdma.CellSignalStrength != null)
            {
                baseStation.SetAsuLevel(cellInfoWcdma.CellSignalStrength.AsuLevel); //Get the signal level as an asu value between 0..31, 99 is unknown Asu is calculated based on 3GPP RSRP.
                baseStation.SetSignalLevel(cellInfoWcdma.CellSignalStrength.Level); //Get signal level as an int from 0..4
                baseStation.SetDbm(cellInfoWcdma.CellSignalStrength.Dbm); //Get the signal strength as dBm
            }
        }
        else if (cellInfo is CellInfoLte)
        {
            //4G
            CellInfoLte cellInfoLte = (CellInfoLte)cellInfo;
            CellIdentityLte cellIdentityLte = cellInfoLte.CellIdentity;
            baseStation = new CellInfor();
            baseStation.SetCellType("LTE");
            baseStation.SetCid(cellIdentityLte.Ci);
            baseStation.SetMnc(cellIdentityLte.Mnc);
            baseStation.SetMcc(cellIdentityLte.Mcc);
            baseStation.SetLac(cellIdentityLte.Tac);
            baseStation.SetBsic_psc_pci(cellIdentityLte.Pci);
            if (cellInfoLte.CellSignalStrength != null)
            {
                baseStation.SetAsuLevel(cellInfoLte.CellSignalStrength.AsuLevel);
                baseStation.SetSignalLevel(cellInfoLte.CellSignalStrength.Level);
                baseStation.SetDbm(cellInfoLte.CellSignalStrength.Dbm);
            }
        }
        else if (cellInfo is CellInfoGsm)
        {
            //2G
            CellInfoGsm cellInfoGsm = (CellInfoGsm)cellInfo;
            CellIdentityGsm cellIdentityGsm = cellInfoGsm.CellIdentity;
            baseStation = new CellInfor();
            baseStation.SetCellType("GSM");
            baseStation.SetCid(cellIdentityGsm.Cid);
            baseStation.SetLac(cellIdentityGsm.Lac);
            baseStation.SetMcc(cellIdentityGsm.Mcc);
            baseStation.SetMnc(cellIdentityGsm.Mnc);
            baseStation.SetBsic_psc_pci(cellIdentityGsm.Psc);
            if (cellInfoGsm.CellSignalStrength != null)
            {
                baseStation.SetAsuLevel(cellInfoGsm.CellSignalStrength.AsuLevel);
                baseStation.SetSignalLevel(cellInfoGsm.CellSignalStrength.Level);
                baseStation.SetDbm(cellInfoGsm.CellSignalStrength.Dbm);
            }
        }
        else
        {
            return null;
        }
        return baseStation;
    }
}

public class MyLocationListener : Java.Lang.Object, ILocationListener
{
    public void OnLocationChanged(Android.Locations.Location location)
    {

    }

    public void OnProviderDisabled(string provider)
    {
        throw new NotImplementedException();
    }

    public void OnProviderEnabled(string provider)
    {
        throw new NotImplementedException();
    }

    public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
    {
        throw new NotImplementedException();
    }

}