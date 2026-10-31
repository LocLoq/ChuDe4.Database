namespace Bai3;

partial class AccountRolesForm
{
    private System.ComponentModel.IContainer components = null;
    private DataGridView dgvRoles;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        dgvRoles = new DataGridView();
        ((System.ComponentModel.ISupportInitialize)dgvRoles).BeginInit();
        SuspendLayout();
        dgvRoles.Name = "dgvRoles";
        dgvRoles.Dock = DockStyle.Fill;
        dgvRoles.ReadOnly = true;
        dgvRoles.AllowUserToAddRows = false;
        dgvRoles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        ClientSize = new Size(600, 360);
        Controls.Add(dgvRoles);
        Name = "AccountRolesForm";
        Load += AccountRolesForm_Load;
        ((System.ComponentModel.ISupportInitialize)dgvRoles).EndInit();
        ResumeLayout(false);
    }
    #endregion
}
