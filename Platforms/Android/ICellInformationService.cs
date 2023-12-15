using Android.Content;

namespace BaseStation
{ 
    public interface ICellInformationService
    {
        void OnStart(Context context);
        void OnStop();
    }
}