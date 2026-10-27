namespace HuongDan;

partial class FoodForm
{
    private DataGridView dgvFood = null!;
    private Button btnSave = null!, btnDelete = null!;

    private void InitializeComponent()
    {
        dgvFood = new DataGridView { Dock = DockStyle.Fill, Name = "dgvFood", AutoGenerateColumns = false,
            AllowUserToAddRows = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect };
        dgvFood.Columns.AddRange(
            new DataGridViewTextBoxColumn { Name = "colId", HeaderText = "Mã món", DataPropertyName = "ID", ReadOnly = true, Width = 80 },
            new DataGridViewTextBoxColumn { Name = "colName", HeaderText = "Tên món", DataPropertyName = "Name", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill },
            new DataGridViewTextBoxColumn { Name = "colCategoryId", DataPropertyName = "CategoryID", Visible = false },
            new DataGridViewTextBoxColumn { Name = "colPrice", HeaderText = "Giá", DataPropertyName = "Price", Width = 120 },
            new DataGridViewTextBoxColumn { Name = "colNotes", HeaderText = "Ghi chú", DataPropertyName = "Notes", Width = 220 });
        dgvFood.DefaultValuesNeeded += dgvFood_DefaultValuesNeeded;
        dgvFood.DataError += dgvFood_DataError;
        btnSave = new Button { Text = "Save", Width = 90 };
        btnDelete = new Button { Text = "Delete", Width = 90 };
        btnSave.Click += btnSave_Click;
        btnDelete.Click += btnDelete_Click;
        var buttons = new FlowLayoutPanel { Dock = DockStyle.Bottom, Height = 46, FlowDirection = FlowDirection.RightToLeft, Padding = new Padding(8) };
        buttons.Controls.AddRange(new Control[] { btnDelete, btnSave });
        ClientSize = new Size(800, 480);
        Controls.Add(dgvFood); Controls.Add(buttons);
        Load += FoodForm_Load;
    }
}
