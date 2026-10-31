namespace HuongDan
{
    partial class CatetoryForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            lvCategory = new ListView();
            chID = new ColumnHeader();
            chName = new ColumnHeader();
            chType = new ColumnHeader();
            btnLoad = new Button();
            btnSeedDatabase = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            txtID = new TextBox();
            txtType = new TextBox();
            txtName = new TextBox();
            btnDelete = new Button();
            btnUpdate = new Button();
            btnAdd = new Button();
            contextMenuStrip1 = new ContextMenuStrip(components);
            tsmViewFood = new ToolStripMenuItem();
            tsmDelete = new ToolStripMenuItem();
            contextMenuStrip1.Items.AddRange(new ToolStripItem[] { tsmViewFood, tsmDelete });
            tsmViewFood.Text = "Xem danh sách món ăn";
            tsmViewFood.Click += tsmViewFood_Click;
            tsmDelete.Text = "Xóa nhóm món";
            tsmDelete.Click += tsmDelete_Click;
            SuspendLayout();
            // 
            // lvCategory
            // 
            lvCategory.Columns.AddRange(new ColumnHeader[] { chID, chName, chType });
            lvCategory.FullRowSelect = true;
            lvCategory.Location = new Point(12, 159);
            lvCategory.MultiSelect = false;
            lvCategory.Name = "lvCategory";
            lvCategory.Size = new Size(776, 310);
            lvCategory.TabIndex = 0;
            lvCategory.UseCompatibleStateImageBehavior = false;
            lvCategory.View = View.Details;
            lvCategory.ContextMenuStrip = contextMenuStrip1;
            lvCategory.Click += lvCategory_Click;
            lvCategory.MouseDown += lvCategory_MouseDown;
            // 
            // chID
            // 
            chID.Text = "Mã Loại";
            chID.Width = 120;
            // 
            // chName
            // 
            chName.Text = "Tên loại món ăn";
            chName.Width = 500;
            // 
            // chType
            // 
            chType.Text = "Loại";
            chType.Width = 300;
            // 
            // btnLoad
            // 
            btnLoad.Location = new Point(685, 118);
            btnLoad.Name = "btnLoad";
            btnLoad.RightToLeft = RightToLeft.No;
            btnLoad.Size = new Size(104, 28);
            btnLoad.TabIndex = 1;
            btnLoad.Text = "Lấy danh sách";
            btnLoad.UseVisualStyleBackColor = true;
            btnLoad.Click += btnLoad_Click;
            // 
            // btnSeedDatabase
            // 
            btnSeedDatabase.Location = new Point(590, 118);
            btnSeedDatabase.Name = "btnSeedDatabase";
            btnSeedDatabase.Size = new Size(89, 28);
            btnSeedDatabase.TabIndex = 2;
            btnSeedDatabase.Text = "Tạo dữ liệu mẫu";
            btnSeedDatabase.UseVisualStyleBackColor = true;
            btnSeedDatabase.Click += btnSeedDatabase_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(59, 29);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 3;
            label1.Text = "Mã loại: ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(59, 61);
            label2.Name = "label2";
            label2.Size = new Size(95, 15);
            label2.TabIndex = 4;
            label2.Text = "Tên loại món ăn:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(59, 92);
            label3.Name = "label3";
            label3.Size = new Size(32, 15);
            label3.TabIndex = 5;
            label3.Text = "Loại:";
            // 
            // txtID
            // 
            txtID.Location = new Point(166, 26);
            txtID.Name = "txtID";
            txtID.ReadOnly = true;
            txtID.Size = new Size(224, 23);
            txtID.TabIndex = 6;
            // 
            // txtType
            // 
            txtType.Location = new Point(166, 89);
            txtType.Name = "txtType";
            txtType.Size = new Size(224, 23);
            txtType.TabIndex = 7;
            // 
            // txtName
            // 
            txtName.Location = new Point(166, 58);
            txtName.Name = "txtName";
            txtName.Size = new Size(224, 23);
            txtName.TabIndex = 8;
            // 
            // btnDelete
            // 
            btnDelete.Enabled = false;
            btnDelete.Location = new Point(506, 118);
            btnDelete.Name = "btnDelete";
            btnDelete.RightToLeft = RightToLeft.No;
            btnDelete.Size = new Size(78, 28);
            btnDelete.TabIndex = 9;
            btnDelete.Text = "Xóa";
            btnDelete.UseVisualStyleBackColor = true;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Enabled = false;
            btnUpdate.Location = new Point(396, 118);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.RightToLeft = RightToLeft.No;
            btnUpdate.Size = new Size(104, 28);
            btnUpdate.TabIndex = 10;
            btnUpdate.Text = "Cập Nhật";
            btnUpdate.UseVisualStyleBackColor = true;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnAdd
            // 
            btnAdd.Location = new Point(301, 118);
            btnAdd.Name = "btnAdd";
            btnAdd.RightToLeft = RightToLeft.No;
            btnAdd.Size = new Size(89, 28);
            btnAdd.TabIndex = 11;
            btnAdd.Text = "Thêm";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // CatetoryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 481);
            Controls.Add(btnAdd);
            Controls.Add(btnUpdate);
            Controls.Add(btnDelete);
            Controls.Add(txtName);
            Controls.Add(txtType);
            Controls.Add(txtID);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnSeedDatabase);
            Controls.Add(btnLoad);
            Controls.Add(lvCategory);
            Name = "CatetoryForm";
            Text = "Quản lý nhóm món ăn";
            Load += CatetoryForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListView lvCategory;
        private ColumnHeader chID;
        private ColumnHeader chName;
        private ColumnHeader chType;
        private Button btnLoad;
        private Button btnSeedDatabase;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox txtID;
        private TextBox txtType;
        private TextBox txtName;
        private Button btnDelete;
        private Button btnUpdate;
        private Button btnAdd;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem tsmViewFood;
        private ToolStripMenuItem tsmDelete;
    }
}
