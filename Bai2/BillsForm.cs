using System.Data;

namespace Bai2;

public partial class BillsForm : Form
{
    public BillsForm()
    {
        InitializeComponent();
        dtFrom.Value = DateTime.Today.AddDays(-30);
    }
    private void BillsForm_Load(object sender, EventArgs e) => LoadBills();
    private void btnLoad_Click(object sender, EventArgs e) => LoadBills();

    private void LoadBills()
    {
        if (dtFrom.Value.Date > dtTo.Value.Date)
        { MessageBox.Show("Ngày bắt đầu phải trước hoặc bằng ngày kết thúc."); return; }
        try
        {
            var query = $"""
                SELECT i.ID AS [Mã hóa đơn], i.Name AS [Tên hóa đơn], t.Name AS [Bàn],
                       i.CheckoutDate AS [Ngày],
                       CAST(i.Amount AS decimal(18,2)) AS [Trước giảm giá],
                       CAST(i.Amount * COALESCE(i.Discount,0) AS decimal(18,2)) AS [Giảm giá],
                       CAST(i.Amount * (1 - COALESCE(i.Discount,0) + COALESCE(i.Tax,0)) AS decimal(18,2)) AS [Thực thu]
                FROM Bills i JOIN dbo.[Table] t ON t.ID=i.TableID
                WHERE i.Status=1 AND (i.CheckoutDate IS NULL OR
                  (i.CheckoutDate >= '{dtFrom.Value:yyyyMMdd}'
                  AND i.CheckoutDate < DATEADD(day,1,'{dtTo.Value:yyyyMMdd}')))
                ORDER BY i.CheckoutDate DESC, i.ID DESC
                """;
            using var reader = Database.ExecuteQuery(query);
            var table = new DataTable(); table.Load(reader); dgvBills.DataSource = table;
            decimal gross = 0, discount = 0, net = 0;
            foreach (DataRow row in table.Rows)
            {
                gross += Convert.ToDecimal(row["Trước giảm giá"]);
                discount += Convert.ToDecimal(row["Giảm giá"]);
                net += Convert.ToDecimal(row["Thực thu"]);
            }
            lblTotals.Text = $"Tổng trước giảm giá: {gross:N0}    Giảm giá: {discount:N0}    Thực thu: {net:N0}";
        }
        catch (Exception ex) { MessageBox.Show($"Không lấy được hóa đơn: {ex.Message}"); }
    }

    private void dgvBills_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0 || dgvBills.Rows[e.RowIndex].Cells[0].Value is null) return;
        using var form = new BillDetailsForm(Convert.ToInt32(dgvBills.Rows[e.RowIndex].Cells[0].Value));
        form.ShowDialog(this);
    }
}
