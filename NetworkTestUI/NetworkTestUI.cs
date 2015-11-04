﻿using System.Drawing;
using System.Windows.Forms;
using Iris.Infrastructure.Models;

namespace NetworkTestUI
{
    public partial class NetworkTestUI : Form
    {
        public NetworkTestUI()
        {
            InitializeComponent();

            AddClient("Test");

            SetUpMouseHook();
        }

        private void AddClient(string address)
        {
            Panel clientPanel = new Panel();

            clientPanel.BorderStyle = BorderStyle.FixedSingle;

            clientPanel.Parent = flowLayoutPanel1;

            clientPanel.Height = clientPanel.Parent.Height - 4;

            TextBox clientAddress = new TextBox();

            clientAddress.Text = address;

            clientAddress.Parent = clientPanel;

            Label clientLabel = new Label();

            clientLabel.Text = "Hi!";

            clientLabel.Parent = clientPanel;

            clientLabel.Location = new Point(4, 20);
        }

        private void SetUpMouseHook()
        {
            Iris.Core.IrisCore.MouseService.MousePositionChanged += MouseService_MousePositionChanged;
        }

        private void MouseService_MousePositionChanged(MousePosition position)
        {
            label2.Text = "Mouse pos: " + position.X + ", " + position.Y;
        }
    }
}