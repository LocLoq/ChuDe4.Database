namespace Bai2;

partial class BillsForm
{
    private System.ComponentModel.IContainer components = null;
    private DateTimePicker dtFrom;
    private DateTimePicker dtTo;
    private Button btnLoad;
    private DataGridView dgvBills;
    private Label lblTotals;
    private Label lblFrom;
    private Label lblTo;
    private FlowLayoutPanel filters;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        dtFrom = new DateTimePicker();
        dtTo = new DateTimePicker();
        btnLoad = new Button();
        dgvBills = new DataGridView();
        lblTotals = new Label();
        lblFrom = new Label();
        lblTo = new Label();
        filters = new FlowLayoutPanel();
        ((System.ComponentModel.ISupportInitialize)dgvBills).BeginInit();
        SuspendLayout();
        dtFrom.Name = "dtFrom";
        dtFrom.Format = DateTimePickerFormat.Short;
        dtFrom.Width = 120;
        dtTo.Name = "dtTo";
        dtTo.Format = DateTimePickerFormat.Short;
        dtTo.Width = 120;
        btnLoad.Name = "btnLoad";
        btnLoad.Text = "Lấy danh sách";
        btnLoad.Width = 110;
        btnLoad.Click += btnLoad_Click;
        lblFrom.Name = "lblFrom";
        lblFrom.Text = "Từ ngày";
        lblFrom.AutoSize = true;
        lblFrom.Padding = new Padding(0, 6, 0, 0);
        lblTo.Name = "lblTo";
        lblTo.Text = "Đến ngày";
        lblTo.AutoSize = true;
        lblTo.Padding = new Padding(12, 6, 0, 0);
        filters.Name = "filters";
        filters.Dock = DockStyle.Top;
        filters.Height = 48;
        filters.Padding = new Padding(8);
        filters.Controls.AddRange(new Control[] { lblFrom, dtFrom, lblTo, dtTo, btnLoad });
        dgvBills.Name = "dgvBills";
        dgvBills.Dock = DockStyle.Fill;
        dgvBills.ReadOnly = true;
        dgvBills.AllowUserToAddRows = false;
        dgvBills.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvBills.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvBills.MultiSelect = false;
        dgvBills.CellDoubleClick += dgvBills_CellDoubleClick;
        lblTotals.Name = "lblTotals";
        lblTotals.Dock = DockStyle.Bottom;
        lblTotals.Height = 38;
        lblTotals.TextAlign = ContentAlignment.MiddleRight;
        lblTotals.Padding = new Padding(8);
        ClientSize = new Size(950, 520);
        Controls.Add(dgvBills);
        Controls.Add(lblTotals);
        Controls.Add(filters);
        Name = "BillsForm";
        Text = "Danh sách hóa đơn";
        Load += BillsForm_Load;
        ((System.ComponentModel.ISupportInitialize)dgvBills).EndInit();
        ResumeLayout(false);
    }
    #endregion
}
