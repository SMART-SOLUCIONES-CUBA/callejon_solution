namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages.Popup
{
    public partial class DataShareCandidateSenderDetailPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        private System.Action<Models.View.DataShareSenderViewModel> approvedCallbackAction;

        public DataShareCandidateSenderDetailPopupPage(Models.View.DataShareSenderViewModel model, System.Action<Models.View.DataShareSenderViewModel> approvedCallbackAction)
        {
            InitializeComponent();

            this.approvedCallbackAction = approvedCallbackAction;

            BindingContext = model;
        }

        private void ApproveButton_Clicked(object sender, System.EventArgs args)
        {
            DisplayAlert("Confirmar", "Esta seguro que desea seleccionar a este amigo para recibir su actualización?", "Aceptar", "Cancelar").ContinueWith(alertTask =>
            {
                if (!alertTask.Result)
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();
                    });

                    approvedCallbackAction = null;

                    return;
                }

                try
                {
                    var model = BindingContext as Models.View.DataShareSenderViewModel;

                    var hostFriendDataModel = Factories.FriendDataModelFactory.CreateSelf();

                    var receiverPresenceMessageContent = System.Text.Encoding.UTF8.GetBytes(string.Format("{0}{1}", Newtonsoft.Json.JsonConvert.SerializeObject(hostFriendDataModel), Providers.ConstantsProvider.SOCKET_END_OF_MESSAGE_MARK));

                    var receiverPresenceNotifyEndPoint = new System.Net.IPEndPoint(model.IPAddress, Providers.ConstantsProvider.DATASHARE_RECEIVER_PRESENCE_UNICAST_SOCKET_PORT);

                    var receiverPresenceNotifySocket = new System.Net.Sockets.Socket(model.IPAddress.AddressFamily, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);

                    receiverPresenceNotifySocket.Connect(receiverPresenceNotifyEndPoint);

                    receiverPresenceNotifySocket.Send(receiverPresenceMessageContent);

                    receiverPresenceNotifySocket.Close();

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();

                        approvedCallbackAction.Invoke(model);

                        approvedCallbackAction = null;
                    });
                }
                catch
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();

                        approvedCallbackAction = null;

                        DisplayAlert("Aviso", "No fue posible comunicarse con el dispositivo de su amigo.", "Aceptar");
                    });
                }
            });
        }
    }
}
