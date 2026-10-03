using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MyWinApp
{
    public partial class Form1 : Form
    {
        // =========================
        // Controls
        // =========================

        private DataGridView dgvEmployees = null!;

        private TextBox txtSearch = null!;

        private TextBox txtEmployeeID = null!;
        private TextBox txtFirstName = null!;
        private TextBox txtLastName = null!;
        private TextBox txtPosition = null!;
        private TextBox txtDepartment = null!;
        private TextBox txtPhoneNumber = null!;

        private ComboBox cmbStatus = null!;

        private Button btnSearch = null!;
        private Button btnRefresh = null!;
        private Button btnAdd = null!;
        private Button btnUpdate = null!;
        private Button btnDelete = null!;
        private Button btnClear = null!;

        public Form1()
        {
            InitializeComponent();

            SetupForm();
            CreateControls();
            LoadEmployees();
        }

        // =====================================================
        // ตั้งค่าหน้าต่าง
        // =====================================================

        private void SetupForm()
        {
            Text = "Employee Management System";
            StartPosition = FormStartPosition.CenterScreen;

            Width = 1100;
            Height = 720;

            MinimumSize = new Size(1000, 650);

            BackColor = Color.FromArgb(245, 247, 250);

            Font = new Font("Segoe UI", 10);
        }

        // =====================================================
        // สร้าง Controls
        // =====================================================

        private void CreateControls()
        {
            // =========================
            // Header
            // =========================

            Panel header = new Panel();

            header.Left = 0;
            header.Top = 0;
            header.Width = ClientSize.Width;
            header.Height = 75;

            header.BackColor = Color.FromArgb(35, 45, 65);

            header.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            Controls.Add(header);

            Label lblTitle = new Label();

            lblTitle.Text = "EMPLOYEE MANAGEMENT";
            lblTitle.ForeColor = Color.White;

            lblTitle.Font =
                new Font("Segoe UI", 18, FontStyle.Bold);

            lblTitle.Left = 25;
            lblTitle.Top = 15;
            lblTitle.AutoSize = true;

            header.Controls.Add(lblTitle);


            Label lblSubTitle = new Label();

            lblSubTitle.Text = "ระบบจัดการข้อมูลพนักงาน";

            lblSubTitle.ForeColor =
                Color.FromArgb(190, 200, 215);

            lblSubTitle.Font =
                new Font("Segoe UI", 9);

            lblSubTitle.Left = 27;
            lblSubTitle.Top = 47;

            lblSubTitle.AutoSize = true;

            header.Controls.Add(lblSubTitle);


            // =========================
            // Search Area
            // =========================

            GroupBox searchGroup = new GroupBox();

            searchGroup.Text = "ค้นหาพนักงาน";

            searchGroup.Left = 20;
            searchGroup.Top = 90;
            searchGroup.Width = 1040;
            searchGroup.Height = 70;

            searchGroup.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            Controls.Add(searchGroup);


            Label lblSearch = new Label();

            lblSearch.Text = "รหัสพนักงาน / ชื่อ";

            lblSearch.Left = 20;
            lblSearch.Top = 28;

            lblSearch.AutoSize = true;

            searchGroup.Controls.Add(lblSearch);


            txtSearch = new TextBox();

            txtSearch.Left = 150;
            txtSearch.Top = 24;

            txtSearch.Width = 300;

            txtSearch.Font =
                new Font("Segoe UI", 10);

            searchGroup.Controls.Add(txtSearch);


            btnSearch = CreateButton(
                "ค้นหา",
                470,
                22,
                100,
                32,
                Color.FromArgb(52, 152, 219)
            );

            btnSearch.Click += btnSearch_Click;

            searchGroup.Controls.Add(btnSearch);


            btnRefresh = CreateButton(
                "รีเฟรช",
                580,
                22,
                100,
                32,
                Color.FromArgb(108, 117, 125)
            );

            btnRefresh.Click += btnRefresh_Click;

            searchGroup.Controls.Add(btnRefresh);


            // =========================
            // Employee Information
            // =========================

            GroupBox infoGroup = new GroupBox();

            infoGroup.Text = "ข้อมูลพนักงาน";

            infoGroup.Left = 20;
            infoGroup.Top = 175;

            infoGroup.Width = 1040;
            infoGroup.Height = 190;

            infoGroup.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Left |
                AnchorStyles.Right;

            Controls.Add(infoGroup);


            // Row 1

            CreateLabel(
                infoGroup,
                "รหัสพนักงาน",
                20,
                30
            );

            txtEmployeeID = CreateTextBox(
                infoGroup,
                120,
                27,
                180
            );


            CreateLabel(
                infoGroup,
                "ชื่อ",
                340,
                30
            );

            txtFirstName = CreateTextBox(
                infoGroup,
                390,
                27,
                200
            );


            CreateLabel(
                infoGroup,
                "นามสกุล",
                630,
                30
            );

            txtLastName = CreateTextBox(
                infoGroup,
                700,
                27,
                250
            );


            // Row 2

            CreateLabel(
                infoGroup,
                "ตำแหน่ง",
                20,
                75
            );

            txtPosition = CreateTextBox(
                infoGroup,
                120,
                72,
                180
            );


            CreateLabel(
                infoGroup,
                "แผนก",
                340,
                75
            );

            txtDepartment = CreateTextBox(
                infoGroup,
                390,
                72,
                200
            );


            CreateLabel(
                infoGroup,
                "เบอร์โทร",
                630,
                75
            );

            txtPhoneNumber = CreateTextBox(
                infoGroup,
                700,
                72,
                250
            );


            // Row 3

            CreateLabel(
                infoGroup,
                "สถานะ",
                20,
                120
            );

            cmbStatus = new ComboBox();

            cmbStatus.Left = 120;
            cmbStatus.Top = 117;

            cmbStatus.Width = 180;

            cmbStatus.DropDownStyle =
                ComboBoxStyle.DropDownList;

            cmbStatus.Items.Clear();

            cmbStatus.Items.Add("Active");
            cmbStatus.Items.Add("Inactive");

            cmbStatus.SelectedIndex = 0;

            infoGroup.Controls.Add(cmbStatus);


            // =========================
            // Buttons
            // =========================

            btnAdd = CreateButton(
                "เพิ่มข้อมูล",
                340,
                115,
                120,
                35,
                Color.FromArgb(40, 167, 69)
            );

            btnAdd.Click += btnAdd_Click;

            infoGroup.Controls.Add(btnAdd);


            btnUpdate = CreateButton(
                "แก้ไข",
                470,
                115,
                100,
                35,
                Color.FromArgb(255, 193, 7)
            );

            btnUpdate.ForeColor = Color.Black;

            btnUpdate.Click += btnUpdate_Click;

            infoGroup.Controls.Add(btnUpdate);


            btnDelete = CreateButton(
                "ลบ",
                580,
                115,
                100,
                35,
                Color.FromArgb(220, 53, 69)
            );

            btnDelete.Click += btnDelete_Click;

            infoGroup.Controls.Add(btnDelete);


            btnClear = CreateButton(
                "ล้างข้อมูล",
                690,
                115,
                120,
                35,
                Color.FromArgb(108, 117, 125)
            );

            btnClear.Click += btnClear_Click;

            infoGroup.Controls.Add(btnClear);


            // =========================
            // DataGridView
            // =========================

            Label lblTable = new Label();

            lblTable.Text = "รายชื่อพนักงาน";

            lblTable.Left = 20;
            lblTable.Top = 380;

            lblTable.Font =
                new Font(
                    "Segoe UI",
                    11,
                    FontStyle.Bold
                );

            lblTable.AutoSize = true;

            Controls.Add(lblTable);


            dgvEmployees = new DataGridView();

            dgvEmployees.Left = 20;
            dgvEmployees.Top = 410;

            dgvEmployees.Width = 1040;
            dgvEmployees.Height = 250;

            dgvEmployees.Anchor =
                AnchorStyles.Top |
                AnchorStyles.Bottom |
                AnchorStyles.Left |
                AnchorStyles.Right;

            dgvEmployees.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            dgvEmployees.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            dgvEmployees.MultiSelect = false;

            dgvEmployees.ReadOnly = true;

            dgvEmployees.AllowUserToAddRows = false;

            dgvEmployees.AllowUserToDeleteRows = false;

            dgvEmployees.RowHeadersVisible = false;

            dgvEmployees.BackgroundColor = Color.White;

            dgvEmployees.BorderStyle =
                BorderStyle.FixedSingle;

            dgvEmployees.CellClick +=
                dgvEmployees_CellClick;

            Controls.Add(dgvEmployees);
        }

        // =====================================================
        // สร้าง Label
        // =====================================================

        private void CreateLabel(
            Control parent,
            string text,
            int left,
            int top)
        {
            Label label = new Label();

            label.Text = text;

            label.Left = left;
            label.Top = top;

            label.AutoSize = true;

            parent.Controls.Add(label);
        }

        // =====================================================
        // สร้าง TextBox
        // =====================================================

        private TextBox CreateTextBox(
            Control parent,
            int left,
            int top,
            int width)
        {
            TextBox textBox = new TextBox();

            textBox.Left = left;
            textBox.Top = top;

            textBox.Width = width;

            textBox.Height = 30;

            parent.Controls.Add(textBox);

            return textBox;
        }

        // =====================================================
        // สร้าง Button
        // =====================================================

        private Button CreateButton(
            string text,
            int left,
            int top,
            int width,
            int height,
            Color color)
        {
            Button button = new Button();

            button.Text = text;

            button.Left = left;
            button.Top = top;

            button.Width = width;
            button.Height = height;

            button.BackColor = color;

            button.ForeColor = Color.White;

            button.FlatStyle =
                FlatStyle.Flat;

            button.FlatAppearance.BorderSize = 0;

            button.Cursor =
                Cursors.Hand;

            return button;
        }

        // =====================================================
        // โหลดข้อมูลทั้งหมด
        // =====================================================

        private void LoadEmployees()
        {
            try
            {
                Database db = new Database();

                using (MySqlConnection conn =
                       db.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                        SELECT
                            EmployeeID,
                            FirstName,
                            LastName,
                            Position,
                            Department,
                            PhoneNumber,
                            CASE
                                WHEN Status = 1 THEN 'Active'
                                ELSE 'Inactive'
                            END AS Status
                        FROM employees
                        ORDER BY EmployeeID
                    ";

                    using (MySqlDataAdapter adapter =
                           new MySqlDataAdapter(sql, conn))
                    {
                        DataTable table =
                            new DataTable();

                        adapter.Fill(table);

                        dgvEmployees.DataSource = table;
                    }
                }

                SetColumnHeaders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "ไม่สามารถโหลดข้อมูลได้\n\n" +
                    ex.Message,
                    "เกิดข้อผิดพลาด",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // เปลี่ยนชื่อหัวตาราง
        // =====================================================

        private void SetColumnHeaders()
        {
            if (dgvEmployees.Columns.Count == 0)
                return;

            dgvEmployees.Columns["EmployeeID"]
                .HeaderText = "รหัสพนักงาน";

            dgvEmployees.Columns["FirstName"]
                .HeaderText = "ชื่อ";

            dgvEmployees.Columns["LastName"]
                .HeaderText = "นามสกุล";

            dgvEmployees.Columns["Position"]
                .HeaderText = "ตำแหน่ง";

            dgvEmployees.Columns["Department"]
                .HeaderText = "แผนก";

            dgvEmployees.Columns["PhoneNumber"]
                .HeaderText = "เบอร์โทรศัพท์";

            dgvEmployees.Columns["Status"]
                .HeaderText = "สถานะ";
        }

        // =====================================================
        // ค้นหา
        // =====================================================

        private void btnSearch_Click(
            object? sender,
            EventArgs e)
        {
            try
            {
                string keyword =
                    txtSearch.Text.Trim();

                Database db = new Database();

                using (MySqlConnection conn =
                       db.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                        SELECT
                            EmployeeID,
                            FirstName,
                            LastName,
                            Position,
                            Department,
                            PhoneNumber,
                            CASE
                                WHEN Status = 1 THEN 'Active'
                                ELSE 'Inactive'
                            END AS Status
                        FROM employees
                        WHERE EmployeeID LIKE @keyword
                        OR FirstName LIKE @keyword
                        OR LastName LIKE @keyword
                        ORDER BY EmployeeID
                    ";

                    using (MySqlCommand cmd =
                           new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@keyword",
                            "%" + keyword + "%"
                        );

                        using (MySqlDataAdapter adapter =
                               new MySqlDataAdapter(cmd))
                        {
                            DataTable table =
                                new DataTable();

                            adapter.Fill(table);

                            dgvEmployees.DataSource =
                                table;
                        }
                    }
                }

                SetColumnHeaders();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "ค้นหาข้อมูลไม่สำเร็จ\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // รีเฟรช
        // =====================================================

        private void btnRefresh_Click(
            object? sender,
            EventArgs e)
        {
            txtSearch.Clear();

            LoadEmployees();

            ClearForm();
        }

        // =====================================================
        // เพิ่มพนักงาน
        // =====================================================

        private void btnAdd_Click(
            object? sender,
            EventArgs e)
        {
            if (!ValidateForm())
                return;

            try
            {
                Database db = new Database();

                using (MySqlConnection conn =
                       db.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                        INSERT INTO employees
                        (
                            EmployeeID,
                            FirstName,
                            LastName,
                            Position,
                            Department,
                            PhoneNumber,
                            Status
                        )
                        VALUES
                        (
                            @EmployeeID,
                            @FirstName,
                            @LastName,
                            @Position,
                            @Department,
                            @PhoneNumber,
                            @Status
                        )
                    ";

                    using (MySqlCommand cmd =
                           new MySqlCommand(sql, conn))
                    {
                        AddParameters(cmd);

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "เพิ่มข้อมูลพนักงานเรียบร้อยแล้ว",
                    "สำเร็จ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LoadEmployees();

                ClearForm();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(
                    "ไม่สามารถเพิ่มข้อมูลได้\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // แก้ไข
        // =====================================================

        private void btnUpdate_Click(
            object? sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtEmployeeID.Text))
            {
                MessageBox.Show(
                    "กรุณาเลือกพนักงานที่ต้องการแก้ไข",
                    "แจ้งเตือน",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (!ValidateForm())
                return;

            try
            {
                Database db = new Database();

                using (MySqlConnection conn =
                       db.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                        UPDATE employees
                        SET
                            FirstName = @FirstName,
                            LastName = @LastName,
                            Position = @Position,
                            Department = @Department,
                            PhoneNumber = @PhoneNumber,
                            Status = @Status
                        WHERE EmployeeID = @EmployeeID
                    ";

                    using (MySqlCommand cmd =
                           new MySqlCommand(sql, conn))
                    {
                        AddParameters(cmd);

                        int result =
                            cmd.ExecuteNonQuery();

                        if (result == 0)
                        {
                            MessageBox.Show(
                                "ไม่พบข้อมูลพนักงาน",
                                "แจ้งเตือน",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );

                            return;
                        }
                    }
                }

                MessageBox.Show(
                    "แก้ไขข้อมูลเรียบร้อยแล้ว",
                    "สำเร็จ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LoadEmployees();

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "แก้ไขข้อมูลไม่สำเร็จ\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // ลบ
        // =====================================================

        private void btnDelete_Click(
            object? sender,
            EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(
                txtEmployeeID.Text))
            {
                MessageBox.Show(
                    "กรุณาเลือกพนักงานที่ต้องการลบ",
                    "แจ้งเตือน",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            DialogResult result =
                MessageBox.Show(
                    "คุณต้องการลบข้อมูลพนักงาน\n" +
                    "รหัส " +
                    txtEmployeeID.Text +
                    " ใช่หรือไม่?",
                    "ยืนยันการลบ",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

            if (result != DialogResult.Yes)
                return;

            try
            {
                Database db = new Database();

                using (MySqlConnection conn =
                       db.GetConnection())
                {
                    conn.Open();

                    string sql = @"
                        DELETE FROM employees
                        WHERE EmployeeID = @EmployeeID
                    ";

                    using (MySqlCommand cmd =
                           new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue(
                            "@EmployeeID",
                            txtEmployeeID.Text.Trim()
                        );

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show(
                    "ลบข้อมูลเรียบร้อยแล้ว",
                    "สำเร็จ",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                LoadEmployees();

                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "ลบข้อมูลไม่สำเร็จ\n\n" +
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // คลิก DataGridView
        // =====================================================

        private void dgvEmployees_CellClick(
            object? sender,
            DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            DataGridViewRow row =
                dgvEmployees.Rows[e.RowIndex];

            txtEmployeeID.Text =
                row.Cells["EmployeeID"]
                .Value?.ToString() ?? "";

            txtFirstName.Text =
                row.Cells["FirstName"]
                .Value?.ToString() ?? "";

            txtLastName.Text =
                row.Cells["LastName"]
                .Value?.ToString() ?? "";

            txtPosition.Text =
                row.Cells["Position"]
                .Value?.ToString() ?? "";

            txtDepartment.Text =
                row.Cells["Department"]
                .Value?.ToString() ?? "";

            txtPhoneNumber.Text =
                row.Cells["PhoneNumber"]
                .Value?.ToString() ?? "";

            string status =
                row.Cells["Status"]
                .Value?.ToString()?.Trim() ?? "";

            if (status == "1")
            {
                cmbStatus.SelectedItem = "Active";
            }
            else if (status == "0")
            {
                cmbStatus.SelectedItem = "Inactive";
            }
            else
            {
                cmbStatus.SelectedIndex = -1;
            }
        }

        // =====================================================
        // เพิ่ม Parameters
        // =====================================================

        private void AddParameters(MySqlCommand cmd)
        {
            cmd.Parameters.AddWithValue(
                "@EmployeeID",
                txtEmployeeID.Text.Trim()
            );

            cmd.Parameters.AddWithValue(
                "@FirstName",
                txtFirstName.Text.Trim()
            );

            cmd.Parameters.AddWithValue(
                "@LastName",
                txtLastName.Text.Trim()
            );

            cmd.Parameters.AddWithValue(
                "@Position",
                txtPosition.Text.Trim()
            );

            cmd.Parameters.AddWithValue(
                "@Department",
                txtDepartment.Text.Trim()
            );

            cmd.Parameters.AddWithValue(
                "@PhoneNumber",
                txtPhoneNumber.Text.Trim()
            );

            // Active = 1
            // Inactive = 0
            int status = cmbStatus.SelectedItem?.ToString() == "Active"
                ? 1
                : 0;

            cmd.Parameters.AddWithValue(
                "@Status",
                status
            );
        }

        // =====================================================
        // ตรวจสอบข้อมูล
        // =====================================================

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(
                txtEmployeeID.Text))
            {
                MessageBox.Show(
                    "กรุณากรอกรหัสพนักงาน",
                    "แจ้งเตือน",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtEmployeeID.Focus();

                return false;
            }

            if (string.IsNullOrWhiteSpace(
                txtFirstName.Text))
            {
                MessageBox.Show(
                    "กรุณากรอกชื่อ",
                    "แจ้งเตือน",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtFirstName.Focus();

                return false;
            }

            if (string.IsNullOrWhiteSpace(
                txtLastName.Text))
            {
                MessageBox.Show(
                    "กรุณากรอกนามสกุล",
                    "แจ้งเตือน",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                txtLastName.Focus();

                return false;
            }

            return true;
        }

        // =====================================================
        // ล้างข้อมูล
        // =====================================================

        private void btnClear_Click(
            object? sender,
            EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            txtEmployeeID.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtPosition.Clear();
            txtDepartment.Clear();
            txtPhoneNumber.Clear();

            cmbStatus.SelectedIndex = 0;

            dgvEmployees.ClearSelection();

            txtEmployeeID.Focus();
        }
    }
}