namespace HuongDan
{
    internal static class Program
    {
        internal const string ConnectionString = @"server=.\LOQLSERVER01; database=PT_App_Desktop_RetaurantManagement; Integrated Security = true; TrustServerCertificate=True;";
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new CatetoryForm());
        }
    }
}
