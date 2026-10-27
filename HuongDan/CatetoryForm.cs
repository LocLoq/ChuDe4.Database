using Microsoft.Data.SqlClient;

namespace HuongDan
{
    public partial class CatetoryForm : Form
    {
        public CatetoryForm()
        {
            InitializeComponent();
        }

        private static string SqlText(string value) => value.Replace("'", "''");

        private void CatetoryForm_Load(object sender, EventArgs e) => LoadCategories();
        private void btnLoad_Click(object sender, EventArgs e) => LoadCategories();

        private void LoadCategories()
        {
            try
            {
                using var reader = Database.ExecuteQuery("SELECT ID, Name, Type FROM Category ORDER BY ID");
                lvCategory.Items.Clear();
                while (reader.Read())
                {
                    var item = new ListViewItem(reader["ID"].ToString());
                    item.SubItems.Add(reader["Name"].ToString());
                    item.SubItems.Add(reader["Type"].ToString());
                    lvCategory.Items.Add(item);
                }
                ClearInput();
            }
            catch (Exception ex) { MessageBox.Show($"Không lấy được nhóm món: {ex.Message}"); }
        }

        private bool ReadInput(out string name, out int type)
        {
            name = txtName.Text.Trim();
            type = -1;
            if (name.Length == 0 || !int.TryParse(txtType.Text, out type) || type is not (0 or 1))
            {
                MessageBox.Show("Nhập tên nhóm và loại 1 (món ăn) hoặc 0 (đồ uống).");
                return false;
            }
            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ReadInput(out var name, out var type)) return;
            try
            {
                Database.ExecuteNonQuery($"INSERT INTO Category (Name, Type) VALUES (N'{SqlText(name)}', {type})");
                LoadCategories();
            }
            catch (Exception ex) { MessageBox.Show($"Không thêm được nhóm món: {ex.Message}"); }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtID.Text, out var id) || !ReadInput(out var name, out var type)) return;
            try
            {
                Database.ExecuteNonQuery($"UPDATE Category SET Name=N'{SqlText(name)}', Type={type} WHERE ID={id}");
                LoadCategories();
            }
            catch (Exception ex) { MessageBox.Show($"Không cập nhật được nhóm món: {ex.Message}"); }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtID.Text, out var id)) return;
            if (MessageBox.Show("Xóa nhóm món đã chọn?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            try
            {
                Database.ExecuteNonQuery($"DELETE FROM Category WHERE ID={id}");
                LoadCategories();
            }
            catch (SqlException ex) when (ex.Number == 547) { MessageBox.Show("Nhóm đã có món ăn nên không thể xóa."); }
            catch (Exception ex) { MessageBox.Show($"Không xóa được nhóm món: {ex.Message}"); }
        }

        private void btnSeedDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                Database.ExecuteNonQuery("IF OBJECT_ID(N'dbo.Category', N'U') IS NULL CREATE TABLE Category (ID int IDENTITY(1,1) PRIMARY KEY, Name nvarchar(100) NOT NULL, Type int NOT NULL)");
                var samples = new (string Name, int Type)[]
                {
                    ("Khai vị", 1), ("Hải sản", 1), ("Gà", 1), ("Cơm", 1), ("Thịt", 1),
                    ("Rau", 1), ("Canh", 1), ("Lẩu", 1), ("Bia", 0), ("Nước ngọt", 0),
                    ("Cà phê", 0), ("Trà đá", 0)
                };
                foreach (var sample in samples)
                    Database.ExecuteNonQuery($"IF NOT EXISTS (SELECT 1 FROM Category WHERE Name=N'{SqlText(sample.Name)}' AND Type={sample.Type}) INSERT INTO Category (Name, Type) VALUES (N'{SqlText(sample.Name)}', {sample.Type})");
                LoadCategories();
            }
            catch (Exception ex) { MessageBox.Show($"Không thêm được dữ liệu mẫu: {ex.Message}"); }
        }

        private void lvCategory_Click(object sender, EventArgs e)
        {
            if (lvCategory.SelectedItems.Count == 0) return;
            var item = lvCategory.SelectedItems[0];
            txtID.Text = item.Text;
            txtName.Text = item.SubItems[1].Text;
            txtType.Text = item.SubItems[2].Text;
            btnUpdate.Enabled = btnDelete.Enabled = true;
        }

        private void lvCategory_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            var item = lvCategory.GetItemAt(e.X, e.Y);
            lvCategory.SelectedItems.Clear();
            if (item == null) ClearInput();
            else { item.Selected = true; lvCategory_Click(sender, e); }
        }

        private void ClearInput()
        {
            txtID.Clear(); txtName.Clear(); txtType.Clear();
            btnUpdate.Enabled = btnDelete.Enabled = false;
        }

        private void tsmViewFood_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtID.Text, out var id) || lvCategory.SelectedItems.Count == 0) return;
            using var form = new FoodForm(id, txtName.Text);
            form.ShowDialog(this);
        }

        private void tsmDelete_Click(object sender, EventArgs e) => btnDelete_Click(sender, e);
    }
}
