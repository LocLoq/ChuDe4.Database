namespace Bai4;

partial class TableBillsForm
{
    private ListBox lstBills = null!;
    private DataGridView dgvDetails = null!;
    private Label lblBill = null!;

    private void InitializeComponent()
    {
        var split = new SplitContainer { Dock = DockStyle.Fill, Size = new Size(930, 480), SplitterDistance = 260 };
        lstBills = new ListBox { Dock = DockStyle.Fill };
        lstBills.SelectedIndexChanged += lstBills_SelectedIndexChanged;
        split.Panel1.Controls.Add(lstBills);
        lblBill = new Label { Dock = DockStyle.Top, Height = 62, Padding = new Padding(8), TextAlign = ContentAlignment.MiddleLeft };
        dgvDetails = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
        split.Panel2.Controls.Add(dgvDetails); split.Panel2.Controls.Add(lblBill);
        ClientSize = new Size(930, 480);
        Controls.Add(split);
        Load += TableBillsForm_Load;
    }
}
