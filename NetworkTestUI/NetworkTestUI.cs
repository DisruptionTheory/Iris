﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Iris.Core;
using Iris.Environment.Win32;
using Iris.Infrastructure.Models;

namespace NetworkTestUI
{
    public partial class NetworkTestUI : Form
    {
        private Dictionary<string, Label> mousePositionLabels = new Dictionary<string, Label>();
        private TextBox _clientAddress;

        public NetworkTestUI()
        {
            InitializeComponent();

            AddClient("Test");

            SetUpMouseHooks();
        }

        private void AddClient(string address)
        {
            Panel clientPanel = new Panel();

            clientPanel.BorderStyle = BorderStyle.FixedSingle;

            clientPanel.Parent = flowLayoutPanel1;

            clientPanel.Height = clientPanel.Parent.Height - 4;

            // This needs to be generalized but a text change event is ... difficult ...
            _clientAddress = new TextBox();

            _clientAddress.Text = address;

            _clientAddress.Parent = clientPanel;

            var mousePositionLabel = new Label
            {
                Text = "Hi!",
                Parent = clientPanel,
                Location = new Point(4, 20),
                Width = 200
            };

            mousePositionLabels[address] = mousePositionLabel;
        }

        private void SetUpMouseHooks()
        {
            MouseHook.MousePositionChanged += MouseHookOnMousePositionChanged;

            //MouseHook.MouseButtonClicked += MouseHookOnMouseButtonClicked;

            IrisCore.MouseService.MousePositionChanged += MouseService_MousePositionChanged;
        }

        private void MouseHookOnMouseButtonClicked(short key)
        {
            lblLocalMouseClick.Text = key + " button clicked";
        }

        private void MouseHookOnMousePositionChanged(long x, long y)
        {
            lblLocalMouseMove.Text = "Local mouse: " + x + ", " + y;

            MousePosition mousePosition = new MousePosition();

            mousePosition.X = x;

            mousePosition.Y = y;

            mousePosition.RecipientId = _clientAddress.Text;

            var t = IrisCore.MouseService.SetMousePosition(mousePosition);
        }

        private void MouseService_MousePositionChanged(MousePosition position)
        {
            this.Invoke(new MethodInvoker(delegate
            {
                mousePositionLabels[mousePositionLabels.Keys.First()].Text = "Mouse pos: " + position.X + ", " + position.Y;
            }));
        }

        private void btnSendStuff_Click(object sender, EventArgs e)
        {
            MouseHookOnMousePositionChanged(5, 10);
        }

        private void NetworkTestUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}