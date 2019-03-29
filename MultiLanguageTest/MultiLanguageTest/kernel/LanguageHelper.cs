using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiLanguageTest.kernel
{
    public class LanguageHelper
    {
        #region 简繁体转换
        /// <summary>
        /// 内容的语言转化
        /// </summary>
        /// <param name="parent"></param>
        public static void SetControlLanguageText(System.Windows.Forms.Control parent)
        {
            if (parent.HasChildren)
            {
                foreach (System.Windows.Forms.Control ctrl in parent.Controls)
                {
                    SetContainerLanguage(ctrl);
                }
            }
            else
            {
                SetLanguage(parent);
            }
        }
        #endregion
        #region 控件简繁体语言转换

        /// <summary>
        /// 设置容器类控件的语言
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        private static void SetContainerLanguage(System.Windows.Forms.Control ctrl)
        {
            if (ctrl is DataGridView)
            {
                try
                {
                    DataGridView dataGridView = (DataGridView)ctrl;
                    foreach (DataGridViewColumn dgvc in dataGridView.Columns)
                    {
                        try
                        {
                            if (dgvc.HeaderText.ToString() != "" && dgvc.Visible)
                            {
                                dgvc.HeaderText = GetLanguageText(dgvc.HeaderText);
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                catch (Exception)
                { }
            }
            if (ctrl is MenuStrip)
            {
                MenuStrip menuStrip = (MenuStrip)ctrl;
                foreach (ToolStripMenuItem toolItem in menuStrip.Items)
                {
                    try
                    {
                        toolItem.Text = GetLanguageText(toolItem.Text);
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        if (toolItem.DropDownItems.Count > 0)
                        {
                            GetItemText(toolItem);
                        }
                    }
                }
            }
            else if (ctrl is TreeView)
            {
                TreeView treeView = (TreeView)ctrl;
                foreach (TreeNode node in treeView.Nodes)
                {
                    try
                    {
                        node.Text = GetLanguageText(node.Text);
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        if (node.Nodes.Count > 0)
                        {
                            GetNodeText(node);
                        }
                    }
                }
            }
            else if (ctrl is TabControl)
            {
                TabControl tabCtrl = (TabControl)ctrl;
                try
                {
                    foreach (TabPage tabPage in tabCtrl.TabPages)
                    {
                        tabPage.Text = GetLanguageText(tabPage.Text);
                    }
                }
                catch (Exception)
                {
                }
            }
            else if (ctrl is StatusStrip)
            {
                StatusStrip statusStrip = (StatusStrip)ctrl;
                foreach (ToolStripItem toolItem in statusStrip.Items)
                {
                    try
                    {
                        toolItem.Text = GetLanguageText(toolItem.Text);
                    }
                    catch (Exception)
                    {
                    }
                    finally
                    {
                        ToolStripDropDownButton tsDDBtn = toolItem as ToolStripDropDownButton;
                        if (tsDDBtn != null && tsDDBtn.DropDownItems.Count > 0)
                        {
                            GetItemText(tsDDBtn);
                        }
                    }
                }
            }
            else if (ctrl is ToolStrip)
            {
                ToolStrip statusStrip = (ToolStrip)ctrl;
                foreach (ToolStripItem toolItem in statusStrip.Items)
                {
                    try
                    {
                        toolItem.Text = GetLanguageText(toolItem.Text);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            else if (ctrl is CheckedListBox)
            {
                CheckedListBox chkListBox = (CheckedListBox)ctrl;
                try
                {
                    for (int n = 0; n < chkListBox.Items.Count; n++)
                    {
                        chkListBox.Items[n] = GetLanguageText(chkListBox.Items[n].ToString());
                    }
                }
                catch (Exception)
                { }
            }

            if (ctrl.HasChildren)
            {
                foreach (System.Windows.Forms.Control c in ctrl.Controls)
                {
                    SetContainerLanguage(c);
                }
            }
            else
            {
                SetLanguage(ctrl);
            }

        }
        /// <summary>
        /// 设置普通控件的语言
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        private static void SetLanguage(System.Windows.Forms.Control ctrl)
        {
            if (true)
            {
                if (ctrl is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)ctrl;
                    try
                    {
                        checkBox.Text = GetLanguageText(checkBox.Text);
                    }
                    catch (Exception)
                    {
                    }
                }
                else if (ctrl is Label)
                {
                    Label label = (Label)ctrl;
                    try
                    {
                        label.Text = GetLanguageText(label.Text);
                    }
                    catch (Exception)
                    {
                    }
                }

                else if (ctrl is Button)
                {
                    Button button = (Button)ctrl;
                    try
                    {
                        button.Text = GetLanguageText(button.Text);
                    }
                    catch (Exception)
                    {
                    }
                }
                else if (ctrl is GroupBox)
                {
                    GroupBox groupBox = (GroupBox)ctrl;
                    try
                    {
                        groupBox.Text = GetLanguageText(groupBox.Text);
                    }
                    catch (Exception)
                    {
                    }
                }
                else if (ctrl is RadioButton)
                {
                    RadioButton radioButton = (RadioButton)ctrl;
                    try
                    {
                        radioButton.Text = GetLanguageText(radioButton.Text);
                    }
                    catch (Exception)
                    {
                    }
                }
            }

        }
        /// <summary>
        /// 递归转化菜单
        /// </summary>
        /// <param name="menuItem"></param>
        private static void GetItemText(ToolStripDropDownItem menuItem)
        {
            foreach (ToolStripItem toolItem in menuItem.DropDownItems)
            {
                try
                {
                    toolItem.Text = GetLanguageText(toolItem.Text);
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (toolItem is ToolStripDropDownItem)
                    {
                        ToolStripDropDownItem subMenuStrip = (ToolStripDropDownItem)toolItem;
                        if (subMenuStrip.DropDownItems.Count > 0)
                        {
                            GetItemText(subMenuStrip);
                        }
                    }
                }

            }
        }
        /// <summary>
        /// 递归转化树
        /// </summary>
        /// <param name="menuItem"></param>
        private static void GetNodeText(TreeNode node)
        {

            foreach (TreeNode treeNode in node.Nodes)
            {
                try
                {
                    treeNode.Text = GetLanguageText(treeNode.Text);
                }
                catch (Exception)
                {
                }
                finally
                {
                    if (treeNode.Nodes.Count > 0)
                    {
                        GetNodeText(treeNode);
                    }
                }
            }
        }


        /// <summary>
        /// 根据语言标识符得到转换后的值
        /// </summary>
        /// <param name="languageFlag"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetLanguageText(string value)
        {
            string languageFlag = Thread.CurrentThread.CurrentUICulture.Name;
            if (string.IsNullOrWhiteSpace(value))
            {
                return value;
            }

            switch (languageFlag.ToUpper())
            {
                case "ZH-CHT":
                    {
                        return ToTraditional(value);
                    }
                default:
                    {
                        return ToSimplified(value);
                    }
            }
        }

        /// <summary>
        /// 简体转换为繁体
        /// </summary>
        /// <param name="str">简体字</param>
        /// <returns>繁体字</returns>
        private static string ToTraditional(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            return "界面";

        }
        /// <summary>
        /// 繁体转换为简体
        /// </summary>
        /// <param name="str">繁体字</param>
        /// <returns>简体字</returns>
        private static string ToSimplified(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            return "BB";
        }
        #endregion

    }
}
