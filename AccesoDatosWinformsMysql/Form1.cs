using AccesoDatosWinForm;
using AccesoDatosWinForm.data;
using System.Data;

namespace AccesoDatosWinformsMysql
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Categories c = new Categories
            {
                CategoryName = "HOLAA",
                Description = "Messi el mejor"
            };
            var conn = new AccesoDatosMySql("localhost", "northwind",
                "root", "", 3306);

            var resukt = conn.ejecutarSentencia(
                "INSERT INTO categories (categoryname, description) " +
                "VALUES (@categoryname, @description)", c
                );

            MessageBox.Show($"Filas afectas {resukt}");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var conn = new AccesoDatosMySql("localhost", "northwind",
                "root", "", 3306);
            string filtro = "c%";
            var tabla = conn.ejecutarQuery("Select * from Categories where categoryname like @filtro;",filtro);
            List<Categories> categories = new List<Categories>();
            foreach (DataRow item in tabla.Rows) {
                Categories c = new Categories()
                {
                    CategoryID = Convert.ToInt32(item[0]),
                    CategoryName = item[1].ToString(),
                    Description = item[2].ToString()
                };
                categories.Add(c);
            }
            if (categories.Count > 0)
            {
                dataGridView1.DataSource = categories;
                cboCategories.ValueMember = "CategoryID";
                cboCategories.DisplayMember = "CategoryName";
                cboCategories.DataSource = categories;
            }

        }
    }
}
