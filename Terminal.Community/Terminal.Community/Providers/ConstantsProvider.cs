namespace SmartSolucionesCuba.CPCC.PCM.Terminal.Community.Providers
{
    public static class ConstantsProvider
    {
        internal const string SOCKET_END_OF_MESSAGE_MARK = "<EOM>";
        internal const int SOCKET_PRESENCE_UNICAST_SERVER_PORT = 8888;
        internal const int SOCKET_DATASHARE_BROADCAST_SERVER_PORT = 9050;
        internal const int SOCKET_BONJOUR_BROADCAST_SERVER_PORT = 8900;

        internal const string ACTIONSHEET_PICKIMAGE_OPTION_CAMARA = "Captura con la Cámara";
        internal const string ACTIONSHEET_PICKIMAGE_OPTION_GALLERY = "Galería de Imágenes";

        public static string UNSELECTABLE_ROW_FLOWLISTVIEW_STYLEID = "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF";

        internal const string API_KEY_SECRET_PROPERTY_KEY = "api-key-secret";
        internal const string API_KEY_HEADER_PROPERTY = "X-Api-Key";
        internal const string API_KEY_SECRET = "v1.9.0-rc-eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";
    }
}
