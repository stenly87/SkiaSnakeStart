namespace WinFormsApp2;

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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        button1 = new System.Windows.Forms.Button();
        button2 = new System.Windows.Forms.Button();
        SuspendLayout();
        // 
        // button1
        // 
        button1.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        button1.Location = new System.Drawing.Point(684, 12);
        button1.Name = "button1";
        button1.Size = new System.Drawing.Size(104, 42);
        button1.TabIndex = 0;
        button1.Text = "Старт";
        button1.UseVisualStyleBackColor = true;
        button1.Click += button1_Click;
        // 
        // button2
        // 
        button2.Anchor = ((System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right));
        button2.Location = new System.Drawing.Point(684, 69);
        button2.Name = "button2";
        button2.Size = new System.Drawing.Size(104, 42);
        button2.TabIndex = 1;
        button2.Text = "Стоп";
        button2.UseVisualStyleBackColor = true;
        button2.Click += button2_Click;
        // 
        // Form1
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(800, 631);
        Controls.Add(button2);
        Controls.Add(button1);
        KeyPreview = true;
        Text = "Form1";
        KeyDown += Form1_KeyDown;
        ResumeLayout(false);
    }

    private System.Windows.Forms.Button button2;

    private System.Windows.Forms.Button button1;

    #endregion
}