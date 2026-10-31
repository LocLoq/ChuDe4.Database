namespace Bai3;

internal static class Program
{
    internal const string ConnectionString = @"server=.\LOQLSERVER01; database=PT_App_Desktop_RetaurantManagement; Integrated Security = true; TrustServerCertificate=True;";
    internal static string SqlText(string value) => value.Replace("'", "''");

    [STAThread]
    private static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new AccountManager());
    }
}
