namespace Bai2;

partial class BillDetailsForm
{
    private System.ComponentModel.IContainer components = null;
    private Label lblHeader;
    private DataGridView dgvDetails;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        lblHeader = new Label();
        dgvDetails = new DataGridView();
        ((System.ComponentModel.ISupportInitialize)dgvDetails).BeginInit();
        SuspendLayout();
        lblHeader.Name = "lblHeader";
        lblHeader.Dock = DockStyle.Top;
        lblHeader.Height = 55;
        lblHeader.Padding = new Padding(12);
        lblHeader.TextAlign = ContentAlignment.MiddleLeft;
        dgvDetails.Name = "dgvDetails";
        dgvDetails.Dock = DockStyle.Fill;
        dgvDetails.ReadOnly = true;
        dgvDetails.AllowUserToAddRows = false;
        dgvDetails.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        ClientSize = new Size(760, 440);
        Controls.Add(dgvDetails);
        Controls.Add(lblHeader);
        Name = "BillDetailsForm";
        Text = "Chi tiết hóa đơn";
        Load += BillDetailsForm_Load;
        ((System.ComponentModel.ISupportInitialize)dgvDetails).EndInit();
        ResumeLayout(false);
    }
    #endregion
}
