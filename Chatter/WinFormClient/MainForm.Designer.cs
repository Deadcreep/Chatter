namespace WinFormClient
{
    partial class MainForm
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
            this.Line = new System.Windows.Forms.Label();
            this.tryLoginButton = new System.Windows.Forms.Button();
            this.passwordTextBox = new System.Windows.Forms.TextBox();
            this.passwordLabel = new System.Windows.Forms.Label();
            this.loginTextBox = new System.Windows.Forms.TextBox();
            this.loginLabel = new System.Windows.Forms.Label();
            this.contactsListBox = new System.Windows.Forms.ListBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.newMessageTextBox = new System.Windows.Forms.TextBox();
            this.MessagesTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Line
            // 
            this.Line.BackColor = System.Drawing.SystemColors.Control;
            this.Line.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Line.Location = new System.Drawing.Point(174, 6);
            this.Line.Name = "Line";
            this.Line.Size = new System.Drawing.Size(10, 625);
            this.Line.TabIndex = 11;
            // 
            // tryLoginButton
            // 
            this.tryLoginButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.tryLoginButton.Location = new System.Drawing.Point(85, 58);
            this.tryLoginButton.Name = "tryLoginButton";
            this.tryLoginButton.Size = new System.Drawing.Size(71, 23);
            this.tryLoginButton.TabIndex = 10;
            this.tryLoginButton.Text = "Sign in";
            this.tryLoginButton.UseVisualStyleBackColor = true;
            this.tryLoginButton.Click += new System.EventHandler(this.tryLoginButton_Click);
            // 
            // passwordTextBox
            // 
            this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.passwordTextBox.Location = new System.Drawing.Point(65, 32);
            this.passwordTextBox.Name = "passwordTextBox";
            this.passwordTextBox.Size = new System.Drawing.Size(92, 20);
            this.passwordTextBox.TabIndex = 9;
            // 
            // passwordLabel
            // 
            this.passwordLabel.AutoSize = true;
            this.passwordLabel.Location = new System.Drawing.Point(6, 35);
            this.passwordLabel.Name = "passwordLabel";
            this.passwordLabel.Size = new System.Drawing.Size(53, 13);
            this.passwordLabel.TabIndex = 8;
            this.passwordLabel.Text = "Password";
            // 
            // loginTextBox
            // 
            this.loginTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.loginTextBox.Location = new System.Drawing.Point(66, 6);
            this.loginTextBox.Name = "loginTextBox";
            this.loginTextBox.Size = new System.Drawing.Size(90, 20);
            this.loginTextBox.TabIndex = 7;
            // 
            // loginLabel
            // 
            this.loginLabel.Location = new System.Drawing.Point(12, 9);
            this.loginLabel.Name = "loginLabel";
            this.loginLabel.Size = new System.Drawing.Size(38, 20);
            this.loginLabel.TabIndex = 6;
            this.loginLabel.Text = "Login";
            // 
            // contactsListBox
            // 
            this.contactsListBox.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.contactsListBox.FormattingEnabled = true;
            this.contactsListBox.Location = new System.Drawing.Point(12, 92);
            this.contactsListBox.Name = "contactsListBox";
            this.contactsListBox.Size = new System.Drawing.Size(156, 537);
            this.contactsListBox.TabIndex = 12;
            this.contactsListBox.SelectedIndexChanged += new System.EventHandler(this.contactsListBox_SelectedIndexChanged);
            // 
            // sendButton
            // 
            this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sendButton.Location = new System.Drawing.Point(789, 530);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(105, 43);
            this.sendButton.TabIndex = 15;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // newMessageTextBox
            // 
            this.newMessageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.newMessageTextBox.Location = new System.Drawing.Point(190, 497);
            this.newMessageTextBox.Multiline = true;
            this.newMessageTextBox.Name = "newMessageTextBox";
            this.newMessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.newMessageTextBox.Size = new System.Drawing.Size(593, 134);
            this.newMessageTextBox.TabIndex = 14;
            this.newMessageTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.newMessageTextBox_KeyDown);
            // 
            // MessagesTextBox
            // 
            this.MessagesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MessagesTextBox.Location = new System.Drawing.Point(190, 12);
            this.MessagesTextBox.Multiline = true;
            this.MessagesTextBox.Name = "MessagesTextBox";
            this.MessagesTextBox.ReadOnly = true;
            this.MessagesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.MessagesTextBox.Size = new System.Drawing.Size(704, 460);
            this.MessagesTextBox.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 643);
            this.Controls.Add(this.sendButton);
            this.Controls.Add(this.newMessageTextBox);
            this.Controls.Add(this.MessagesTextBox);
            this.Controls.Add(this.contactsListBox);
            this.Controls.Add(this.Line);
            this.Controls.Add(this.tryLoginButton);
            this.Controls.Add(this.passwordTextBox);
            this.Controls.Add(this.passwordLabel);
            this.Controls.Add(this.loginTextBox);
            this.Controls.Add(this.loginLabel);
            this.Name = "MainForm";
            this.Text = "Chatter";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Line;
        private System.Windows.Forms.Button tryLoginButton;
        private System.Windows.Forms.TextBox passwordTextBox;
        private System.Windows.Forms.Label passwordLabel;
        private System.Windows.Forms.TextBox loginTextBox;
        private System.Windows.Forms.Label loginLabel;
        private System.Windows.Forms.ListBox contactsListBox;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.TextBox newMessageTextBox;
        private System.Windows.Forms.TextBox MessagesTextBox;
    }
}