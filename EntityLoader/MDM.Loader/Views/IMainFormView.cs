namespace MDM.Loader.Views
{
    public interface IMainFormView
    {
        void SetStatusMesage(string message);
        void AppendLogText(string message);
    }
}