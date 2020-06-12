using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;

namespace HWIDChanger
{
	public partial class Changer : Form
	{
		public int Randomnum()
		{
			Random random = new Random();
			return random.Next(1000, 9999);
		}

		public Changer()
		{
			this.InitializeComponent();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\IDConfigDB\\Hardware Profiles\\0001", true))
				{
					bool flag = registryKey != null;
					if (flag)
					{
						object value = registryKey.GetValue("HwProfileGuid");
						bool flag2 = value != null;
						if (flag2)
						{
							string text = value.ToString();
							int num = text.Length - 1;
							int length = text.Length - 34;
							string text2 = text.Substring(33, length);
							string str = text.Substring(0, 33);
							string str2 = "}";
							string str3 = this.Randomnum().ToString();
							string text3 = str + str3 + str2;
							this.textBox2.Text = text3;
							this.textBox1.Text = text;
							this.hasBeenChecked = true;
							if(Properties.Settings.Default.hwidoriginal == "hwidempty")
							{
								Properties.Settings.Default.hwidoriginal = textBox1.Text;
								Properties.Settings.Default.Save();
							}
						}
						else
						{
							MessageBox.Show("There doesn't appear to be a registry key with that name, please try again later!");
						}
					}
					else
					{
						MessageBox.Show("There doesn't appear to be a registry folder with that name, please try again later!");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Fatal application error!");
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			bool flag = this.hasBeenChecked;
			if (flag)
			{
				string text = this.textBox2.Text;
				MessageBox.Show(text);
				try
				{
					using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\IDConfigDB\\Hardware Profiles\\0001", true))
					{
						bool flag2 = registryKey != null;
						if (flag2)
						{
							object value = registryKey.GetValue("HwProfileGuid");
							bool flag3 = value != null;
							if (flag3)
							{
								registryKey.SetValue("HwProfileGuid", text);
								MessageBox.Show("HWID SET!", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
							else
							{
								MessageBox.Show("There doesn't appear to be a registry key with that name, please try again later!");
							}
						}
						else
						{
							MessageBox.Show("There doesn't appear to be a registry folder with that name, please try again later!");
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show("Fatal application error!");
				}
			}
			else
			{
				MessageBox.Show("Please get your current hardware ID first!");
			}
		}

		private const string HWIDGUIPath = "SYSTEM\\CurrentControlSet\\Control\\IDConfigDB\\Hardware Profiles\\0001";

		private bool hasBeenChecked = false;

		private void button3_Click(object sender, EventArgs e)
		{

			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SYSTEM\\CurrentControlSet\\Control\\IDConfigDB\\Hardware Profiles\\0001", true))
				{
					bool flag2 = registryKey != null;
					if (flag2)
					{
						object value = registryKey.GetValue("HwProfileGuid");
						bool flag3 = value != null;
						if (flag3)
						{
							if (Properties.Settings.Default.hwidoriginal == "hwidempty")
							{
								MessageBox.Show("No Original HWID SAVED!");
							}
							else
							{
								registryKey.SetValue("HwProfileGuid", Properties.Settings.Default.hwidoriginal);
								MessageBox.Show("HWID RESTORED!", "Informations", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
						}
						else
						{
							MessageBox.Show("There doesn't appear to be a registry key with that name, please try again later!");
						}
					}
					else
					{
						MessageBox.Show("There doesn't appear to be a registry folder with that name, please try again later!");
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Fatal application error!");
			}
		}
	}
}
