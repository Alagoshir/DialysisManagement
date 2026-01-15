using System;

namespace DialysisManagement
{
    /// <summary>
    /// Contesto globale applicazione
    /// </summary>
    public static class AppContext
    {
        /// <summary>
        /// Versione applicazione
        /// </summary>
        public const string Version = "1.0.0";

        /// <summary>
        /// Nome applicazione
        /// </summary>
        public const string AppName = "Dialysis Management System";

        /// <summary>
        /// Copyright
        /// </summary>
        public const string Copyright = "© 2026 Centro Dialisi";

        /// <summary>
        /// Percorso base applicazione
        /// </summary>
        public static string BasePath => AppDomain.CurrentDomain.BaseDirectory;

        /// <summary>
        /// Modalità debug
        /// </summary>
        public static bool IsDebugMode
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }

        /// <summary>
        /// Informazioni versione complete
        /// </summary>
        public static string VersionInfo => $"{AppName} v{Version}";

        /// <summary>
        /// Data build
        /// </summary>
        public static DateTime BuildDate => new DateTime(2026, 1, 14);
    }
}
