namespace CodeGeneration.UI
{
    partial class MainFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Object_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.主表名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.主表列名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.外键表名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.外键列名 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.字段名称 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.字段Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.表Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.字段类型ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.是否主键 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.是否可为空 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.长度 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.是否是标识位 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 279);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据表";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView2);
            this.groupBox2.Location = new System.Drawing.Point(336, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(866, 279);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据字段";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView3);
            this.groupBox3.Location = new System.Drawing.Point(12, 297);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(462, 190);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "关联字段";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.Object_id});
            this.dataGridView1.Location = new System.Drawing.Point(10, 20);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(244, 253);
            this.dataGridView1.TabIndex = 4;
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // name
            // 
            this.name.DataPropertyName = "Name";
            this.name.HeaderText = "表名";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            // 
            // Object_id
            // 
            this.Object_id.DataPropertyName = "Object_id";
            this.Object_id.HeaderText = "Id";
            this.Object_id.Name = "Object_id";
            this.Object_id.ReadOnly = true;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToOrderColumns = true;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.字段名称,
            this.字段Id,
            this.表Id,
            this.字段类型ID,
            this.是否主键,
            this.是否可为空,
            this.长度,
            this.是否是标识位});
            this.dataGridView2.Location = new System.Drawing.Point(6, 20);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowTemplate.Height = 23;
            this.dataGridView2.Size = new System.Drawing.Size(844, 253);
            this.dataGridView2.TabIndex = 0;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AllowUserToOrderColumns = true;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.主表名,
            this.主表列名,
            this.外键表名,
            this.外键列名});
            this.dataGridView3.Location = new System.Drawing.Point(6, 20);
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowTemplate.Height = 23;
            this.dataGridView3.Size = new System.Drawing.Size(444, 164);
            this.dataGridView3.TabIndex = 4;
            // 
            // 主表名
            // 
            this.主表名.DataPropertyName = "MainName";
            this.主表名.HeaderText = "主表列名";
            this.主表名.Name = "主表名";
            this.主表名.ReadOnly = true;
            // 
            // 主表列名
            // 
            this.主表列名.DataPropertyName = "MainColumns";
            this.主表列名.HeaderText = "主表列名";
            this.主表列名.Name = "主表列名";
            this.主表列名.ReadOnly = true;
            // 
            // 外键表名
            // 
            this.外键表名.DataPropertyName = "RefName";
            this.外键表名.HeaderText = "外键表名";
            this.外键表名.Name = "外键表名";
            this.外键表名.ReadOnly = true;
            // 
            // 外键列名
            // 
            this.外键列名.DataPropertyName = "RefColumns";
            this.外键列名.HeaderText = "外键列名";
            this.外键列名.Name = "外键列名";
            this.外键列名.ReadOnly = true;
            // 
            // 字段名称
            // 
            this.字段名称.DataPropertyName = "Name";
            this.字段名称.HeaderText = "字段名称";
            this.字段名称.Name = "字段名称";
            this.字段名称.ReadOnly = true;
            // 
            // 字段Id
            // 
            this.字段Id.DataPropertyName = "Columns_id";
            this.字段Id.HeaderText = "字段Id";
            this.字段Id.Name = "字段Id";
            this.字段Id.ReadOnly = true;
            // 
            // 表Id
            // 
            this.表Id.DataPropertyName = "Object_id";
            this.表Id.HeaderText = "表Id";
            this.表Id.Name = "表Id";
            this.表Id.ReadOnly = true;
            // 
            // 字段类型ID
            // 
            this.字段类型ID.DataPropertyName = "User_type_id";
            this.字段类型ID.HeaderText = "字段类型ID";
            this.字段类型ID.Name = "字段类型ID";
            this.字段类型ID.ReadOnly = true;
            // 
            // 是否主键
            // 
            this.是否主键.DataPropertyName = "Isprimarykey";
            this.是否主键.HeaderText = "是否主键";
            this.是否主键.Name = "是否主键";
            this.是否主键.ReadOnly = true;
            // 
            // 是否可为空
            // 
            this.是否可为空.DataPropertyName = "Is_nullable";
            this.是否可为空.HeaderText = "是否可为空";
            this.是否可为空.Name = "是否可为空";
            this.是否可为空.ReadOnly = true;
            // 
            // 长度
            // 
            this.长度.DataPropertyName = "Max_length";
            this.长度.HeaderText = "长度";
            this.长度.Name = "长度";
            this.长度.ReadOnly = true;
            // 
            // 是否是标识位
            // 
            this.是否是标识位.DataPropertyName = "Is_identity";
            this.是否是标识位.HeaderText = "是否是标识位";
            this.是否是标识位.Name = "是否是标识位";
            this.是否是标识位.ReadOnly = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 20);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "生成实体类";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Location = new System.Drawing.Point(480, 297);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(722, 190);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "代码生成项";
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1214, 499);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "MainFrm";
            this.Text = "MainFrm";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn Object_id;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.DataGridViewTextBoxColumn 主表名;
        private System.Windows.Forms.DataGridViewTextBoxColumn 主表列名;
        private System.Windows.Forms.DataGridViewTextBoxColumn 外键表名;
        private System.Windows.Forms.DataGridViewTextBoxColumn 外键列名;
        private System.Windows.Forms.DataGridViewTextBoxColumn 字段名称;
        private System.Windows.Forms.DataGridViewTextBoxColumn 字段Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn 表Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn 字段类型ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn 是否主键;
        private System.Windows.Forms.DataGridViewTextBoxColumn 是否可为空;
        private System.Windows.Forms.DataGridViewTextBoxColumn 长度;
        private System.Windows.Forms.DataGridViewTextBoxColumn 是否是标识位;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox4;

    }
}