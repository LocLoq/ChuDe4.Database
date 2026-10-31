using System.Data;

namespace Bai2;

public partial class BillDetailsForm : Form
{
    private readonly int _invoiceId;
    public BillDetailsForm() : this(0) { }
    public BillDetailsForm(int invoiceId)
    { _invoiceId = invoiceId; InitializeComponent(); }

    private void BillDetailsForm_Load(object sender, EventArgs e)
    {
        try
        {
            using (var reader = Database.ExecuteQuery($"SELECT i.Name, t.Name, i.CheckoutDate, i.Amount * (1 - COALESCE(i.Discount,0) + COALESCE(i.Tax,0)), i.Amount * COALESCE(i.Discount,0), i.Amount * COALESCE(i.Tax,0) FROM Bills i JOIN dbo.[Table] t ON t.ID=i.TableID WHERE i.ID={_invoiceId}"))
            {
                if (!reader.Read()) { MessageBox.Show("Không tìm thấy hóa đơn."); Close(); return; }
                var date = reader.IsDBNull(2) ? "Chưa có ngày thanh toán" : reader.GetDateTime(2).ToString("dd/MM/yyyy HH:mm");
                lblHeader.Text = $"#{_invoiceId} - {reader.GetString(0)} | Bàn {reader.GetString(1)} | {date} | " +
                    $"Thực thu: {Convert.ToDecimal(reader.GetValue(3)):N0} | Giảm: {Convert.ToDecimal(reader.GetValue(4)):N0} | Thuế: {Convert.ToDecimal(reader.GetValue(5)):N0}";
            }
            using var details = Database.ExecuteQuery($"SELECT f.Name AS [Món ăn], d.Quantity AS [Số lượng], f.Price AS [Giá hiện tại] FROM BillDetails d JOIN Food f ON f.ID=d.FoodID WHERE d.InvoiceID={_invoiceId} ORDER BY d.ID");
            var table = new DataTable(); table.Load(details); dgvDetails.DataSource = table;
        }
        catch (Exception ex) { MessageBox.Show($"Không lấy được chi tiết hóa đơn: {ex.Message}"); }
    }
}
