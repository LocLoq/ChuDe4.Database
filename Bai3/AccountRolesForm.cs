using System.Data;

namespace Bai3;

public partial class AccountRolesForm : Form
{
    private readonly string _accountName;
    public AccountRolesForm() : this(string.Empty) { }
    public AccountRolesForm(string accountName)
    { _accountName = accountName; InitializeComponent(); Text = $"Vai trò của {accountName}"; }

    private void AccountRolesForm_Load(object sender, EventArgs e)
    {
        try
        {
            using var reader = Database.ExecuteQuery($"SELECT r.RoleName AS [Vai trò], ra.Actived AS [Đang kích hoạt], ra.Notes AS [Ghi chú] FROM RoleAccount ra JOIN dbo.[Role] r ON r.ID=ra.RoleID WHERE ra.AccountName=N'{Program.SqlText(_accountName)}' ORDER BY r.RoleName");
            var table = new DataTable(); table.Load(reader); dgvRoles.DataSource = table;
        }
        catch (Exception ex) { MessageBox.Show($"Không lấy được vai trò: {ex.Message}"); }
    }
}
