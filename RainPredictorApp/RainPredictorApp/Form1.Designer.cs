namespace RainPredictorApp
{
    partial class Form1
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
            label1 = new Label();
            label2 = new Label();
            button1 = new Button();
            toolTip1 = new ToolTip(components);
            label3 = new Label();
            button2 = new Button();
            textBox1 = new TextBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Yu Mincho", 11.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            label1.Location = new Point(266, 61);
            label1.Name = "label1";
            label1.Size = new Size(265, 19);
            label1.TabIndex = 0;
            label1.Text = "Predict rainfall in Australia today!\r\n";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Yu Mincho Demibold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(266, 18);
            label2.Name = "label2";
            label2.Size = new Size(273, 27);
            label2.TabIndex = 1;
            label2.Text = "Rain Predictor for Australia";
            // 
            // button1
            // 
            button1.Location = new Point(45, 115);
            button1.Name = "button1";
            button1.Size = new Size(87, 53);
            button1.TabIndex = 2;
            button1.Text = "Get current weather data";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(138, 132);
            label3.Name = "label3";
            label3.Size = new Size(13, 19);
            label3.TabIndex = 3;
            label3.Text = "i";
            // 
            // button2
            // 
            button2.Location = new Point(45, 284);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 4;
            button2.Text = "Predict rain";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_ClickAsync;
            // 
            // textBox1
            // 
            textBox1.AcceptsReturn = true;
            textBox1.Location = new Point(45, 245);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(345, 33);
            textBox1.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox1);
            Controls.Add(button2);
            Controls.Add(label3);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Text = "RainPredictorAUS";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Button button1;
        private ToolTip toolTip1;
        private Label label3;
        private Button button2;
        private TextBox textBox1;
    }
}
