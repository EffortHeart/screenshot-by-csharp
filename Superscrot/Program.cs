using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Superscrot
{
    /// <summary>
    /// Probably shouldn't do nearly as much as it does right now.
    /// </summary>
    public static class Program
    {
        private static Configuration config = null;
        private static Manager manager = null;
        private static string settingsPath = string.Empty;
        private static bool startedWithDefaultSettings = false;
        private static EventWaitHandle startupEventHandle;

        /// <summary>
        /// Occurs when the <see cref="Config"/> property changes.
        /// </summary>
        public static event EventHandler ConfigurationChanged;

        /// <summary>
        /// Provides common configurable settings.
        /// </summary>
        public static Configuration Config
        {
            get { return config; }
            internal set
            {
                if (value != config)
                {
                    config = value;
                    OnConfigurationChanged();
                }
            }
        }

        /// <summary>
        /// Coordinates top-level functionality and provides common functions
        /// that interact between classes.
        /// </summary>
        public static Manager Manager
        {
            get { return manager; }
            internal set { manager = value; }
        }

        /// <summary>
        /// Gets the path to the file where the settings are located.
        /// </summary>
        public static string SettingsPath
        {
            get { return settingsPath; }
        }

        /// <summary>
        /// This application's tray icon.
        /// </summary>
        public static TrayIcon Tray
        {
            get { return TrayIcon.GetInstance(); }
        }

        /// <summary>
        /// Cleans up and exit the application.
        /// </summary>
        public static void Exit()
        {
            Tray.Dispose();
            Manager.Dispose();
            Application.Exit();
        }

        /// <summary>
        /// Starts the config editor.
        /// </summary>
        public static void ShowConfigEditor()
        {
            using (var settings = new Dialogs.Settings())
            {
                settings.Configuration = new Configuration(Program.Config);
                settings.ShowDialog();
            }
        }

        private static void LoadSettings()
        {
            string appData = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Superscrot");
            if (!Directory.Exists(appData))
                Directory.CreateDirectory(appData);

            settingsPath = Path.Combine(appData, "Config.xml");

            var logName = string.Format("{0:y}.svclog", DateTime.Now);
            var logPath = Path.Combine(appData, logName);
            Trace.Listeners.Add(new XmlWriterTraceListener(logPath, "Superscrot"));
            Trace.AutoFlush = true;

            if (File.Exists(settingsPath))
            {
                config = Configuration.LoadSettings(settingsPath);
            }
            else
            {
                //Save default settings
                config = new Configuration();
                Program.Config.SaveSettings(settingsPath);
                startedWithDefaultSettings = true;
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(UnhandledException);

            Manager = new Superscrot.Manager();
            LoadSettings();

            CommandlineParser cmd = new CommandlineParser(args);
            if (startedWithDefaultSettings || cmd["config"] != null)
            {
                ShowConfigEditor();
            }

            bool created = false;
            startupEventHandle = new EventWaitHandle(false,
                EventResetMode.ManualReset,
                Environment.UserName + "SuperscrotStartup", out created);
            if (created)
            {
                if (!Manager.InitializeKeyboardHook())
                {
                    Exit();
                    return;
                }
                Tray.Show();
                Application.Run();
            }
            else
            {
                MessageBox.Show(SR.InstanceAlreadyRunning, Application.ProductName,
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
        }

        /// <summary>
        /// Raises the <see cref="ConfigurationChanged"/> event.
        /// </summary>
        private static void OnConfigurationChanged()
        {
            var handler = ConfigurationChanged;
            if (handler != null)
                handler(null, EventArgs.Empty);
        }

        /// <summary>
        /// Logs unhandled exceptions as fatal exceptions.
        /// </summary>
        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                Exception ex = e.ExceptionObject as Exception;
                Trace.WriteLine(ex);
            }
            catch (Exception ex)
            {
                Trace.WriteLine("An exception occurred while handling an unhandled exception:");
                Trace.WriteLine(ex);
            }
        }
    }
}
