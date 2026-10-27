namespace Bai4;

partial class BillDetailsForm
{
    private Label lblHeader = null!;
    private DataGridView dgvDetails = null!;

    private void InitializeComponent()
    {
        lblHeader = new Label { Dock = DockStyle.Top, Height = 55, Padding = new Padding(12), TextAlign = ContentAlignment.MiddleLeft };
        dgvDetails = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
        ClientSize = new Size(760, 440);
        Controls.Add(dgvDetails); Controls.Add(lblHeader);
        Text = "Chi tiết hóa đơn";
        Load += BillDetailsForm_Load;
    }
}
