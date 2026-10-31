using Microsoft.Data.SqlClient;
using System.Data;

namespace HuongDan;

public partial class FoodForm : Form
{
    private readonly int _categoryId;
    private DataTable _foods = new();

    public FoodForm() : this(0, string.Empty) { }

    public FoodForm(int categoryId, string categoryName)
    {
        _categoryId = categoryId;
        InitializeComponent();
        Text = $"Món ăn - {categoryName}";
    }

    private static string SqlText(string value) => value.Replace("'", "''");
    private static string SqlNullable(object value) => value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString())
        ? "NULL" : $"N'{SqlText(value.ToString()!)}'";

    private void FoodForm_Load(object sender, EventArgs e) => LoadFoods();

    private void LoadFoods()
    {
        try
        {
            using var reader = Database.ExecuteQuery($"SELECT ID, Name, Unit, FoodCategoryID, Price, Notes FROM Food WHERE FoodCategoryID={_categoryId} ORDER BY ID");
            _foods = new DataTable();
            _foods.Load(reader);
            dgvFood.DataSource = _foods;
        }
        catch (Exception ex) { MessageBox.Show($"Không tải được món ăn: {ex.Message}"); }
    }

    private void dgvFood_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
    {
        e.Row.Cells["colCategoryId"].Value = _categoryId;
        e.Row.Cells["colPrice"].Value = 0;
    }

    private void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (!dgvFood.EndEdit()) return;
            if (dgvFood.DataSource is object source && BindingContext?[source] is CurrencyManager manager) manager.EndCurrentEdit();
            using var connection = new SqlConnection(Program.ConnectionString);
            connection.Open();
            using var transaction = connection.BeginTransaction();
            foreach (DataRow row in _foods.Rows)
            {
                if (row.RowState is not (DataRowState.Added or DataRowState.Modified)) continue;
                var name = row["Name"].ToString()?.Trim() ?? "";
                var unit = row["Unit"].ToString()?.Trim() ?? "";
                if (name.Length == 0 || unit.Length == 0 || !int.TryParse(row["Price"].ToString(), out var price) || price < 0)
                { MessageBox.Show("Mỗi món cần tên, đơn vị và giá không âm."); return; }
                var notes = SqlNullable(row["Notes"]);
                var statement = row.RowState == DataRowState.Added
                    ? $"INSERT Food (Name, Unit, FoodCategoryID, Price, Notes) VALUES (N'{SqlText(name)}', N'{SqlText(unit)}', {_categoryId}, {price}, {notes})"
                    : $"UPDATE Food SET Name=N'{SqlText(name)}', Unit=N'{SqlText(unit)}', Price={price}, Notes={notes} WHERE ID={Convert.ToInt32(row["ID"])} AND FoodCategoryID={_categoryId}";
                Database.ExecuteNonQuery(statement, connection, transaction);
            }
            transaction.Commit();
            LoadFoods();
        }
        catch (Exception ex) { MessageBox.Show($"Không lưu được món ăn: {ex.Message}"); }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (dgvFood.CurrentRow?.DataBoundItem is not DataRowView view) return;
        if (MessageBox.Show("Xóa món đã chọn?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
        var row = view.Row;
        if (row.RowState == DataRowState.Added) { row.Delete(); return; }
        try
        {
            Database.ExecuteNonQuery($"DELETE Food WHERE ID={Convert.ToInt32(row["ID"])} AND FoodCategoryID={_categoryId}");
            row.Delete();
            row.AcceptChanges();
        }
        catch (SqlException ex) when (ex.Number == 547) { MessageBox.Show("Món đã có trong hóa đơn nên không thể xóa."); }
        catch (Exception ex) { MessageBox.Show($"Không xóa được món ăn: {ex.Message}"); }
    }

    private void dgvFood_DataError(object sender, DataGridViewDataErrorEventArgs e)
    { MessageBox.Show("Giá món phải là số nguyên."); e.ThrowException = false; }
}
