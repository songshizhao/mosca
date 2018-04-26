namespace MoscaPC
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.InputOpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.OutputFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.DebugTextBox = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SelectInput = new System.Windows.Forms.Button();
            this.lb1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_SelectOutput = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // InputOpenFileDialog
            // 
            this.InputOpenFileDialog.FileName = "InputFileDialog";
            this.InputOpenFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.InputOpenFileDialog_FileOk);
            // 
            // OutputFileDialog
            // 
            this.OutputFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.OutputFileDialog_FileOk);
            // 
            // DebugTextBox
            // 
            this.DebugTextBox.Location = new System.Drawing.Point(0, 184);
            this.DebugTextBox.Margin = new System.Windows.Forms.Padding(2);
            this.DebugTextBox.Multiline = true;
            this.DebugTextBox.Name = "DebugTextBox";
            this.DebugTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.DebugTextBox.Size = new System.Drawing.Size(1078, 414);
            this.DebugTextBox.TabIndex = 9;
            this.DebugTextBox.Text = "计算信息：\r\n";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.SelectInput);
            this.panel1.Controls.Add(this.lb1);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.btn_SelectOutput);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.textBox2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Location = new System.Drawing.Point(0, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1078, 180);
            this.panel1.TabIndex = 10;
            // 
            // SelectInput
            // 
            this.SelectInput.Location = new System.Drawing.Point(299, 25);
            this.SelectInput.Margin = new System.Windows.Forms.Padding(2);
            this.SelectInput.Name = "SelectInput";
            this.SelectInput.Size = new System.Drawing.Size(83, 21);
            this.SelectInput.TabIndex = 9;
            this.SelectInput.Text = "输入";
            this.SelectInput.UseVisualStyleBackColor = true;
            this.SelectInput.Click += new System.EventHandler(this.SelectInput_Click);
            // 
            // lb1
            // 
            this.lb1.AutoSize = true;
            this.lb1.Location = new System.Drawing.Point(12, 11);
            this.lb1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lb1.Name = "lb1";
            this.lb1.Size = new System.Drawing.Size(53, 12);
            this.lb1.TabIndex = 11;
            this.lb1.Text = "输入文件";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(300, 145);
            this.button3.Margin = new System.Windows.Forms.Padding(2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(82, 20);
            this.button3.TabIndex = 17;
            this.button3.Text = "计算";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.Run_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(13, 25);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(270, 21);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "C:\\Windows\\appcompat";
            // 
            // btn_SelectOutput
            // 
            this.btn_SelectOutput.Location = new System.Drawing.Point(300, 80);
            this.btn_SelectOutput.Margin = new System.Windows.Forms.Padding(2);
            this.btn_SelectOutput.Name = "btn_SelectOutput";
            this.btn_SelectOutput.Size = new System.Drawing.Size(82, 20);
            this.btn_SelectOutput.TabIndex = 14;
            this.btn_SelectOutput.Text = "浏览";
            this.btn_SelectOutput.UseVisualStyleBackColor = true;
            this.btn_SelectOutput.Click += new System.EventHandler(this.SelectOutput);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 121);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 16;
            this.label2.Text = "进度";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(14, 81);
            this.textBox2.Margin = new System.Windows.Forms.Padding(2);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(270, 21);
            this.textBox2.TabIndex = 13;
            this.textBox2.Text = "C:\\Windows\\appcompat";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 58);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "输出文件";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(13, 145);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(269, 18);
            this.progressBar1.TabIndex = 15;
            this.progressBar1.Value = 1;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "MOSCA-PC";
            this.notifyIcon1.Visible = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(963, 593);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.DebugTextBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Mosca子通道分析程序示例";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog InputOpenFileDialog;
        private System.Windows.Forms.SaveFileDialog OutputFileDialog;
        private System.Windows.Forms.TextBox DebugTextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button SelectInput;
        private System.Windows.Forms.Label lb1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_SelectOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

