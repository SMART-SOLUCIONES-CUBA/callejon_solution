namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Providers
{
    public static class ConstantsProvider
    {
        internal const string SOCKET_END_OF_MESSAGE_MARK = "<EOM>";
        internal const int SOCKET_BUFFER_LENGHT = 10240;
        internal const int DATASHARE_RECEIVER_PRESENCE_UNICAST_SOCKET_PORT = 9650;
        internal const int DATASHARE_SENDER_BUNDLE_BROADCAST_SOCKET_PORT = 9450;
        internal const int DATASHARE_SENDER_PRESENCE_BROADCAST_SOCKET_PORT = 9800;
        internal const int DATASHARE_BUNDLE_BROADCAST_SOCKET_PORT = 9900;

        internal const string ACTIONSHEET_PICKIMAGE_OPTION_CAMARA = "Captura con la Cámara";
        internal const string ACTIONSHEET_PICKIMAGE_OPTION_GALLERY = "Galería de Imágenes";

        public static string UNSELECTABLE_ROW_FLOWLISTVIEW_STYLEID = "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF";

        internal const string API_KEY_SECRET_PROPERTY_KEY = "api-key-secret";
        internal const string API_KEY_HEADER_PROPERTY = "X-Api-Key";
        internal const string API_KEY_SECRET = "v1.9.0-rc-eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";
    }
}
