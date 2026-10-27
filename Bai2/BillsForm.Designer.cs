namespace Bai2;

partial class BillsForm
{
    private DateTimePicker dtFrom = null!, dtTo = null!;
    private Button btnLoad = null!;
    private DataGridView dgvBills = null!;
    private Label lblTotals = null!;

    private void InitializeComponent()
    {
        dtFrom = new DateTimePicker { Format = DateTimePickerFormat.Short, Width = 120, Value = DateTime.Today.AddDays(-30) };
        dtTo = new DateTimePicker { Format = DateTimePickerFormat.Short, Width = 120 };
        btnLoad = new Button { Text = "Lấy danh sách", Width = 110 };
        btnLoad.Click += btnLoad_Click;
        var filters = new FlowLayoutPanel { Dock = DockStyle.Top, Height = 48, Padding = new Padding(8) };
        filters.Controls.AddRange(new Control[] { new Label { Text = "Từ ngày", AutoSize = true, Padding = new Padding(0, 6, 0, 0) }, dtFrom,
            new Label { Text = "Đến ngày", AutoSize = true, Padding = new Padding(12, 6, 0, 0) }, dtTo, btnLoad });
        dgvBills = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false };
        dgvBills.CellDoubleClick += dgvBills_CellDoubleClick;
        lblTotals = new Label { Dock = DockStyle.Bottom, Height = 38, TextAlign = ContentAlignment.MiddleRight, Padding = new Padding(8) };
        ClientSize = new Size(950, 520);
        Controls.Add(dgvBills); Controls.Add(lblTotals); Controls.Add(filters);
        Text = "Danh sách hóa đơn";
        Load += BillsForm_Load;
    }
}
