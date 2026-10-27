namespace Bai4;

partial class MainForm
{
    private DataGridView dgvTables = null!;
    private TextBox txtID = null!, txtCode = null!, txtName = null!, txtSeats = null!;
    private ComboBox cmbStatus = null!, cmbHall = null!;

    private void InitializeComponent()
    {
        txtID = new TextBox { ReadOnly = true }; txtCode = new TextBox(); txtName = new TextBox(); txtSeats = new TextBox();
        cmbStatus = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        cmbStatus.Items.AddRange(new object[] { "Chưa đặt", "Đã đặt", "Có khách" });
        cmbHall = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList };
        var inputs = new TableLayoutPanel { Dock = DockStyle.Top, Height = 102, Padding = new Padding(8), ColumnCount = 6, RowCount = 2 };
        for (int i = 0; i < 6; i++) inputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, i % 2 == 0 ? 10 : 23.33f));
        inputs.Controls.Add(new Label { Text = "Mã số", AutoSize = true }, 0, 0); inputs.Controls.Add(txtID, 1, 0);
        inputs.Controls.Add(new Label { Text = "Mã bàn", AutoSize = true }, 2, 0); inputs.Controls.Add(txtCode, 3, 0);
        inputs.Controls.Add(new Label { Text = "Tên bàn", AutoSize = true }, 4, 0); inputs.Controls.Add(txtName, 5, 0);
        inputs.Controls.Add(new Label { Text = "Trạng thái", AutoSize = true }, 0, 1); inputs.Controls.Add(cmbStatus, 1, 1);
        inputs.Controls.Add(new Label { Text = "Số chỗ", AutoSize = true }, 2, 1); inputs.Controls.Add(txtSeats, 3, 1);
        inputs.Controls.Add(new Label { Text = "Sảnh", AutoSize = true }, 4, 1); inputs.Controls.Add(cmbHall, 5, 1);
        foreach (Control control in inputs.Controls) if (control is TextBox or ComboBox) control.Dock = DockStyle.Fill;
        var btnNew = new Button { Text = "Nhập mới", Width = 90 }; btnNew.Click += (_, _) => ClearInput();
        var btnAdd = new Button { Text = "Thêm bàn", Width = 90 }; btnAdd.Click += btnAdd_Click;
        var btnUpdate = new Button { Text = "Cập nhật", Width = 90 }; btnUpdate.Click += btnUpdate_Click;
        var btnDelete = new Button { Text = "Xóa", Width = 80 }; btnDelete.Click += btnDelete_Click;
        var btnCurrent = new Button { Text = "Hóa đơn hiện tại", Width = 135 }; btnCurrent.Click += btnCurrentBill_Click;
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 45, Padding = new Padding(8) };
        buttons.Controls.AddRange(new Control[] { btnNew, btnAdd, btnUpdate, btnDelete, btnCurrent });
        var contextMenu = new ContextMenuStrip();
        var delete = new ToolStripMenuItem("Xóa bàn"); delete.Click += tsmDelete_Click;
        var history = new ToolStripMenuItem("Xem danh mục hóa đơn"); history.Click += tsmBills_Click;
        var journal = new ToolStripMenuItem("Xem nhật ký hóa đơn"); journal.Click += tsmJournal_Click;
        contextMenu.Items.AddRange(new ToolStripItem[] { delete, history, journal });
        dgvTables = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false, MultiSelect = false,
            SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
            ContextMenuStrip = contextMenu };
        dgvTables.SelectionChanged += dgvTables_SelectionChanged; dgvTables.CellMouseDown += dgvTables_CellMouseDown;
        ClientSize = new Size(950, 560);
        Controls.Add(dgvTables); Controls.Add(buttons); Controls.Add(inputs);
        Text = "Quản lý bàn";
        Load += MainForm_Load;
    }
}
