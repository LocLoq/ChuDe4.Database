namespace Bai4;

partial class TableBillJournalForm
{
    private System.ComponentModel.IContainer components = null;
    private Label lblSummary;
    private DataGridView dgvJournal;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        lblSummary = new Label();
        dgvJournal = new DataGridView();
        ((System.ComponentModel.ISupportInitialize)dgvJournal).BeginInit();
        SuspendLayout();
        lblSummary.Name = "lblSummary";
        lblSummary.Dock = DockStyle.Top;
        lblSummary.Height = 48;
        lblSummary.Padding = new Padding(10);
        lblSummary.TextAlign = ContentAlignment.MiddleLeft;
        dgvJournal.Name = "dgvJournal";
        dgvJournal.Dock = DockStyle.Fill;
        dgvJournal.ReadOnly = true;
        dgvJournal.AllowUserToAddRows = false;
        dgvJournal.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        ClientSize = new Size(920, 480);
        Controls.Add(dgvJournal);
        Controls.Add(lblSummary);
        Name = "TableBillJournalForm";
        Load += TableBillJournalForm_Load;
        ((System.ComponentModel.ISupportInitialize)dgvJournal).EndInit();
        ResumeLayout(false);
    }
    #endregion
}
