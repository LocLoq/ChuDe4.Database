namespace Bai3;

partial class AccountManager
{
    private ComboBox cmbRoleFilter = null!, cmbStatus = null!, cmbNewRole = null!;
    private TextBox txtAccount = null!, txtPassword = null!, txtFullName = null!, txtEmail = null!, txtPhone = null!;
    private DataGridView dgvAccounts = null!;

    private void InitializeComponent()
    {
        cmbRoleFilter = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 170 };
        cmbStatus = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Width = 140 };
        cmbStatus.Items.AddRange(new object[] { "Tất cả trạng thái", "Đang hoạt động", "Không hoạt động" });
        var btnFilter = new Button { Text = "Lọc", Width = 75 };
        btnFilter.Click += btnFilter_Click;
        var filters = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 45, Padding = new Padding(8) };
        filters.Controls.AddRange(new Control[] { new Label { Text = "Nhóm", AutoSize = true, Padding = new Padding(0, 6, 0, 0) }, cmbRoleFilter,
            new Label { Text = "Trạng thái", AutoSize = true, Padding = new Padding(12, 6, 0, 0) }, cmbStatus, btnFilter });
        txtAccount = new TextBox(); txtPassword = new TextBox { UseSystemPasswordChar = true };
        txtFullName = new TextBox(); txtEmail = new TextBox(); txtPhone = new TextBox();
        cmbNewRole = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        var inputs = new TableLayoutPanel { Dock = DockStyle.Top, Height = 105, Padding = new Padding(8), ColumnCount = 6, RowCount = 2 };
        for (int i = 0; i < 6; i++) inputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, i % 2 == 0 ? 10 : 23.33f));
        inputs.Controls.Add(new Label { Text = "Tài khoản", AutoSize = true }, 0, 0); inputs.Controls.Add(txtAccount, 1, 0);
        inputs.Controls.Add(new Label { Text = "Họ tên", AutoSize = true }, 2, 0); inputs.Controls.Add(txtFullName, 3, 0);
        inputs.Controls.Add(new Label { Text = "Vai trò mới", AutoSize = true }, 4, 0); inputs.Controls.Add(cmbNewRole, 5, 0);
        inputs.Controls.Add(new Label { Text = "Mật khẩu", AutoSize = true }, 0, 1); inputs.Controls.Add(txtPassword, 1, 1);
        inputs.Controls.Add(new Label { Text = "Email", AutoSize = true }, 2, 1); inputs.Controls.Add(txtEmail, 3, 1);
        inputs.Controls.Add(new Label { Text = "Điện thoại", AutoSize = true }, 4, 1); inputs.Controls.Add(txtPhone, 5, 1);
        foreach (Control control in inputs.Controls) if (control is TextBox or ComboBox) control.Dock = DockStyle.Fill;
        var btnNew = new Button { Text = "Nhập mới", Width = 95 };
        var btnAdd = new Button { Text = "Thêm", Width = 90 };
        var btnUpdate = new Button { Text = "Cập nhật", Width = 95 };
        var btnReset = new Button { Text = "Đặt lại mật khẩu", Width = 140 };
        btnNew.Click += (_, _) => { dgvAccounts.CurrentCell = null; dgvAccounts.ClearSelection(); txtAccount.Clear(); txtPassword.Clear(); txtFullName.Clear(); txtEmail.Clear(); txtPhone.Clear(); };
        btnAdd.Click += btnAdd_Click; btnUpdate.Click += btnUpdate_Click; btnReset.Click += btnResetPassword_Click;
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 45, Padding = new Padding(8) };
        buttons.Controls.AddRange(new Control[] { btnNew, btnAdd, btnUpdate, btnReset });
        var contextMenu = new ContextMenuStrip();
        var disable = new ToolStripMenuItem("Xóa tài khoản"); disable.Click += tsmDisable_Click;
        var roles = new ToolStripMenuItem("Xem danh sách vai trò"); roles.Click += tsmRoles_Click;
        contextMenu.Items.AddRange(new ToolStripItem[] { disable, roles });
        dgvAccounts = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, MultiSelect = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            ContextMenuStrip = contextMenu };
        dgvAccounts.SelectionChanged += dgvAccounts_SelectionChanged;
        dgvAccounts.CellMouseDown += dgvAccounts_CellMouseDown;
        ClientSize = new Size(1000, 600);
        Controls.Add(dgvAccounts); Controls.Add(buttons); Controls.Add(inputs); Controls.Add(filters);
        Text = "Quản lý tài khoản";
        Load += AccountManager_Load;
    }
}
