namespace Bai4;

partial class TableBillJournalForm
{
    private Label lblSummary = null!;
    private DataGridView dgvJournal = null!;
    private void InitializeComponent()
    {
        lblSummary = new Label { Dock = DockStyle.Top, Height = 48, Padding = new Padding(10), TextAlign = ContentAlignment.MiddleLeft };
        dgvJournal = new DataGridView { Dock = DockStyle.Fill, ReadOnly = true, AllowUserToAddRows = false,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
        ClientSize = new Size(920, 480);
        Controls.Add(dgvJournal); Controls.Add(lblSummary);
        Load += TableBillJournalForm_Load;
    }
}
