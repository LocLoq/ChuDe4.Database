namespace Bai4;

partial class TableBillsForm
{
    private System.ComponentModel.IContainer components = null;
    private SplitContainer split;
    private ListBox lstBills;
    private DataGridView dgvDetails;
    private Label lblBill;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        split = new SplitContainer();
        lstBills = new ListBox();
        dgvDetails = new DataGridView();
        lblBill = new Label();
        ((System.ComponentModel.ISupportInitialize)split).BeginInit();
        split.Panel1.SuspendLayout();
        split.Panel2.SuspendLayout();
        split.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvDetails).BeginInit();
        SuspendLayout();
        split.Name = "split";
        split.Dock = DockStyle.Fill;
        split.Size = new Size(930, 480);
        split.SplitterDistance = 260;
        split.Panel1.Controls.Add(lstBills);
        split.Panel2.Controls.Add(dgvDetails);
        split.Panel2.Controls.Add(lblBill);
        lstBills.Name = "lstBills";
        lstBills.Dock = DockStyle.Fill;
        lstBills.SelectedIndexChanged += lstBills_SelectedIndexChanged;
        lblBill.Name = "lblBill";
        lblBill.Dock = DockStyle.Top;
        lblBill.Height = 62;
        lblBill.Padding = new Padding(8);
        lblBill.TextAlign = ContentAlignment.MiddleLeft;
        dgvDetails.Name = "dgvDetails";
        dgvDetails.Dock = DockStyle.Fill;
        dgvDetails.ReadOnly = true;
        dgvDetails.AllowUserToAddRows = false;
        dgvDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        ClientSize = new Size(930, 480);
        Controls.Add(split);
        Name = "TableBillsForm";
        Load += TableBillsForm_Load;
        ((System.ComponentModel.ISupportInitialize)dgvDetails).EndInit();
        split.Panel1.ResumeLayout(false);
        split.Panel2.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)split).EndInit();
        split.ResumeLayout(false);
        ResumeLayout(false);
    }
    #endregion
}
