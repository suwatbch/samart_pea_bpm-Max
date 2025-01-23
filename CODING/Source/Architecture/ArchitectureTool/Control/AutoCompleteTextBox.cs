using System;
using System.Collections.Generic;
using System.Text;
using PEA.BPM.Infrastructure.Interface.BusinessEntities;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Threading;

namespace PEA.BPM.Architecture.ArchitectureTool.Control
{
    public class AutoCompleteTextBox<T> : TextBox where T : IListItem
    {
        private int _popupHeight = 60;
        private int _popupExtendWidth = 20;
        private Color _popupBackColor = Color.LightCyan;
        private List<T> _dataSource;
        private T _selectedItem = default(T);
        private ListBox _listBox;
        private bool _popupable = true;

        public AutoCompleteTextBox(System.Windows.Forms.Control replacedControl)
        {
            replacedControl.Visible = false;
            this.Location = replacedControl.Location;
            this.Size = replacedControl.Size;
            this.TabIndex = replacedControl.TabIndex;
            replacedControl.Parent.Controls.Add(this);
            if (replacedControl.Tag != null)
            {
                AutoCompleteTextBox<T> actb = (AutoCompleteTextBox<T>)replacedControl.Tag;
                actb.Dispose();
                replacedControl.Tag = null;
            }
            this.TabIndex = replacedControl.TabIndex;
            replacedControl.Tag = this;
        }

        public static T GetSelectedItem(System.Windows.Forms.Control replacedControl)
        {
            object o = replacedControl.Tag;
            if (o != null)
            {
                AutoCompleteTextBox<T> actb = (AutoCompleteTextBox<T>)o;
                return actb._selectedItem;
            }

            return default(T);
        }

        public static string GetText(System.Windows.Forms.Control replacedControl)
        {
            object o = replacedControl.Tag;
            if (o != null)
            {
                AutoCompleteTextBox<T> actb = (AutoCompleteTextBox<T>)o;
                return actb.Text;
            }

            return null;
        }

        public static void SetSelectedItem(System.Windows.Forms.Control replacedControl, string itemId)
        {
            object o = replacedControl.Tag;
            if (o != null)
            {
                AutoCompleteTextBox<T> actb = (AutoCompleteTextBox<T>)o;
                actb._popupable = false;

                if (itemId != null)
                {
                    T t = actb.DataSource.Find(delegate(T dt)
                    {
                        return (dt.DisplayText.IndexOf(itemId) > -1);
                    });

                    if (null != t)
                    {
                        actb._selectedItem = t;
                        actb.Text = t.DisplayText;
                    }
                    else
                    {
                        actb._selectedItem = default(T);
                        actb.Text = string.Empty;
                    }
                }
                else
                {
                    actb._selectedItem = default(T);
                    actb.Text = string.Empty;
                }

                actb._popupable = true;
            }
        }

        public List<T> DataSource
        {
            get { return _dataSource; }
            set { _dataSource = value; }
        }
        /// <summary>
        /// Default = 60
        /// </summary>
        public int PopupHeight
        {
            set { _popupHeight = value; }
            get { return _popupHeight; }
        }

        /// <summary>
        /// Default = 20
        /// </summary>
        public int PopupExtendWidth
        {
            get { return _popupExtendWidth; }
            set { _popupExtendWidth = value; }
        }

        /// <summary>
        /// Default = LightCyan
        /// </summary>
        public Color PopupBackColor
        {
            get { return _popupBackColor; }
            set { _popupBackColor = value; }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (_listBox != null && _listBox.Visible == true && _listBox.Items.Count > 0)
            {
                switch (e.KeyCode)
                {
                    case Keys.Down:
                        if (_listBox.Items.Count > _listBox.SelectedIndex + 1)
                        {
                            _listBox.SelectedIndex = _listBox.SelectedIndex + 1;
                        }
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.Up:
                        if (_listBox.Items.Count > 1 && _listBox.SelectedIndex > 0)
                        {
                            _listBox.SelectedIndex = _listBox.SelectedIndex - 1;
                        }
                        e.SuppressKeyPress = true;
                        break;
                    case Keys.Enter:
                        if (_listBox.SelectedIndex > -1)
                        {
                            _popupable = false;
                            _selectedItem = (T)_listBox.SelectedItem;
                            this.Text = _selectedItem.DisplayText;
                            this.SelectAll();
                            this.Focus();
                            _listBox.Hide();
                            _listBox.Dispose();
                            _listBox = null;
                            if (ItemSelect != null)
                            {
                                ItemSelect(this, _selectedItem);
                            }

                            _popupable = true;
                            e.SuppressKeyPress = true;
                        }
                        break;
                    case Keys.Escape:
                        _selectedItem = default(T);
                        this.SelectAll();
                        _listBox.Hide();
                        e.SuppressKeyPress = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (e.KeyCode == Keys.Down)
                {
                    e.SuppressKeyPress = true;
                    OnTextChanged(null);
                }
            }

            base.OnKeyDown(e);
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (_popupable)
            {
                if (_listBox != null)
                {
                    _listBox.Visible = true;
                    _listBox.Show();
                }
                else
                {
                    _listBox = new ListBox();
                    _listBox.MouseClick += new MouseEventHandler(_listBox_MouseClick);
                    _listBox.Size = new System.Drawing.Size(this.Width + PopupExtendWidth, _popupHeight);
                    _listBox.Location = new System.Drawing.Point(this.Location.X, this.Location.Y + this.Height + 1);
                    _listBox.BackColor = _popupBackColor;
                    _listBox.HorizontalScrollbar = true;

                    System.Windows.Forms.Control parentCtrl = this.Parent;
                    while (parentCtrl.GetType().BaseType != typeof(Form))
                    {
                        _listBox.Location = new System.Drawing.Point(_listBox.Location.X + parentCtrl.Location.X,
                            _listBox.Location.Y + parentCtrl.Location.Y);
                        parentCtrl = parentCtrl.Parent;
                    }

                    _listBox.DisplayMember = "DisplayText";
                    parentCtrl.Controls.Add(_listBox);
                    parentCtrl.Controls.SetChildIndex(_listBox, 0);
                }

                _selectedItem = default(T);
                List<T> filterB = _dataSource.FindAll(
                    delegate(T t)
                    {
                        return (t.DisplayText.IndexOf(this.Text.ToUpper()) > -1);
                    }
                    );


                _listBox.DataSource = filterB;
            }
        }

        private void _listBox_MouseClick(object sender, MouseEventArgs e)
        {
            OnKeyDown(new KeyEventArgs(Keys.Enter));
        }


        protected override void OnLeave(EventArgs e)
        {
            BackgroundWorker bgw = new BackgroundWorker();
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgw.RunWorkerAsync();
            base.OnLeave(e);
        }

        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(300);
            if (_listBox != null && _listBox.Visible)
            {
                _selectedItem = default(T);
                this.SelectAll();
                _listBox.Hide();
                _listBox.Dispose();
                _listBox = null;
            }
        }

        public delegate void ItemSelectHandler(object sender, T selectedItem);
        public event ItemSelectHandler ItemSelect;
    }
}
