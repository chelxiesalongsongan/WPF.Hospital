using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using WPF.Hospital.Repository;
using WPF.Hospital.Service;

namespace WPF.Hospital
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                })
                .ConfigureServices((context, services) =>
                {
                    // DbContext with sensitive data logging enabled for debugging
                    services.AddDbContext<HospitaDbContext>(options =>
                        options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection"))
                               .EnableSensitiveDataLogging() // para makita ang inner exception details
                    );

                    // Repositories
                    services.AddScoped<IPatientRepository, PatientRepository>();
                    services.AddScoped<IDoctorRepository, DoctorRepository>();
                    services.AddScoped<IHistoryRepository, HistoryRepository>();
                    services.AddScoped<IMedicineRepository, MedicineRepository>();
                    services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();

                    // Services
                    services.AddScoped<IPatientService, PatientService>();
                    services.AddScoped<IDoctorService, DoctorService>();
                    services.AddScoped<IHistoryService, HistoryService>();
                    services.AddScoped<IMedicineService, MedicineService>();
                    services.AddScoped<IPrescriptionService, PrescriptionService>();

                    // Main Window
                    services.AddTransient<MainWindow>();
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (_host)
            {
                await _host.StopAsync();
            }
            base.OnExit(e);
        }
    }
}
