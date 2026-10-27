namespace Bai3;

partial class AccountRolesForm
{
    private DataGridView dgvRoles = null!;
    private void InitializeComponent()
    {
        dgvRoles = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
        ClientSize = new Size(600, 360);
        Controls.Add(dgvRoles);
        Load += AccountRolesForm_Load;
    }
}
