namespace SmartSolucionesCuba.CPCC.PCM.Terminal.NMMediaStore.Providers.Constant
{
    public static class StringConstantProvider
    {
        private const string SUPPORT_ORIENTATION_MESSAGE = "Por favor contacte con el personal de asistencia técnica para notificar esta situación.";

        public const string INVALID_CREDENTIALS_LDAP_EXCEPTION_MESSAGE = "Usuario y/o contraseña incorrectos.";

        public static string UNSPECTED_LDAP_EXCEPTION_MESSAGE = string.Format("Error al contactar con el servicio de autenticación.{1}{1}{0}", SUPPORT_ORIENTATION_MESSAGE, System.Environment.NewLine);

        public const string INSUFFICIENT_STORAGE_SPACE_EXCEPTION_MESSAGE = "El espacio de almacenamiento disponible en su dispositivo no será suficiente para descargar este audio visual.";

        public const string UNSPECIFIED_STORAGE_DEVICE_EXCEPTION_MESSAGE = "Dispositivo de almacenamiento no especificado.";

        public static string UPGRADE_REQUIRED_EXCEPTION_MESSAGE = string.Format("Es necearia la actualización de esta aplicación para poder continuar.{1}{1}{0}", SUPPORT_ORIENTATION_MESSAGE, System.Environment.NewLine);

        public static string UNSPECTED_API_RESPONSE_EXCEPTION_MESSAGE = string.Format("Respuesta del proveedor del sistema no esperada.{1}{1}{0}", SUPPORT_ORIENTATION_MESSAGE, System.Environment.NewLine);

        public static string NETWORK_EXCEPTION_MESSAGE = string.Format("Error de comunicación con el proveedor del sistema.{1}{1}{0}", SUPPORT_ORIENTATION_MESSAGE, System.Environment.NewLine);

        public static string UNSPECTED_EXCEPTION_MESSAGE = string.Format("Error no esperado en el sistema.{1}{1}{0}", SUPPORT_ORIENTATION_MESSAGE, System.Environment.NewLine);

        public const string ADVICE_ALERT_DIALOG_TITLE = "Aviso";

        public const string CONFIRMATION_ALERT_DIALOG_TITLE = "Confirmar";

        public const string MALE_NONE_OPTION = "Ninguno";

        public const string OK_ACTION = "Aceptar";

        public const string CANCEL_ACTION = "Cancelar";
    }
}
