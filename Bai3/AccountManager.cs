using Microsoft.Data.SqlClient;
using System.Data;

namespace Bai3;

public partial class AccountManager : Form
{
    public AccountManager() => InitializeComponent();

    private void AccountManager_Load(object sender, EventArgs e)
    {
        try
        {
            using var reader = Database.ExecuteQuery("SELECT ID, RoleName FROM dbo.[Role] ORDER BY RoleName");
            var roles = new DataTable(); roles.Load(reader);
            var filterRoles = roles.Copy();
            var all = filterRoles.NewRow(); all["ID"] = 0; all["RoleName"] = "Tất cả nhóm";
            filterRoles.Rows.InsertAt(all, 0);
            cmbRoleFilter.DisplayMember = "RoleName"; cmbRoleFilter.ValueMember = "ID"; cmbRoleFilter.DataSource = filterRoles;
            cmbNewRole.DisplayMember = "RoleName"; cmbNewRole.ValueMember = "ID"; cmbNewRole.DataSource = roles;
            cmbStatus.SelectedIndex = 0;
            LoadAccounts();
        }
        catch (Exception ex) { MessageBox.Show($"Không tải được vai trò: {ex.Message}"); }
    }

    private void LoadAccounts()
    {
        try
        {
            int role = cmbRoleFilter.SelectedValue is int value ? value : 0;
            int status = cmbStatus.SelectedIndex switch { 1 => 1, 2 => 0, _ => -1 };
            var query = $"""
                SELECT a.AccountName AS [Tài khoản], a.FullName AS [Họ tên], a.Email,
                       a.Tell AS [Điện thoại], a.DateCreated AS [Ngày tạo],
                       CAST(CASE WHEN EXISTS (SELECT 1 FROM RoleAccount ra WHERE ra.AccountName=a.AccountName AND ra.Actived=1) THEN 1 ELSE 0 END AS bit) AS [Hoạt động]
                FROM Account a
                WHERE ({role}=0 OR EXISTS (SELECT 1 FROM RoleAccount ra WHERE ra.AccountName=a.AccountName AND ra.RoleID={role}))
                  AND ({status}=-1 OR {status}=CASE WHEN EXISTS (SELECT 1 FROM RoleAccount ra WHERE ra.AccountName=a.AccountName AND ra.Actived=1) THEN 1 ELSE 0 END)
                ORDER BY a.AccountName
                """;
            using var reader = Database.ExecuteQuery(query);
            var table = new DataTable(); table.Load(reader); dgvAccounts.DataSource = table;
        }
        catch (Exception ex) { MessageBox.Show($"Không tải được tài khoản: {ex.Message}"); }
    }

    private void btnFilter_Click(object sender, EventArgs e) => LoadAccounts();

    private void btnNew_Click(object sender, EventArgs e)
    {
        dgvAccounts.CurrentCell = null;
        dgvAccounts.ClearSelection();
        txtAccount.Clear();
        txtPassword.Clear();
        txtFullName.Clear();
        txtEmail.Clear();
        txtPhone.Clear();
    }

    private void dgvAccounts_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvAccounts.CurrentRow?.DataBoundItem is not DataRowView row) return;
        txtAccount.Text = row["Tài khoản"].ToString();
        txtFullName.Text = row["Họ tên"].ToString();
        txtEmail.Text = row["Email"].ToString();
        txtPhone.Text = row["Điện thoại"].ToString();
        txtPassword.Clear();
    }

    private bool ValidInput(bool needsPassword)
    {
        if (string.IsNullOrWhiteSpace(txtAccount.Text) || string.IsNullOrWhiteSpace(txtFullName.Text) ||
            (needsPassword && string.IsNullOrWhiteSpace(txtPassword.Text)))
        { MessageBox.Show("Nhập tên tài khoản, họ tên và mật khẩu khi thêm mới."); return false; }
        return true;
    }

    private string AccountValues(bool withPassword)
    {
        var name = Program.SqlText(txtAccount.Text.Trim());
        var full = Program.SqlText(txtFullName.Text.Trim());
        var email = string.IsNullOrWhiteSpace(txtEmail.Text) ? "NULL" : $"N'{Program.SqlText(txtEmail.Text.Trim())}'";
        var phone = Program.SqlText(txtPhone.Text.Trim());
        var password = Program.SqlText(txtPassword.Text);
        return withPassword
            ? $"N'{name}', N'{password}', N'{full}', {email}, N'{phone}'"
            : $"FullName=N'{full}', Email={email}, Tell=N'{phone}'";
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        if (!ValidInput(true)) return;
        if (cmbNewRole.SelectedValue is not int role) { MessageBox.Show("Chọn vai trò cho tài khoản mới."); return; }
        try
        {
            var name = Program.SqlText(txtAccount.Text.Trim());
            using var connection = new SqlConnection(Program.ConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();
            Database.ExecuteNonQuery($"INSERT Account (AccountName, [Password], FullName, Email, Tell) VALUES ({AccountValues(true)})", connection, transaction);
            Database.ExecuteNonQuery($"INSERT RoleAccount (AccountName, RoleID, Actived) VALUES (N'{name}', {role}, 1)", connection, transaction);
            transaction.Commit();
            LoadAccounts();
        }
        catch (Exception ex) { MessageBox.Show($"Không thêm được tài khoản: {ex.Message}"); }
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!ValidInput(false)) return;
        try
        {
            var name = Program.SqlText(txtAccount.Text.Trim());
            if (Database.ExecuteNonQuery($"UPDATE Account SET {AccountValues(false)} WHERE AccountName=N'{name}'") == 0) { MessageBox.Show("Không tìm thấy tài khoản."); return; }
            LoadAccounts();
        }
        catch (Exception ex) { MessageBox.Show($"Không cập nhật được tài khoản: {ex.Message}"); }
    }

    private void btnResetPassword_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtAccount.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
        { MessageBox.Show("Chọn tài khoản và nhập mật khẩu mới."); return; }
        try
        {
            var name = Program.SqlText(txtAccount.Text.Trim());
            var password = Program.SqlText(txtPassword.Text);
            if (Database.ExecuteNonQuery($"UPDATE Account SET [Password]=N'{password}' WHERE AccountName=N'{name}'") == 0) { MessageBox.Show("Không tìm thấy tài khoản."); return; }
            txtPassword.Clear(); MessageBox.Show("Đã đặt lại mật khẩu.");
        }
        catch (Exception ex) { MessageBox.Show($"Không đặt lại được mật khẩu: {ex.Message}"); }
    }

    private void tsmDisable_Click(object sender, EventArgs e)
    {
        if (dgvAccounts.CurrentRow?.DataBoundItem is not DataRowView row) return;
        var name = row["Tài khoản"].ToString()!;
        if (MessageBox.Show($"Vô hiệu hóa mọi vai trò của {name}?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
        try
        {
            Database.ExecuteNonQuery($"UPDATE RoleAccount SET Actived=0 WHERE AccountName=N'{Program.SqlText(name)}'");
            LoadAccounts();
        }
        catch (Exception ex) { MessageBox.Show($"Không vô hiệu hóa được tài khoản: {ex.Message}"); }
    }

    private void tsmRoles_Click(object sender, EventArgs e)
    {
        if (dgvAccounts.CurrentRow?.DataBoundItem is not DataRowView row) return;
        using var form = new AccountRolesForm(row["Tài khoản"].ToString()!);
        form.ShowDialog(this);
    }

    private void dgvAccounts_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
            dgvAccounts.CurrentCell = e.RowIndex >= 0 ? dgvAccounts.Rows[e.RowIndex].Cells[0] : null;
    }
}
