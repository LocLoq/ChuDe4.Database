using System.Data;

namespace Bai4;

public partial class TableBillJournalForm : Form
{
    private readonly int _tableId;
    public TableBillJournalForm(int tableId, string tableCode)
    { _tableId = tableId; InitializeComponent(); Text = $"Nhật ký hóa đơn - {tableCode}"; }

    private void TableBillJournalForm_Load(object sender, EventArgs e)
    {
        try
        {
            var query = $"""
                SELECT i.ID AS [Mã hóa đơn], i.CheckoutDate AS [Ngày lập],
                       a.FullName AS [Nhân viên], i.Total AS [Thực thu],
                       CAST(COALESCE(i.Tax,0) AS decimal(18,2)) AS [Thuế],
                       CAST(COALESCE(i.Discount,0) AS decimal(18,2)) AS [Giảm giá],
                       i.Status AS [Đã trả]
                FROM Invoice i JOIN Account a ON a.AccountName=i.AccountName
                WHERE i.TableID={_tableId} ORDER BY i.CheckoutDate DESC, i.ID DESC
                """;
            using var reader = Database.ExecuteQuery(query);
            var table = new DataTable(); table.Load(reader); dgvJournal.DataSource = table;
            decimal total = 0, tax = 0, discount = 0;
            foreach (DataRow row in table.Rows)
            { total += Convert.ToDecimal(row["Thực thu"]); tax += Convert.ToDecimal(row["Thuế"]); discount += Convert.ToDecimal(row["Giảm giá"]); }
            lblSummary.Text = $"Số hóa đơn: {table.Rows.Count}    Tổng tiền: {total:N0}    Thuế: {tax:N0}    Giảm giá: {discount:N0}";
        }
        catch (Exception ex) { MessageBox.Show($"Không lấy được nhật ký hóa đơn: {ex.Message}"); }
    }
}
