using Microsoft.Data.SqlClient;
using System.Data;

namespace Bai4;

public partial class MainForm : Form
{
    public MainForm() => InitializeComponent();

    private void MainForm_Load(object sender, EventArgs e) => LoadTables();

    private void LoadTables()
    {
        try
        {
            using var reader = Database.ExecuteQuery("SELECT ID, Name AS [Tên bàn], Status AS [Trạng thái], Capacity AS [Số chỗ] FROM dbo.[Table] ORDER BY Name");
            var table = new DataTable();
            table.Load(reader);
            dgvTables.DataSource = table;
            ClearInput();
        }
        catch (Exception ex) { MessageBox.Show($"Không tải được bàn: {ex.Message}"); }
    }

    private void ClearInput()
    {
        dgvTables.CurrentCell = null;
        dgvTables.ClearSelection();
        txtID.Clear();
        txtName.Clear();
        txtSeats.Clear();
        cmbStatus.SelectedIndex = 0;
    }

    private void btnNew_Click(object sender, EventArgs e) => ClearInput();

    private void dgvTables_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvTables.CurrentRow?.DataBoundItem is not DataRowView row) return;
        txtID.Text = row["ID"].ToString();
        txtName.Text = row["Tên bàn"].ToString();
        cmbStatus.SelectedIndex = Convert.ToInt32(row["Trạng thái"]);
        txtSeats.Text = row["Số chỗ"] == DBNull.Value ? "" : row["Số chỗ"].ToString();
    }

    private bool ValidInput(out string name, out string capacity)
    {
        name = Program.SqlText(txtName.Text.Trim());
        capacity = "NULL";
        if (name.Length == 0 || cmbStatus.SelectedIndex < 0 ||
            (txtSeats.TextLength > 0 && (!int.TryParse(txtSeats.Text, out var seats) || seats <= 0)))
        {
            MessageBox.Show("Nhập tên bàn, trạng thái và số chỗ hợp lệ.");
            return false;
        }
        if (txtSeats.TextLength > 0) capacity = int.Parse(txtSeats.Text).ToString();
        return true;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        if (!ValidInput(out var name, out var capacity)) return;
        try
        {
            Database.ExecuteNonQuery($"INSERT dbo.[Table] (Name, Status, Capacity) VALUES (N'{name}', {cmbStatus.SelectedIndex}, {capacity})");
            LoadTables();
        }
        catch (Exception ex) { MessageBox.Show($"Không thêm được bàn: {ex.Message}"); }
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!int.TryParse(txtID.Text, out var id) || !ValidInput(out var name, out var capacity)) return;
        try
        {
            Database.ExecuteNonQuery($"UPDATE dbo.[Table] SET Name=N'{name}', Status={cmbStatus.SelectedIndex}, Capacity={capacity} WHERE ID={id}");
            LoadTables();
        }
        catch (Exception ex) { MessageBox.Show($"Không cập nhật được bàn: {ex.Message}"); }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (!int.TryParse(txtID.Text, out var id)) return;
        if (MessageBox.Show("Xóa bàn đã chọn?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
        try
        {
            if (Database.ExecuteNonQuery($"DELETE dbo.[Table] WHERE ID={id} AND NOT EXISTS (SELECT 1 FROM Bills WHERE TableID={id})") == 0)
            {
                MessageBox.Show("Bàn đã có hóa đơn nên không thể xóa.");
                return;
            }
            LoadTables();
        }
        catch (SqlException ex) when (ex.Number == 547) { MessageBox.Show("Bàn đã có hóa đơn nên không thể xóa."); }
        catch (Exception ex) { MessageBox.Show($"Không xóa được bàn: {ex.Message}"); }
    }

    private int? SelectedTableId() => int.TryParse(txtID.Text, out var id) ? id : null;

    private void btnCurrentBill_Click(object sender, EventArgs e)
    {
        if (SelectedTableId() is not int id) return;
        try
        {
            int billId;
            using (var reader = Database.ExecuteQuery($"SELECT TOP (1) ID FROM Bills WHERE TableID={id} AND Status=0 ORDER BY ID DESC"))
            {
                if (!reader.Read()) { MessageBox.Show("Bàn chưa có hóa đơn hiện tại."); return; }
                billId = reader.GetInt32(0);
            }
            using var form = new BillDetailsForm(billId);
            form.ShowDialog(this);
        }
        catch (Exception ex) { MessageBox.Show($"Không xem được hóa đơn: {ex.Message}"); }
    }

    private void tsmDelete_Click(object sender, EventArgs e) => btnDelete_Click(sender, e);

    private void tsmBills_Click(object sender, EventArgs e)
    {
        if (SelectedTableId() is not int id) return;
        using var form = new TableBillsForm(id, txtName.Text);
        form.ShowDialog(this);
    }

    private void tsmJournal_Click(object sender, EventArgs e)
    {
        if (SelectedTableId() is not int id) return;
        using var form = new TableBillJournalForm(id, txtName.Text);
        form.ShowDialog(this);
    }

    private void dgvTables_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right) return;
        dgvTables.CurrentCell = e.RowIndex >= 0 ? dgvTables.Rows[e.RowIndex].Cells[0] : null;
        if (e.RowIndex < 0) ClearInput();
    }
}
