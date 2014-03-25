namespace MDM.Loader.Views
{
    public interface IMainFormView
    {
        void AppendLogText(string message);

        void SetStatusMesage(string message);
    }
}