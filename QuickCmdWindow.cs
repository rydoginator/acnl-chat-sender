using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ntrclient {
	public partial class QuickCmdWindow : Form {
		void loadCmds() {
			for (int i = 0; i <= 9; i++) {
				string[] t = new string[2];
				t[0] = i.ToString();
				t[1] = Program.sm.quickCmds[i];
				dataGridView1.Rows.Add(t);
			}
		}

		public QuickCmdWindow() {
			InitializeComponent();
			
			loadCmds();
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e) {

		}

		private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e) {

		}

		private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {

		}

		private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e) {
			string t, id;
			if (e.RowIndex < 0) {
				return;
			}
			t = (string) dataGridView1.Rows[e.RowIndex].Cells[1].Value;
			id = (string)dataGridView1.Rows[e.RowIndex].Cells[0].Value;
			Program.sm.quickCmds[int.Parse(id)] = t;
		}
	}
}
