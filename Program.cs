using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using DialysisManagement.Data;
using DialysisManagement.Repositories;
using DialysisManagement.Services;
using DialysisManagement.Utilities;
using DialysisManagement.Views;

namespace DialysisManagement
{
    /// <summary>
    /// Entry point dell'applicazione con configurazione Dependency Injection
    /// </summary>
    static class Program
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        [STAThread]
        static void Main()
        {
            // Configura NLog
            LogManager.LoadConfiguration("nlog.config");
            var logger = LogManager.GetCurrentClassLogger();

            try
            {
                logger.Info("=== Avvio Applicazione Dialysis Management ===");

                // Setup applicazione Windows Forms
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                // Configura gestione eccezioni globali
                Application.ThreadException += Application_ThreadException;
                AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

                // Configura Dependency Injection
                var services = new ServiceCollection();
                ConfigureServices(services);
                ServiceProvider = services.BuildServiceProvider();

                logger.Info("Dependency Injection configurato con successo");

                // Verifica connessione database
                var dbHelper = ServiceProvider.GetRequiredService<IDatabaseHelper>();
                if (!dbHelper.TestConnection())
                {
                    logger.Error("Impossibile connettersi al database");
                    MessageBox.Show(
                        "Errore di connessione al database.\n\n" +
                        "Verificare che il servizio MySQL sia avviato e configurato correttamente.",
                        "Errore Database",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    return;
                }

                logger.Info("Connessione database OK");

                // Avvia form di login
                var loginForm = ServiceProvider.GetRequiredService<LoginForm>();
                Application.Run(loginForm);

                logger.Info("=== Chiusura Applicazione ===");
            }
            catch (Exception ex)
            {
                logger.Fatal(ex, "Errore fatale durante l'avvio dell'applicazione");
                MessageBox.Show(
                    $"Errore fatale:\n\n{ex.Message}\n\nL'applicazione verrà chiusa.",
                    "Errore Fatale",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                // Cleanup
                if (ServiceProvider is IDisposable disposable)
                {
                    disposable.Dispose();
                }
                LogManager.Shutdown();
            }
        }

        /// <summary>
        /// Configura tutti i servizi per Dependency Injection
        /// </summary>
        private static void ConfigureServices(IServiceCollection services)
        {
            // Logging
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                builder.AddNLog();
            });

            // Database
            services.AddSingleton<IDatabaseHelper, DatabaseHelper>();

            // Repositories
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IDialysisSessionRepository, DialysisSessionRepository>();
            services.AddScoped<ILabTestRepository, LabTestRepository>();
            services.AddScoped<IVascularAccessRepository, VascularAccessRepository>();
            services.AddScoped<IInstrumentalExamRepository, InstrumentalExamRepository>();
            services.AddScoped<IExamAttachmentRepository, ExamAttachmentRepository>();
            services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
            services.AddScoped<IMedicationRepository, MedicationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuditLogRepository, AuditLogRepository>();
            services.AddScoped<IBackupRepository, BackupRepository>();

            // Services
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IDialysisSessionService, DialysisSessionService>();
            services.AddScoped<ILabTestService, LabTestService>();
            services.AddScoped<IVascularAccessService, VascularAccessService>();
            services.AddScoped<IInstrumentalExamService, InstrumentalExamService>();
            services.AddScoped<IPrescriptionService, PrescriptionService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IAuditService, AuditService>();
            services.AddScoped<IBackupService, BackupService>();
            services.AddScoped<IReportService, ReportService>();

            // Utilities
            services.AddSingleton<IValidationHelper, ValidationHelper>();
            services.AddSingleton<IEncryptionHelper, EncryptionHelper>();
            services.AddSingleton<IFileHelper, FileHelper>();
            services.AddSingleton<IExportHelper, ExportHelper>();

            // Forms (Transient - nuova istanza ogni volta)
            services.AddTransient<LoginForm>();
            services.AddTransient<MainDashboard>();
            services.AddTransient<PatientListForm>();
            services.AddTransient<PatientDetailForm>();
            services.AddTransient<DialysisSessionForm>();
            services.AddTransient<LabTestsForm>();
            services.AddTransient<InstrumentalExamsForm>();
            services.AddTransient<ReportsForm>();
            services.AddTransient<SettingsForm>();
            services.AddTransient<BackupRestoreForm>();
        }

        /// <summary>
        /// Gestione eccezioni non gestite da thread UI
        /// </summary>
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var logger = LogManager.GetCurrentClassLogger();
            logger.Error(e.Exception, "Eccezione non gestita da thread UI");

            MessageBox.Show(
                $"Si è verificato un errore:\n\n{e.Exception.Message}\n\n" +
                "L'errore è stato registrato nel log.",
                "Errore",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        /// <summary>
        /// Gestione eccezioni non gestite da altri thread
        /// </summary>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var logger = LogManager.GetCurrentClassLogger();
            var exception = e.ExceptionObject as Exception;

            logger.Fatal(exception, "Eccezione non gestita - Terminazione applicazione");

            MessageBox.Show(
                $"Errore fatale:\n\n{exception?.Message}\n\n" +
                "L'applicazione verrà chiusa.",
                "Errore Fatale",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }
}
