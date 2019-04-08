using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Json;

namespace MultiLanguageTest.kernel
{
    public class LanguageHelper
    {
        #region 多语言转换
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
        #region 控件多语言转换

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
            if (string.IsNullOrWhiteSpace(value) || ConfigurationManager.AppSettings["Language"].ToUpper() == "ZH-CN")
            {
                return value;
            }

            return zhCNToOthers(value);

        }

        /// <summary>
        /// 中文转为其它语言
        /// </summary>
        /// <param name="str">中文</param>
        /// <returns>其它语言</returns>
        private static string zhCNToOthers(string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;
            if (LanguageData != null && LanguageData.ContainsKey(str))
            {
                return LanguageData[str];
            }
            return str;

        }

        #endregion
        #region 加载语言库
        private static Dictionary<string, string> LanguageData;

        public static void LoadLanguageFile()
        {
            string langPath = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["LanguagePath"]
                + ConfigurationManager.AppSettings["Language"].ToUpper() + "\\" + "Basic.json";
            if (File.Exists(langPath))
            {
                using (FileStream fileStream = new FileStream(langPath, FileMode.Open))
                {
                    try
                    {
                        int fsLen = (int)fileStream.Length;
                        byte[] heByte = new byte[fsLen];
                        int r = fileStream.Read(heByte, 0, heByte.Length);
                        string myStr = Encoding.Default.GetString(heByte).Replace("\r\n", "");
                        JsonMe json = new JsonMe();

                        LanguageData = json.JsonToDictionary(myStr);

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    finally
                    {
                        fileStream.Close();
                    }
                }
            }
        }
        #endregion
    }
}
