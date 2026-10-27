using Microsoft.Data.SqlClient;
using System.Data;

namespace Bai4;

public partial class MainForm : Form
{
    public MainForm() => InitializeComponent();

    private void MainForm_Load(object sender, EventArgs e)
    { LoadHalls(); LoadTables(); }

    private void LoadHalls()
    {
        try
        {
            using var reader = Database.ExecuteQuery("SELECT ID, Name FROM Hall ORDER BY Name");
            var table = new DataTable(); table.Load(reader);
            cmbHall.DisplayMember = "Name"; cmbHall.ValueMember = "ID"; cmbHall.DataSource = table;
        }
        catch (Exception ex) { MessageBox.Show($"Không tải được sảnh: {ex.Message}"); }
    }

    private void LoadTables()
    {
        try
        {
            using var reader = Database.ExecuteQuery("SELECT t.ID, t.TableCode AS [Mã bàn], t.Name AS [Tên bàn], t.Status AS [Trạng thái], t.Seats AS [Số chỗ], t.HallID, h.Name AS [Sảnh] FROM dbo.[Table] t JOIN Hall h ON h.ID=t.HallID ORDER BY h.Name, t.TableCode");
            var table = new DataTable(); table.Load(reader); dgvTables.DataSource = table;
            if (dgvTables.Columns["HallID"] is DataGridViewColumn hallColumn) hallColumn.Visible = false;
            ClearInput();
        }
        catch (Exception ex) { MessageBox.Show($"Không tải được bàn: {ex.Message}"); }
    }

    private void ClearInput()
    {
        dgvTables.CurrentCell = null;
        dgvTables.ClearSelection();
        txtID.Clear(); txtCode.Clear(); txtName.Clear(); txtSeats.Clear();
        cmbStatus.SelectedIndex = 0;
    }

    private void dgvTables_SelectionChanged(object sender, EventArgs e)
    {
        if (dgvTables.CurrentRow?.DataBoundItem is not DataRowView row) return;
        txtID.Text = row["ID"].ToString(); txtCode.Text = row["Mã bàn"].ToString();
        txtName.Text = row["Tên bàn"].ToString();
        cmbStatus.SelectedIndex = Convert.ToInt32(row["Trạng thái"]);
        txtSeats.Text = row["Số chỗ"] == DBNull.Value ? "" : row["Số chỗ"].ToString();
        cmbHall.SelectedValue = row["HallID"];
    }

    private bool ValidInput(out string values)
    {
        values = "";
        if (string.IsNullOrWhiteSpace(txtCode.Text) || cmbHall.SelectedValue is not int hall ||
            (txtSeats.TextLength > 0 && (!int.TryParse(txtSeats.Text, out var seats) || seats <= 0)))
        { MessageBox.Show("Nhập mã bàn, sảnh và số chỗ hợp lệ."); return false; }
        var code = Program.SqlText(txtCode.Text.Trim());
        var name = string.IsNullOrWhiteSpace(txtName.Text) ? "NULL" : $"N'{Program.SqlText(txtName.Text.Trim())}'";
        var seatText = txtSeats.TextLength == 0 ? "NULL" : txtSeats.Text;
        values = $"N'{code}', {name}, {cmbStatus.SelectedIndex}, {seatText}, {hall}";
        return true;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        if (!ValidInput(out var values)) return;
        try
        {
            Database.ExecuteNonQuery($"INSERT dbo.[Table] (TableCode, Name, Status, Seats, HallID) VALUES ({values})");
            LoadTables();
        }
        catch (Exception ex) { MessageBox.Show($"Không thêm được bàn: {ex.Message}"); }
    }

    private void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!int.TryParse(txtID.Text, out var id) || !ValidInput(out _)) return;
        try
        {
            var code = Program.SqlText(txtCode.Text.Trim());
            var name = string.IsNullOrWhiteSpace(txtName.Text) ? "NULL" : $"N'{Program.SqlText(txtName.Text.Trim())}'";
            var seats = txtSeats.TextLength == 0 ? "NULL" : txtSeats.Text;
            Database.ExecuteNonQuery($"UPDATE dbo.[Table] SET TableCode=N'{code}', Name={name}, Status={cmbStatus.SelectedIndex}, Seats={seats}, HallID={cmbHall.SelectedValue} WHERE ID={id}");
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
            if (Database.ExecuteNonQuery($"DELETE dbo.[Table] WHERE ID={id} AND NOT EXISTS (SELECT 1 FROM Invoice WHERE TableID={id})") == 0) { MessageBox.Show("Bàn đã có hóa đơn nên không thể xóa."); return; }
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
            int invoiceId;
            using (var reader = Database.ExecuteQuery($"SELECT TOP (1) ID FROM Invoice WHERE TableID={id} AND Status=0 ORDER BY CheckoutDate DESC, ID DESC"))
            {
                if (!reader.Read()) { MessageBox.Show("Bàn chưa có hóa đơn hiện tại."); return; }
                invoiceId = reader.GetInt32(0);
            }
            using var form = new BillDetailsForm(invoiceId); form.ShowDialog(this);
        }
        catch (Exception ex) { MessageBox.Show($"Không xem được hóa đơn: {ex.Message}"); }
    }

    private void tsmDelete_Click(object sender, EventArgs e) => btnDelete_Click(sender, e);
    private void tsmBills_Click(object sender, EventArgs e)
    {
        if (SelectedTableId() is not int id) return;
        using var form = new TableBillsForm(id, txtCode.Text); form.ShowDialog(this);
    }
    private void tsmJournal_Click(object sender, EventArgs e)
    {
        if (SelectedTableId() is not int id) return;
        using var form = new TableBillJournalForm(id, txtCode.Text); form.ShowDialog(this);
    }
    private void dgvTables_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right) return;
        dgvTables.CurrentCell = e.RowIndex >= 0 ? dgvTables.Rows[e.RowIndex].Cells[0] : null;
        if (e.RowIndex < 0) ClearInput();
    }
}
