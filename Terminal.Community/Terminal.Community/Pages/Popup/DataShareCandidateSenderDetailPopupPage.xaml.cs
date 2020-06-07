namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Pages.Popup
{
    public partial class DataShareCandidateSenderDetailPopupPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        private System.Action<Models.View.DataShareSenderViewModel> approvedCallbackAction;

        public DataShareCandidateSenderDetailPopupPage(Models.View.DataShareSenderViewModel model, System.Action<Models.View.DataShareSenderViewModel> approvedCallbackAction)
        {
            InitializeComponent();

            this.approvedCallbackAction = approvedCallbackAction;

            BindingContext = new Models.Page.Popup.DataShareCandidateSenderDetailPopupPageModel
            {
                Sender = model,
                IsDataUpdateNewer = model.Friend.DataUpdatedAt > Helpers.DataSyncronizationHelper.Instance.SyncronizationDate
            };
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
                    var pageBindingContext = BindingContext as Models.Page.Popup.DataShareCandidateSenderDetailPopupPageModel;

                    var hostFriendDataModel = Factories.FriendDataModelFactory.CreateSelf();

                    var receiverPresenceMessageContent = System.Text.Encoding.UTF8.GetBytes(string.Format("{0}{1}", Newtonsoft.Json.JsonConvert.SerializeObject(hostFriendDataModel), Providers.ConstantsProvider.SOCKET_END_OF_MESSAGE_MARK));

                    var receiverPresenceNotifyEndPoint = new System.Net.IPEndPoint(pageBindingContext.Sender.IPAddress, Providers.ConstantsProvider.DATASHARE_RECEIVER_PRESENCE_UNICAST_SOCKET_PORT);

                    var receiverPresenceNotifySocket = new System.Net.Sockets.Socket(pageBindingContext.Sender.IPAddress.AddressFamily, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);

                    receiverPresenceNotifySocket.Connect(receiverPresenceNotifyEndPoint);

                    receiverPresenceNotifySocket.Send(receiverPresenceMessageContent);

                    receiverPresenceNotifySocket.Close();

                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        Rg.Plugins.Popup.Services.PopupNavigation.Instance.PopAsync();

                        approvedCallbackAction.Invoke(pageBindingContext.Sender);

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
