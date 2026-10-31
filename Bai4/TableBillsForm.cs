using System.Data;

namespace Bai4;

public partial class TableBillsForm : Form
{
    private readonly int _tableId;
    private sealed record BillItem(int Id, DateTime? Date)
    { public override string ToString() => $"{(Date.HasValue ? Date.Value.ToString("dd/MM/yyyy HH:mm") : "Chưa có ngày")} - #{Id}"; }

    public TableBillsForm() : this(0, string.Empty) { }
    public TableBillsForm(int tableId, string tableCode)
    { _tableId = tableId; InitializeComponent(); Text = $"Danh mục hóa đơn - {tableCode}"; }

    private void TableBillsForm_Load(object sender, EventArgs e)
    {
        try
        {
            using (var reader = Database.ExecuteQuery($"SELECT ID, CheckoutDate FROM Bills WHERE TableID={_tableId} ORDER BY CheckoutDate DESC, ID DESC"))
                while (reader.Read()) lstBills.Items.Add(new BillItem(reader.GetInt32(0), reader.IsDBNull(1) ? null : reader.GetDateTime(1)));
            if (lstBills.Items.Count > 0) lstBills.SelectedIndex = 0;
        }
        catch (Exception ex) { MessageBox.Show($"Không lấy được danh mục hóa đơn: {ex.Message}"); }
    }

    private void lstBills_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (lstBills.SelectedItem is not BillItem item) return;
        try
        {
            using (var reader = Database.ExecuteQuery($"SELECT Name, Amount * (1 - COALESCE(Discount,0) + COALESCE(Tax,0)), Amount * COALESCE(Discount,0), Amount * COALESCE(Tax,0), Status FROM Bills WHERE ID={item.Id}"))
            {
                if (reader.Read()) lblBill.Text = $"#{item.Id} - {reader.GetString(0)} | Thực thu: {Convert.ToDecimal(reader.GetValue(1)):N0} | Giảm: {Convert.ToDecimal(reader.GetValue(2)):N0} | Thuế: {Convert.ToDecimal(reader.GetValue(3)):N0} | {(reader.GetBoolean(4) ? "Đã trả" : "Chưa trả")}";
            }
            using var details = Database.ExecuteQuery($"SELECT f.Name AS [Món ăn], d.Quantity AS [Số lượng], f.Price AS [Giá hiện tại] FROM BillDetails d JOIN Food f ON f.ID=d.FoodID WHERE d.InvoiceID={item.Id} ORDER BY d.ID");
            var table = new DataTable(); table.Load(details); dgvDetails.DataSource = table;
        }
        catch (Exception ex) { MessageBox.Show($"Không lấy được chi tiết hóa đơn: {ex.Message}"); }
    }
}
