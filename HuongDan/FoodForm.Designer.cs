namespace HuongDan;

partial class FoodForm
{
    private System.ComponentModel.IContainer components = null;
    private DataGridView dgvFood;
    private DataGridViewTextBoxColumn colId;
    private DataGridViewTextBoxColumn colName;
    private DataGridViewTextBoxColumn colCategoryId;
    private DataGridViewTextBoxColumn colUnit;
    private DataGridViewTextBoxColumn colPrice;
    private DataGridViewTextBoxColumn colNotes;
    private Button btnSave;
    private Button btnDelete;
    private FlowLayoutPanel buttons;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null) components.Dispose();
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        dgvFood = new DataGridView();
        colId = new DataGridViewTextBoxColumn();
        colName = new DataGridViewTextBoxColumn();
        colCategoryId = new DataGridViewTextBoxColumn();
        colUnit = new DataGridViewTextBoxColumn();
        colPrice = new DataGridViewTextBoxColumn();
        colNotes = new DataGridViewTextBoxColumn();
        btnSave = new Button();
        btnDelete = new Button();
        buttons = new FlowLayoutPanel();
        ((System.ComponentModel.ISupportInitialize)dgvFood).BeginInit();
        SuspendLayout();
        dgvFood.Dock = DockStyle.Fill;
        dgvFood.Name = "dgvFood";
        dgvFood.AutoGenerateColumns = false;
        dgvFood.AllowUserToAddRows = true;
        dgvFood.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvFood.Columns.AddRange(new DataGridViewColumn[] { colId, colName, colCategoryId, colUnit, colPrice, colNotes });
        dgvFood.DefaultValuesNeeded += dgvFood_DefaultValuesNeeded;
        dgvFood.DataError += dgvFood_DataError;
        colId.Name = "colId";
        colId.HeaderText = "Mã món";
        colId.DataPropertyName = "ID";
        colId.ReadOnly = true;
        colId.Width = 80;
        colName.Name = "colName";
        colName.HeaderText = "Tên món";
        colName.DataPropertyName = "Name";
        colName.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        colCategoryId.Name = "colCategoryId";
        colCategoryId.DataPropertyName = "FoodCategoryID";
        colCategoryId.Visible = false;
        colUnit.Name = "colUnit";
        colUnit.HeaderText = "Đơn vị";
        colUnit.DataPropertyName = "Unit";
        colUnit.Width = 100;
        colPrice.Name = "colPrice";
        colPrice.HeaderText = "Giá";
        colPrice.DataPropertyName = "Price";
        colPrice.Width = 120;
        colNotes.Name = "colNotes";
        colNotes.HeaderText = "Ghi chú";
        colNotes.DataPropertyName = "Notes";
        colNotes.Width = 220;
        btnSave.Name = "btnSave";
        btnSave.Text = "Lưu";
        btnSave.Width = 90;
        btnSave.Click += btnSave_Click;
        btnDelete.Name = "btnDelete";
        btnDelete.Text = "Xóa";
        btnDelete.Width = 90;
        btnDelete.Click += btnDelete_Click;
        buttons.Name = "buttons";
        buttons.Dock = DockStyle.Bottom;
        buttons.Height = 46;
        buttons.FlowDirection = FlowDirection.RightToLeft;
        buttons.Padding = new Padding(8);
        buttons.Controls.AddRange(new Control[] { btnDelete, btnSave });
        ClientSize = new Size(800, 480);
        Controls.Add(dgvFood);
        Controls.Add(buttons);
        Name = "FoodForm";
        Load += FoodForm_Load;
        ((System.ComponentModel.ISupportInitialize)dgvFood).EndInit();
        ResumeLayout(false);
    }
    #endregion
}
