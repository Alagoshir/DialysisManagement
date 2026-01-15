using Microsoft.Extensions.DependencyInjection;
using DialysisManagement.Data;
using DialysisManagement.Repositories;
using DialysisManagement.Services;
using DialysisManagement.Utilities;

namespace DialysisManagement.Extensions
{
    /// <summary>
    /// Extension methods per configurazione servizi
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registra tutti i repository
        /// </summary>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
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

            return services;
        }

        /// <summary>
        /// Registra tutti i servizi business logic
        /// </summary>
        public static IServiceCollection AddBusinessServices(this IServiceCollection services)
        {
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

            return services;
        }

        /// <summary>
        /// Registra tutti gli helper/utilities
        /// </summary>
        public static IServiceCollection AddUtilities(this IServiceCollection services)
        {
            services.AddSingleton<IValidationHelper, ValidationHelper>();
            services.AddSingleton<IEncryptionHelper, EncryptionHelper>();
            services.AddSingleton<IFileHelper, FileHelper>();
            services.AddSingleton<IExportHelper, ExportHelper>();

            return services;
        }

        /// <summary>
        /// Registra layer database
        /// </summary>
        public static IServiceCollection AddDatabase(this IServiceCollection services)
        {
            services.AddSingleton<IDatabaseHelper, DatabaseHelper>();

            return services;
        }
    }
}
