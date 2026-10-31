namespace Bai4;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private DataGridView dgvTables;
    private TextBox txtID;
    private TextBox txtName;
    private TextBox txtSeats;
    private ComboBox cmbStatus;
    private TableLayoutPanel inputs;
    private FlowLayoutPanel buttons;
    private Button btnNew;
    private Button btnAdd;
    private Button btnUpdate;
    private Button btnDelete;
    private Button btnCurrent;
    private ContextMenuStrip contextMenu;
    private ToolStripMenuItem delete;
    private ToolStripMenuItem history;
    private ToolStripMenuItem journal;
    private Label lblID;
    private Label lblName;
    private Label lblStatus;
    private Label lblSeats;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        dgvTables = new DataGridView();
        contextMenu = new ContextMenuStrip(components);
        delete = new ToolStripMenuItem();
        history = new ToolStripMenuItem();
        journal = new ToolStripMenuItem();
        txtID = new TextBox();
        txtName = new TextBox();
        txtSeats = new TextBox();
        cmbStatus = new ComboBox();
        inputs = new TableLayoutPanel();
        lblID = new Label();
        lblName = new Label();
        lblStatus = new Label();
        lblSeats = new Label();
        buttons = new FlowLayoutPanel();
        btnNew = new Button();
        btnAdd = new Button();
        btnUpdate = new Button();
        btnDelete = new Button();
        btnCurrent = new Button();
        ((System.ComponentModel.ISupportInitialize)dgvTables).BeginInit();
        contextMenu.SuspendLayout();
        inputs.SuspendLayout();
        buttons.SuspendLayout();
        SuspendLayout();
        // 
        // dgvTables
        // 
        dgvTables.AllowUserToAddRows = false;
        dgvTables.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvTables.ContextMenuStrip = contextMenu;
        dgvTables.Dock = DockStyle.Fill;
        dgvTables.Location = new Point(0, 147);
        dgvTables.MultiSelect = false;
        dgvTables.Name = "dgvTables";
        dgvTables.ReadOnly = true;
        dgvTables.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvTables.Size = new Size(950, 413);
        dgvTables.TabIndex = 1;
        dgvTables.CellMouseDown += dgvTables_CellMouseDown;
        dgvTables.SelectionChanged += dgvTables_SelectionChanged;
        // 
        // contextMenu
        // 
        contextMenu.Items.AddRange(new ToolStripItem[] { delete, history, journal });
        contextMenu.Name = "contextMenu";
        contextMenu.Size = new Size(203, 70);
        // 
        // delete
        // 
        delete.Name = "delete";
        delete.Size = new Size(202, 22);
        delete.Text = "Xóa bàn";
        delete.Click += tsmDelete_Click;
        // 
        // history
        // 
        history.Name = "history";
        history.Size = new Size(202, 22);
        history.Text = "Xem danh mục hóa đơn";
        history.Click += tsmBills_Click;
        // 
        // journal
        // 
        journal.Name = "journal";
        journal.Size = new Size(202, 22);
        journal.Text = "Xem nhật ký hóa đơn";
        journal.Click += tsmJournal_Click;
        // 
        // txtID
        // 
        txtID.Dock = DockStyle.Fill;
        txtID.Location = new Point(151, 11);
        txtID.Name = "txtID";
        txtID.ReadOnly = true;
        txtID.Size = new Size(320, 23);
        txtID.TabIndex = 1;
        // 
        // txtName
        // 
        txtName.Dock = DockStyle.Fill;
        txtName.Location = new Point(617, 11);
        txtName.Name = "txtName";
        txtName.Size = new Size(322, 23);
        txtName.TabIndex = 3;
        // 
        // txtSeats
        // 
        txtSeats.Dock = DockStyle.Fill;
        txtSeats.Location = new Point(617, 45);
        txtSeats.Name = "txtSeats";
        txtSeats.Size = new Size(322, 23);
        txtSeats.TabIndex = 7;
        // 
        // cmbStatus
        // 
        cmbStatus.Dock = DockStyle.Fill;
        cmbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
        cmbStatus.Items.AddRange(new object[] { "Chưa đặt", "Đã đặt", "Có khách" });
        cmbStatus.Location = new Point(151, 45);
        cmbStatus.Name = "cmbStatus";
        cmbStatus.Size = new Size(320, 23);
        cmbStatus.TabIndex = 5;
        // 
        // inputs
        // 
        inputs.ColumnCount = 4;
        inputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
        inputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
        inputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 15F));
        inputs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35F));
        inputs.Controls.Add(lblID, 0, 0);
        inputs.Controls.Add(txtID, 1, 0);
        inputs.Controls.Add(lblName, 2, 0);
        inputs.Controls.Add(txtName, 3, 0);
        inputs.Controls.Add(lblStatus, 0, 1);
        inputs.Controls.Add(cmbStatus, 1, 1);
        inputs.Controls.Add(lblSeats, 2, 1);
        inputs.Controls.Add(txtSeats, 3, 1);
        inputs.Dock = DockStyle.Top;
        inputs.Location = new Point(0, 0);
        inputs.Name = "inputs";
        inputs.Padding = new Padding(8);
        inputs.RowCount = 2;
        inputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 34F));
        inputs.RowStyles.Add(new RowStyle(SizeType.Absolute, 8F));
        inputs.Size = new Size(950, 102);
        inputs.TabIndex = 3;
        // 
        // lblID
        // 
        lblID.AutoSize = true;
        lblID.Location = new Point(11, 8);
        lblID.Name = "lblID";
        lblID.Size = new Size(39, 15);
        lblID.TabIndex = 0;
        lblID.Text = "Mã số";
        // 
        // lblName
        // 
        lblName.AutoSize = true;
        lblName.Location = new Point(477, 8);
        lblName.Name = "lblName";
        lblName.Size = new Size(49, 15);
        lblName.TabIndex = 2;
        lblName.Text = "Tên bàn";
        // 
        // lblStatus
        // 
        lblStatus.AutoSize = true;
        lblStatus.Location = new Point(11, 42);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(60, 15);
        lblStatus.TabIndex = 4;
        lblStatus.Text = "Trạng thái";
        // 
        // lblSeats
        // 
        lblSeats.AutoSize = true;
        lblSeats.Location = new Point(477, 42);
        lblSeats.Name = "lblSeats";
        lblSeats.Size = new Size(43, 15);
        lblSeats.TabIndex = 6;
        lblSeats.Text = "Số chỗ";
        // 
        // buttons
        // 
        buttons.Controls.Add(btnNew);
        buttons.Controls.Add(btnAdd);
        buttons.Controls.Add(btnUpdate);
        buttons.Controls.Add(btnDelete);
        buttons.Controls.Add(btnCurrent);
        buttons.Dock = DockStyle.Top;
        buttons.Location = new Point(0, 102);
        buttons.Name = "buttons";
        buttons.Padding = new Padding(8);
        buttons.Size = new Size(950, 45);
        buttons.TabIndex = 2;
        // 
        // btnNew
        // 
        btnNew.Location = new Point(11, 11);
        btnNew.Name = "btnNew";
        btnNew.Size = new Size(90, 23);
        btnNew.TabIndex = 0;
        btnNew.Text = "Nhập mới";
        btnNew.Click += btnNew_Click;
        // 
        // btnAdd
        // 
        btnAdd.Location = new Point(107, 11);
        btnAdd.Name = "btnAdd";
        btnAdd.Size = new Size(90, 23);
        btnAdd.TabIndex = 1;
        btnAdd.Text = "Thêm bàn";
        btnAdd.Click += btnAdd_Click;
        // 
        // btnUpdate
        // 
        btnUpdate.Location = new Point(203, 11);
        btnUpdate.Name = "btnUpdate";
        btnUpdate.Size = new Size(90, 23);
        btnUpdate.TabIndex = 2;
        btnUpdate.Text = "Cập nhật";
        btnUpdate.Click += btnUpdate_Click;
        // 
        // btnDelete
        // 
        btnDelete.Location = new Point(299, 11);
        btnDelete.Name = "btnDelete";
        btnDelete.Size = new Size(80, 23);
        btnDelete.TabIndex = 3;
        btnDelete.Text = "Xóa";
        btnDelete.Click += btnDelete_Click;
        // 
        // btnCurrent
        // 
        btnCurrent.Location = new Point(385, 11);
        btnCurrent.Name = "btnCurrent";
        btnCurrent.Size = new Size(135, 23);
        btnCurrent.TabIndex = 4;
        btnCurrent.Text = "Hóa đơn hiện tại";
        btnCurrent.Click += btnCurrentBill_Click;
        // 
        // MainForm
        // 
        ClientSize = new Size(950, 560);
        Controls.Add(dgvTables);
        Controls.Add(buttons);
        Controls.Add(inputs);
        Name = "MainForm";
        Text = "Quản lý bàn";
        Load += MainForm_Load;
        ((System.ComponentModel.ISupportInitialize)dgvTables).EndInit();
        contextMenu.ResumeLayout(false);
        inputs.ResumeLayout(false);
        inputs.PerformLayout();
        buttons.ResumeLayout(false);
        ResumeLayout(false);
    }
    #endregion
}
